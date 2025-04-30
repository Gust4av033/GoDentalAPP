using GoDentalAPP.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoDentalAPP.src.GoDentalAPP.CORE.Entities;

namespace GoDentalAPP.src.GoDentalAPP.INFRAESTRUCTURE.Repositorios
{
    public interface IFacturaRepository
    {
        Task<string> GenerarNumeroFactura(string tipoDte);
        Task<int> CrearFactura(Factura factura);
        Task<Factura> ObtenerFacturaPorId(int id);
        Task<bool> ActualizarEstadoDte(int facturaId, string estado, string selloRecibido = null);
        Task<List<Factura>> BuscarFacturas(int? clienteId = null, string estado = null);
        Task<List<Factura>> ObtenerFacturasPendientesSincronizacion();
        Task ActualizarFactura(Factura factura);

        public class FacturaRepository : IFacturaRepository
        {
            private readonly AppDbContext _context;

            public FacturaRepository(AppDbContext context)
            {
                _context = context;
            }

            public async Task<string> GenerarNumeroFactura(string tipoDte)
            {
                var comando = _context.Database.GetDbConnection().CreateCommand();
                comando.CommandText = "EXEC sp_ObtenerNumeroFactura @TipoDocumento";
                comando.Parameters.Add(new SqlParameter("@TipoDocumento", tipoDte));

                var resultado = await comando.ExecuteScalarAsync();
                return resultado.ToString();
            }

            public async Task<int> CrearFactura(Factura factura)
            {
                _context.Facturas.Add(factura);
                await _context.SaveChangesAsync();
                return factura.FacturaID;
            }

            public async Task<Factura> ObtenerFacturaPorId(int id)
            {
                return await _context.Facturas
                    .Include(f => f.Detalles)
                    .ThenInclude(d => d.Insumo)
                    .FirstOrDefaultAsync(f => f.FacturaID == id);
            }

            public async Task<bool> ActualizarEstadoDte(int facturaId, string estado, string selloRecibido = null)
            {
                var factura = await _context.Facturas.FindAsync(facturaId);
                if (factura == null) return false;

                factura.EstadoDte = estado;
                if (selloRecibido != null)
                    factura.SelloRecibido = selloRecibido;

                await _context.SaveChangesAsync();
                return true;
            }

            public Task ActualizarFactura(Factura factura)
            {
                throw new NotImplementedException();
            }

            public Task<List<Factura>> BuscarFacturas(int? clienteId = null, string estado = null)
            {
                throw new NotImplementedException();
            }

            public Task<List<Factura>> ObtenerFacturasPendientesSincronizacion()
            {
                throw new NotImplementedException();
            }
        }
    }
}
