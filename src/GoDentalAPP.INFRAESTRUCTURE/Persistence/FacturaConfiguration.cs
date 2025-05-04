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
    public class FacturaConfiguration
    {
        public void Configure(EntityTypeBuilder<Factura> builder)
        {
            builder.ToTable("Facturas");

            builder.HasKey(f => f.FacturaID);

            builder.Property(f => f.FechaFactura)
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(f => f.TotalFactura)
                .HasColumnType("decimal(10, 2)");

            builder.Property(f => f.NumeroFactura)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(f => f.CodigoGeneracion)
                .HasColumnType("uniqueidentifier");

            builder.Property(f => f.TipoDte)
                .HasMaxLength(2);

            builder.Property(f => f.SelloRecibido)
                .HasMaxLength(500);

            builder.Property(f => f.NumeroControl)
                .HasMaxLength(50);

            builder.Property(f => f.EstadoDte)
                .HasMaxLength(20);

            builder.Property(f => f.CodigoControl)
                .HasMaxLength(100);

            builder.Property(f => f.Observaciones)
                .HasMaxLength(500);

            builder.Property(f => f.NumeroDocumentoCliente)
                .HasMaxLength(20);

            // Relationships
            builder.HasOne(f => f.Venta)
                .WithMany(v => v.Facturas)
                .HasForeignKey(f => f.VentaID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(f => f.TipoPago)
                .WithMany()
                .HasForeignKey(f => f.TipoPagoID);

            builder.HasOne(f => f.Estados)
                .WithMany()
                .HasForeignKey(f => f.EstadoID);

            builder.HasOne(f => f.Cliente)
                .WithMany(c => c.Facturas)
                .HasForeignKey(f => f.ClienteId);

            builder.HasOne(f => f.TipoDocumentoClienteNav)
                .WithMany()
                .HasForeignKey(f => f.TipoDocumentoCliente);
        }
    }
}
