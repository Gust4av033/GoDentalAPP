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

        [ForeignKey("Venta")]
        public int? VentaID { get; set; }

        public DateTime FechaFactura { get; set; } = DateTime.Now;

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal TotalFactura { get; set; }

        [Required]
        [StringLength(50)]
        public string NumeroFactura { get; set; }

        [ForeignKey("TipoPago")]
        public int? TipoPagoID { get; set; }

        [ForeignKey("Estado")]
        public int? EstadoID { get; set; }

        public Guid? CodigoGeneracion { get; set; }

        [StringLength(2)]
        public string? TipoDte { get; set; }

        [StringLength(500)]
        public string? SelloRecibido { get; set; }

        [StringLength(50)]
        public string? NumeroControl { get; set; }

        [StringLength(20)]
        public string? EstadoDte { get; set; }

        public string? JsonDte { get; set; }

        public string? PdfBase64 { get; set; }

        [ForeignKey("Cliente")]
        public int? ClienteId { get; set; }

        [StringLength(100)]
        public string? CodigoControl { get; set; }

        public DateTime? FechaHoraCertificacion { get; set; }

        [StringLength(100)]
        public string? CAE { get; set; }

        public DateTime? FechaVencimientoCAE { get; set; }

        [StringLength(500)]
        public string? Observaciones { get; set; }

        [ForeignKey("TipoDocumentoCliente")]
        public int? TipoDocumentoCliente { get; set; }

        [StringLength(20)]
        public string? NumeroDocumentoCliente { get; set; }

        // Navigation properties
        public virtual Venta Venta { get; set; }
        public virtual TipoPago TipoPago { get; set; }
        public virtual Estado Estados { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual TipoDocumento TipoDocumentoClienteNav { get; set; }
        public virtual ICollection<DetalleFactura> Detalles { get; set; }
        public virtual ICollection<FacturaSincronizacion> Sincronizaciones { get; set; }
        public bool Sincronizada { get; internal set; }
        public DateTime FechaSincronizacion { get; internal set; }
        public string CodigoMH { get; internal set; }
    }
}