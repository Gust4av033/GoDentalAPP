using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoDentalAPP.src.GoDentalAPP.CORE.Entities
{
    public class DetalleFactura
    {
        [Key]
        public int DetalleFacturaId { get; set; }

        [Required]
        public int FacturaId { get; set; }
        public virtual Factura Factura { get; set; }

        [Required]
        public int InsumoId { get; set; }
        public virtual InsumoDental Insumo { get; set; }

        [Required]
        public decimal Cantidad { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Precio { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Total { get; set; }


    }
}
