using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoDentalAPP.src.GoDentalAPP.CORE.Entities
{
    public class FacturaSincronizacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Factura")]
        public int FacturaId { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public DateTime? UltimoIntento { get; set; }

        public int IntentosRealizados { get; set; } = 0;

        [Required]
        [StringLength(20)]
        public string EstadoSincronizacion { get; set; }

        [StringLength(500)]
        public string? MensajeError { get; set; }

        // Navigation property
        public virtual Factura Factura { get; set; }
    }
}
