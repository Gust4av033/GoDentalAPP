using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoDentalAPP.src.GoDentalAPP.CORE.Interfaces;
using GoDentalAPP.src.GoDentalAPP.INFRAESTRUCTURE.Repositorios;
using GoDentalAPP.Infrastructure.Persistence;
using System.Net.NetworkInformation;
using GoDentalAPP.src.GoDentalAPP.CORE.Entities;
using Microsoft.EntityFrameworkCore;


namespace GoDentalAPP.src.GoDentalAPP.INFRAESTRUCTURE.Services
{
    public class SincronizacionService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IFacturacionService _facturacionService;
        private readonly AppDbContext _context;
        private readonly TimeSpan _intervalo = TimeSpan.FromMinutes(5);

        public SincronizacionService(
            IServiceProvider serviceProvider,
            IFacturacionService facturacionService,
            AppDbContext context)
        {
            _serviceProvider = serviceProvider;
            _facturacionService = facturacionService;
            _context = context;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (HayConexionInternet())
                {
                    await SincronizarFacturasPendientes();
                }
                await Task.Delay(_intervalo, stoppingToken);
            }
        }

        private bool HayConexionInternet()
        {
            try
            {
                using (var ping = new Ping())
                {
                    var result = ping.Send("8.8.8.8", 2000);
                    return result?.Status == IPStatus.Success;
                }
            }
            catch
            {
                return false;
            }
        }

        private async Task SincronizarFacturasPendientes()
        {
            var facturasPendientes = await _context.Set<FacturaSincronizacion>()
                .Include(fs => fs.Factura)
                .Where(fs => fs.EstadoSincronizacion == "Pendiente"
                && fs.IntentosRealizados < 3)
                .ToListAsync();

            foreach (var item in facturasPendientes)
            {
                try
                {
                    item.EstadoSincronizacion = "EnProceso";
                    item.UltimoIntento = DateTime.Now;
                    item.IntentosRealizados++;
                    await _context.SaveChangesAsync();

                    // Intentar sincronizar con el MH y obtener resultado explícito
                    var resultadoSincronizacion = await _facturacionService.SincronizarFactura(item.Factura);

                    if (resultadoSincronizacion.Exitoso)
                    {
                        item.EstadoSincronizacion = "Completado";
                        item.Factura.Sincronizada = true;
                    }
                    else
                    {
                        item.EstadoSincronizacion = "Error";
                        item.MensajeError = resultadoSincronizacion.MensajeError;
                    }
                }
                catch (Exception ex)
                {
                    item.EstadoSincronizacion = "Error";
                    item.MensajeError = ex.Message;
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}
