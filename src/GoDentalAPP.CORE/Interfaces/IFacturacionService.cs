using GoDentalAPP.src.GoDentalAPP.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoDentalAPP.src.GoDentalAPP.CORE.Interfaces
{
    public interface IFacturacionService
    {
        Task<Factura> CrearFacturaNormal(FacturaDto facturaDto);
        Task<Factura> CrearCreditoFiscal(FacturaDto facturaDto);
        Task<ResultadoSincronizacion> SincronizarFactura(Factura factura);
    }

    public class ResultadoSincronizacion
    {
        public bool Exitoso { get; set; }
        public string MensajeError { get; set; }
    }
}
