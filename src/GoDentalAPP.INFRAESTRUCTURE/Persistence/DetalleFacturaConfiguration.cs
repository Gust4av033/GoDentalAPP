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
    public class DetalleFacturaConfiguration
    {
        public void Configure(EntityTypeBuilder<DetalleFactura> builder)
        {
            builder.ToTable("DetalleFactura");

            builder.HasKey(df => df.DetalleFacturaID);

            builder.Property(df => df.Cantidad)
                .IsRequired();

            builder.Property(df => df.PrecioUnitario)
                .HasColumnType("decimal(10, 2)")
                .IsRequired();

            builder.Property(df => df.Subtotal)
                .HasColumnType("decimal(10, 2)")
                .IsRequired();

            builder.Property(df => df.MontoImpuesto)
                .HasColumnType("decimal(10, 2)")
                .IsRequired();

            builder.Property(df => df.Total)
                .HasColumnType("decimal(10, 2)")
                .IsRequired();

            // Relationships
            builder.HasOne(df => df.Factura)
                .WithMany(f => f.Detalles)
                .HasForeignKey(df => df.FacturaID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(df => df.InsumoDental)
                .WithMany()
                .HasForeignKey(df => df.InsumoID);

            builder.HasOne(df => df.Impuesto)
                .WithMany()
                .HasForeignKey(df => df.ImpuestoID);
        }
    }
}
