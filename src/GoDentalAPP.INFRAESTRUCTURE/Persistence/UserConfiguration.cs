using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GoDentalAPP.src.GoDentalAPP.CORE.Entities;

namespace GoDentalAPP.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Usuarios");
            builder.HasKey(u => u.UserID);

            builder.Property(u => u.NombreUsuario)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.CorreoElectronico)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Contrasena)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(u => u.FechaRegistro)
                .HasDefaultValueSql("GETDATE()");

            // Establecer valor por defecto para la FK
            builder.Property(u => u.EstadoId)
                   .HasDefaultValue(1); // Suponiendo que el ID 1 es "Activo"

            builder.HasOne(u => u.Estado)
                   .WithMany()
                   .HasForeignKey(u => u.EstadoId);

            // Configuración CORRECTA de la relación:
            builder.HasOne(u => u.Rol)  // Propiedad de navegación a Roles
                   .WithMany(r => r.Usuarios)  // Colección inversa en Roles
                   .HasForeignKey(u => u.RolID)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}