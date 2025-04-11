// filepath: src/GoDentalAPP.CORE/Entities/User.cs
using Microsoft.Xaml.Behaviors.Media;

public class User
{
    public int UserID { get; set; }
    public string NombreUsuario { get; set; }
    public string CorreoElectronico { get; set; }
    public string Contrasena { get; set; }
    public int? RolID { get; set; }

    public DateTime FechaRegistro { get; set; }
    public bool Estado { get; set; }

    public StoryboardTrigger Rol { get; set; } // Relación con la entidad Rol
}