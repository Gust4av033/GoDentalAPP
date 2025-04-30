using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoDentalAPP.src.GoDentalAPP.CORE.Entities
{
    public class Factura
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FacturaID { get; set; }

        public int? VentaID { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime FechaFactura { get; set; } = DateTime.Now;

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal TotalFactura { get; set; }

        [Required]
        [StringLength(50)]
        public string NumeroFactura { get; set; }

        public int? TipoPagoID { get; set; }

        public int? EstadoID { get; set; }

        public Guid? CodigoGeneracion { get; set; }

        [StringLength(2)]
        public string TipoDte { get; set; }

        [StringLength(500)]
        public string SelloRecibido { get; set; }

        [StringLength(50)]
        public string NumeroControl { get; set; }

        [StringLength(20)]
        public string EstadoDte { get; set; }

        public string JsonDte { get; set; }

        public string PdfBase64 { get; set; }

        public int ClienteId { get; set; }
//propiedades para guardar factura offline
        public string Serie { get; set; }
        public bool EsOffline { get; set; }
        public bool Sincronizada { get; set; }
        public string RutaPdfLocal { get; set; }
        public string SerieOffline { get; set; }
        public DateTime FechaSincronizacion { get; set; }
        public string CodigoMH { get; set; }

        // Propiedades de navegación
        // public virtual TipoPago TipoPago { get; set; }
        public virtual Estado Estado { get; set; }
       // public virtual Venta Venta { get; set; }
        public virtual ICollection<DetalleFactura> Detalles { get; set; } = new List<DetalleFactura>();
    }
}