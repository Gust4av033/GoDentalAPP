using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoDentalAPP.Core.Entities
{
    public class User
    {
        [Key]
        [Column("UsuarioID")] // Mapea a la columna "UsuarioID" en la BD
        public int UserID { get; set; }

        [Required]
        [Column("NombreUsuario")]
        public string NombreUsuario { get; set; }

        [Required]
        [Column("CorreoElectronico")]
        public string CorreoElectronico { get; set; }

        [Required]
        [Column("Contrasena")]
        public string Contrasena { get; set; }

        [Column("RolID")]
        public int? RolID { get; set; } // Clave foránea nullable

        // Propiedad de navegación
        [ForeignKey("RolID")]
        public virtual Roles Rol { get; set; }

        [Column("FechaRegistro")]
        public DateTime FechaRegistro { get; set; }

        public int? EstadoId { get; set; }  // Estado como FK (nullable o no)
        public Estado Estado { get; set; }  // Propiedad de navegación

        // Propiedad calculada para el nombre del rol (opcional)
        [NotMapped]
        public string NombreRol => Rol?.NombreRol;
    }
}