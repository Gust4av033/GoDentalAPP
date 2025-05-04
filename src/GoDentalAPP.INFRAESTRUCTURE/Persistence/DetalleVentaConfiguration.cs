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
    public class DetalleVentaConfiguration
    {
        public void Configure(EntityTypeBuilder<DetalleVenta> builder)
        {
            builder.ToTable("DetalleVenta");

            builder.HasKey(dv => dv.DetalleVentaID);

            builder.Property(dv => dv.Cantidad)
                .IsRequired();

            builder.Property(dv => dv.PrecioUnitario)
                .HasColumnType("decimal(10, 2)")
                .IsRequired();

            builder.Property(dv => dv.Total)
                .HasColumnType("decimal(10, 2)")
                .IsRequired();

            builder.Property(dv => dv.Subtotal)
                .HasColumnType("decimal(10, 2)");

            builder.Property(dv => dv.MontoImpuesto)
                .HasColumnType("decimal(10, 2)");

            // Relationships
            builder.HasOne(dv => dv.Venta)
                .WithMany(v => v.Detalles)
                .HasForeignKey(dv => dv.VentaID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(dv => dv.InsumoDental)
                .WithMany()
                .HasForeignKey(dv => dv.InsumoID);

            builder.HasOne(dv => dv.Impuesto)
                .WithMany(i => i.DetallesVenta)
                .HasForeignKey(dv => dv.ImpuestoID);
        }
    }
}
