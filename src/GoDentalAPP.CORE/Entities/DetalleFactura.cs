﻿using System;
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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DetalleFacturaID { get; set; }

        [ForeignKey("Factura")]
        public int FacturaID { get; set; }

        [ForeignKey("InsumoDental")]
        public int? InsumoID { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal PrecioUnitario { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Subtotal { get; set; }

        [ForeignKey("Impuesto")]
        public int? ImpuestoID { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal MontoImpuesto { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Total { get; set; }

        // Navigation properties
        public virtual Factura Factura { get; set; }
        public virtual InsumoDental InsumoDental { get; set; }
        public virtual Impuesto Impuesto { get; set; }

    }
}
