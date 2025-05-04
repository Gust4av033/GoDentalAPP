using GoDentalAPP.src.GoDentalAPP.CORE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoDentalAPP.src.GoDentalAPP.INFRAESTRUCTURE.Persistence
{
    public class ProveedorConfiguration
    {
        public void Configure(EntityTypeBuilder<Proveedor> builder)
        {
            builder.ToTable("Proveedores");

            builder.HasKey(p => p.ProveedorID);

            builder.Property(p => p.NombreProveedor)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Contacto)
                .HasMaxLength(100);

            builder.Property(p => p.Telefono)
                .HasMaxLength(20);

            builder.Property(p => p.CorreoElectronico)
                .HasMaxLength(100);

            builder.Property(p => p.Direccion)
                .HasMaxLength(255);

            builder.Property(p => p.FechaRegistro)
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(p => p.EstadoID)
                .HasDefaultValue(1);

            // Relationships
            builder.HasOne(p => p.Estado)
                .WithMany()
                .HasForeignKey(p => p.EstadoID);
        }
    }
}
