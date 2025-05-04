using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoDentalAPP.src.GoDentalAPP.CORE.Entities
{
    public class TipoPago
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TipoPagoID { get; set; }

        [Required]
        [StringLength(50)]
        public string NombreTipoPago { get; set; }

        // Navigation properties
        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
