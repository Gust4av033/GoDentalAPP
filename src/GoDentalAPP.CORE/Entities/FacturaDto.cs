
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoDentalAPP.src.GoDentalAPP.CORE.Entities
{

    public class DetalleFacturaDto
    {
        public int InsumoId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Total => Cantidad * PrecioUnitario;
    }
    public class FacturaDto
    {
        public int ClienteId { get; set; }
        public DateTime FechaFactura { get; set; } = DateTime.Now;
        public decimal TotalFactura { get; set; }
        public string NumeroFactura { get; set; }
        public int? TipoPagoID { get; set; }
        public List<DetalleFacturaDto> Detalles { get; set; } = new List<DetalleFacturaDto>();
    }
}
