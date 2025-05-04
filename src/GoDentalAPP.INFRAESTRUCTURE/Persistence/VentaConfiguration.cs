using GoDentalAPP.src.GoDentalAPP.CORE.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoDentalAPP.src.GoDentalAPP.INFRAESTRUCTURE.Persistence
{
    public class VentaConfiguration
    {
        public void Configure(EntityTypeBuilder<Venta> builder)
        {
            builder.ToTable("Ventas");

            builder.HasKey(v => v.VentaID);

            builder.Property(v => v.FechaVenta)
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(v => v.TotalVenta)
                .HasColumnType("decimal(10, 2)")
                .IsRequired();

            // Relationships
            builder.HasOne(v => v.Cliente)
                .WithMany(c => c.Ventas)
                .HasForeignKey(v => v.ClienteID)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(v => v.Usuario)
                .WithMany()
                .HasForeignKey(v => v.UsuarioID)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
