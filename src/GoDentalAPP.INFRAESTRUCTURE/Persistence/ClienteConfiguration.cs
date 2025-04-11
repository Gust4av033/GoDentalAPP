using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GoDentalAPP.Core.Entities;

namespace GoDentalAPP.Infrastructure.Persistence.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");
            builder.HasKey(c => c.ClienteID);
            builder.Property(c => c.NombreCompleto).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Telefono).HasMaxLength(20);
            builder.Property(c => c.CorreoElectronico).HasMaxLength(100);
            builder.Property(c => c.Direccion).HasMaxLength(255);
            builder.Property(c => c.LinkDireccion).HasMaxLength(255);
            builder.Property(c => c.FechaRegistro).HasDefaultValueSql("GETDATE()");
            builder.HasOne(c => c.Estado)
                   .WithMany(e => e.Clientes)
                   .HasForeignKey(c => c.EstadoID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}