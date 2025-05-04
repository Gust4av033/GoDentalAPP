using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoDentalAPP.src.GoDentalAPP.CORE.Entities
{
    public class DetallePedidoProveedor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DetallePedidoID { get; set; }

        [ForeignKey("PedidoProveedor")]
        public int? PedidoID { get; set; }

        [ForeignKey("InsumoDental")]
        public int? InsumoID { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal PrecioUnitario { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Total { get; set; }

        // Navigation properties
        public virtual PedidoProveedor PedidoProveedor { get; set; }
        public virtual InsumoDental InsumoDental { get; set; }
    }
}
