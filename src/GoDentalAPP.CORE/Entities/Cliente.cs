using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GoDentalAPP.src.GoDentalAPP.CORE.Entities
{
    
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClienteID { get; set; }

        [Required]
        [StringLength(100)]
        public string NombreCompleto { get; set; }

        [StringLength(20)]
        public string? Telefono { get; set; }

        [StringLength(100)]
        public string? CorreoElectronico { get; set; }

        [StringLength(255)]
        public string? Direccion { get; set; }

        [StringLength(255)]
        public string? LinkDireccion { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [ForeignKey("Estado")]
        public int EstadoID { get; set; } = 1;

        [StringLength(20)]
        public string? NIT { get; set; }

        [StringLength(20)]
        public string? NRC { get; set; }

        [StringLength(50)]
        public string? TipoContribuyente { get; set; }

        [StringLength(100)]
        public string? Giro { get; set; }

        [ForeignKey("TipoDocumento")]
        [Column("TiposDocumentoID")]
        public int? TiposDocumentoID { get; set; }

        [StringLength(20)]
        public string? NumeroDocumento { get; set; }

        // Navigation properties
        public virtual Estado Estado { get; set; }
        public virtual TipoDocumento TipoDocumento { get; set; }
        public virtual ICollection<Venta> Ventas { get; set; }
        public virtual ICollection<Factura> Facturas { get; set; }

        //Calculadas
        public string NombreEstado => Estado?.NombreEstado ?? "No asignado";
        public string NombreTipoDocumento => TipoDocumento?.Nombre ?? "No asignado";

        // Propiedad para verificar estado activo
        public bool EstaActivo => EstadoID == 1; // Asumiendo que 1 es el ID de estado activo

    }
}