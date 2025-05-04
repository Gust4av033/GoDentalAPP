using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoDentalAPP.src.GoDentalAPP.CORE.Entities
{
    public class Impuesto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImpuestoID { get; set; }

        [Required]
        [StringLength(10)]
        public string Codigo { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [Column(TypeName = "decimal(5, 2)")]
        public decimal Porcentaje { get; set; }

        [StringLength(255)]
        public string? Descripcion { get; set; }

        // Navigation properties
        public virtual ICollection<DetalleFactura> DetallesFactura { get; set; }
        public virtual ICollection<DetalleVenta> DetallesVenta { get; set; }
    }
}
