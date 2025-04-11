using Microsoft.EntityFrameworkCore; // Ensure this is included
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GoDentalAPP.Core.Entities;

namespace GoDentalAPP.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Usuarios"); // Maps the entity to the table "Usuarios"
            builder.HasKey(u => u.UserID);
            builder.Property(u => u.NombreUsuario).IsRequired().HasMaxLength(50);
            builder.Property(u => u.CorreoElectronico).IsRequired().HasMaxLength(100);
            builder.Property(u => u.Contrasena).IsRequired().HasMaxLength(255);
            builder.Property(u => u.FechaRegistro).HasDefaultValueSql("GETDATE()");
            builder.Property(u => u.Estado).HasDefaultValue(true);
            builder.HasOne(u => u.Rol)
                   .WithMany()
                   .HasForeignKey(u => u.RolID)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
