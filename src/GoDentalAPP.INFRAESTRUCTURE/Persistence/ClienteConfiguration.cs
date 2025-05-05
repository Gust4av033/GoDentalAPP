using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GoDentalAPP.src.GoDentalAPP.CORE.Entities;

namespace GoDentalAPP.Infrastructure.Persistence.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");

            builder.HasKey(c => c.ClienteID);

            builder.Property(c => c.NombreCompleto)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Telefono)
                .HasMaxLength(20);

            builder.Property(c => c.CorreoElectronico)
                .HasMaxLength(100);

            builder.Property(c => c.Direccion)
                .HasMaxLength(255);

            builder.Property(c => c.LinkDireccion)
                .HasMaxLength(255);

            builder.Property(c => c.FechaRegistro)
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(c => c.EstadoID)
                .HasDefaultValue(1);

            builder.Property(c => c.NIT)
                .HasMaxLength(20);

            builder.Property(c => c.NRC)
                .HasMaxLength(20);

            builder.Property(c => c.TipoContribuyente)
                .HasMaxLength(50);

            builder.Property(c => c.Giro)
                .HasMaxLength(100);

            builder.Property(c => c.NumeroDocumento)
                .HasMaxLength(20);

            // Relationships
            builder.HasOne(c => c.Estado)
                .WithMany()
                .HasForeignKey(c => c.EstadoID) // Nombre exacto
                .HasConstraintName("FK_Clientes_Estados");

            builder.HasOne(c => c.TipoDocumento)
                .WithMany()
                .HasForeignKey(c => c.TiposDocumentoID) // Usar el nombre exacto
                .HasConstraintName("FK_Clientes_TiposDocumento");

        }
    }
}