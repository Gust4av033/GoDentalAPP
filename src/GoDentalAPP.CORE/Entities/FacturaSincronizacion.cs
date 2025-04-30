using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoDentalAPP.src.GoDentalAPP.CORE.Entities
{
    public class FacturaSincronizacion
    {
        [Key]
        public int Id { get; set; }
        public int FacturaId { get; set; }
        public virtual Factura Factura { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? UltimoIntento { get; set; }
        public int IntentosRealizados { get; set; }
        public string EstadoSincronizacion { get; set; } // Pendiente, EnProceso, Completado, Error
        public string MensajeError { get; set; }
    }
}
