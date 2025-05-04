using GoDentalAPP.src.GoDentalAPP.APP.Views.Login;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoDentalAPP.src.GoDentalAPP.CORE.Entities
{
    public class Venta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VentaID { get; set; }

        [ForeignKey("Cliente")]
        public int? ClienteID { get; set; }

        public DateTime FechaVenta { get; set; } = DateTime.Now;

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal TotalVenta { get; set; }

        [ForeignKey("Usuario")]
        public int? UsuarioID { get; set; }

        // Navigation properties
        public virtual Cliente Cliente { get; set; }
        public virtual User Usuario { get; set; }
        public virtual ICollection<DetalleVenta> Detalles { get; set; }
        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
