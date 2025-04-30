using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;
using GoDentalAPP.src.GoDentalAPP.CORE.Interfaces;
using GoDentalAPP.src.GoDentalAPP.INFRAESTRUCTURE.Repositorios;
using System.Text.Json;
using GoDentalAPP.src.GoDentalAPP.CORE.Entities;
using GoDentalAPP.src.GoDentalAPP.CORE.Configuration;
using GoDentalAPP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GoDentalAPP.Infrastructure.Services
{
    public class FacturacionService : IFacturacionService
    {
        private readonly AppDbContext _context;
        private readonly IFacturaRepository _facturaRepo;
        private readonly IDteService _dteService;
        private readonly IPdfService _pdfService;
        private readonly IInsumoRepository _insumoRepo;
        private readonly IClienteRepository _clienteRepo;
        private readonly DteSettings _settings;

        public FacturacionService(
            IFacturaRepository facturaRepo,
            IDteService dteService,
            IPdfService pdfService,
            IInsumoRepository insumoRepo,
            IClienteRepository clienteRepo,
            DteSettings settings)  // Cambiado de IOptions<DteSettings> a DteSettings
        {
            _facturaRepo = facturaRepo;
            _dteService = dteService;
            _pdfService = pdfService;
            _insumoRepo = insumoRepo;
            _clienteRepo = clienteRepo;
            _settings = settings;
        }

        public async Task<Factura> CrearFacturaNormal(FacturaDto facturaDto)
        {
            // Validar stock
            foreach (var detalle in facturaDto.Detalles)
            {
                var insumo = await _insumoRepo.GetInsumoDentalAsync(detalle.InsumoId);
                if (insumo.CantidadEnStock < detalle.Cantidad)
                    throw new Exception($"Stock insuficiente para {insumo.NombreInsumo}");
            }

            // Generar número de factura
            var numeroFactura = await _facturaRepo.GenerarNumeroFactura("01");

            // Crear factura en BD
            var factura = new Factura
            {
                NumeroFactura = numeroFactura,
                FechaFactura = DateTime.Now,
                TipoDte = "01",
                EstadoDte = "PENDIENTE",
                ClienteId = facturaDto.ClienteId,
                Detalles = facturaDto.Detalles.Select(d => new DetalleFactura
                {
                    InsumoId = d.InsumoId,
                    Cantidad = d.Cantidad,
                    Precio = d.PrecioUnitario,
                    Total = d.Total
                }).ToList()
            };
            factura.TotalFactura = factura.Detalles.Sum(d => d.Total);

            var facturaId = await _facturaRepo.CrearFactura(factura);

            // Actualizar stock - versión corregida
            foreach (var detalle in factura.Detalles)
            {
                var insumo = await _insumoRepo.GetInsumoDentalAsync(detalle.InsumoId);
                if (insumo != null)
                {
                    insumo.CantidadEnStock -= (int)detalle.Cantidad;
                    await _insumoRepo.UpdateInsumoDentalAsync(insumo.InsumoID, insumo);
                }
            }

            return await _facturaRepo.ObtenerFacturaPorId(facturaId);
        }

        public async Task<Factura> CrearCreditoFiscal(FacturaDto facturaDto)
        {
            var factura = await CrearFacturaNormal(facturaDto);
            factura.TipoDte = "03";

            // Obtener datos para DTE
            var cliente = await _clienteRepo.ObtenerPorId(facturaDto.ClienteId);
            var token = await _dteService.ObtenerTokenAutenticacion();

            // Generar Guid primero
            var codigoGeneracion = Guid.NewGuid();

            var dteData = new DteData
            {
                Identificacion = new Identificacion
                {
                    TipoDte = "03",
                    NumeroControl = factura.NumeroFactura,
                    CodigoGeneracion = Guid.NewGuid().ToString(),
                    FechaEmision = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")
                },
                Emisor = new Emisor
                {
                    Nit = _settings.Nit,
                    Nrc = _settings.Nrc,
                    Nombre = _settings.NombreEmpresa,
                    CodigoActividad = _settings.CodigoActividad
                },
                Receptor = new Receptor
                {
                    Nit = cliente.Nit,
                    Nombre = cliente.NombreCompleto
                },
                CuerpoDocumento = factura.Detalles.Select((d, i) => new CuerpoDocumento
                {
                    NumItem = i + 1,
                    Cantidad = d.Cantidad,
                    Codigo = d.Insumo.CodigoBarras,
                    Descripcion = d.Insumo.NombreInsumo,
                    PrecioUni = d.Precio,
                    VentaGravada = (bool)d.Insumo.TieneImpuesto ? d.Total : 0,
                    Tributos = (bool)d.Insumo.TieneImpuesto ? "20" :"",
                    IvaItem = (bool)d.Insumo.TieneImpuesto ? d.Total * 0.13m : 0
                }).ToList(),
                Resumen = new Resumen
                {
                    TotalGravada = factura.Detalles
                        .Where(d => (bool)d.Insumo.TieneImpuesto)
                        .Sum(d => d.Total),
                    TotalExenta = factura.Detalles
                        .Where(d => (bool)!d.Insumo.TieneImpuesto)
                        .Sum(d => d.Total),
                    MontoTotalOperacion = factura.Detalles.Sum(d => d.Total),
                    TotalLetras = NumeroALetras(factura.Detalles.Sum(d => d.Total)),
                    Pagos = new List<Pago> { new Pago { Codigo = 1, MontoPago = factura.Detalles.Sum(d => d.Total) } }
                }
            };

            // Enviar al MH
            var response = await _dteService.EnviarDte(dteData, token);

            // Actualizar factura con respuesta
            factura.CodigoGeneracion = codigoGeneracion; // Asignar Guid directamente
            factura.SelloRecibido = response.SelloRecibido;
            factura.EstadoDte = response.Estado;
            factura.JsonDte = JsonSerializer.Serialize(dteData);

            // Generar PDF
            factura.PdfBase64 = await _pdfService.GenerarPdfFactura(factura);

            await _facturaRepo.ActualizarFactura(factura);
            return factura;
        }

        private string NumeroALetras(decimal numero)
        {
            return "SON: " + numero.ToString("N2") + " DÓLARES";
        }

        // Implementación CORREGIDA del método de sincronización
        public async Task<ResultadoSincronizacion> SincronizarFactura(Factura factura)
        {
            var resultado = new ResultadoSincronizacion();

            try
            {
                // 1. Verificar si la factura existe
                var facturaExistente = await _context.Facturas
                    .FirstOrDefaultAsync(f => f.FacturaID == factura.FacturaID);

                if (facturaExistente == null)
                {
                    resultado.Exitoso = false;
                    resultado.MensajeError = "Factura no encontrada";
                    return resultado;
                }

                // 2. Lógica de sincronización con MH (ejemplo)
                var respuestaMH = await SimularEnvioMH(facturaExistente);

                if (respuestaMH.Exito)
                {
                    // 3. Actualizar estado de la factura
                    facturaExistente.Sincronizada = true;
                    facturaExistente.FechaSincronizacion = DateTime.Now;
                    facturaExistente.CodigoMH = respuestaMH.CodigoRespuesta;

                    await _context.SaveChangesAsync();

                    resultado.Exitoso = true;
                }
                else
                {
                    resultado.Exitoso = false;
                    resultado.MensajeError = respuestaMH.MensajeError;
                }
            }
            catch (Exception ex)
            {
                resultado.Exitoso = false;
                resultado.MensajeError = $"Error al sincronizar: {ex.Message}";
            }

            return resultado;
        }

        // Método auxiliar para simular envío al MH
        private async Task<(bool Exito, string CodigoRespuesta, string MensajeError)> SimularEnvioMH(Factura factura)
        {
            // Simular tiempo de espera
            await Task.Delay(1000);

            // Simular respuesta exitosa en el 80% de los casos
            if (new Random().Next(0, 100) < 80)
            {
                return (true, "MH-" + DateTime.Now.Ticks.ToString(), null);
            }

            return (false, null, "Error simulado del servicio MH");
        }
    }
}


    