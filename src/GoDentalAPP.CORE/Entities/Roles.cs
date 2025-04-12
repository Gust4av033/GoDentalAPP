using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // ¡Agrega esto!


namespace GoDentalAPP.Core.Entities
{
    public class Roles // Cambiado a public
    {
        [Key]
        public int RolID { get; set; }
        public required string NombreRol { get; set; }
        public string Descripcion { get; set; }

        // Relación inversa (DEBE ser public)
        public virtual ICollection<User> Usuarios { get; set; }
    }
}