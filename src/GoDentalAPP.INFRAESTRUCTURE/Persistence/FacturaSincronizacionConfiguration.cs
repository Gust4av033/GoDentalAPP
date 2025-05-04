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
    public class FacturaSincronizacionConfiguration
    {
        public void Configure(EntityTypeBuilder<FacturaSincronizacion> builder)
        {
            builder.ToTable("FacturaSincronizacion");

            builder.HasKey(fs => fs.Id);

            builder.Property(fs => fs.FechaCreacion)
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(fs => fs.UltimoIntento)
                .HasColumnType("datetime");

            builder.Property(fs => fs.IntentosRealizados)
                .HasDefaultValue(0);

            builder.Property(fs => fs.EstadoSincronizacion)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(fs => fs.MensajeError)
                .HasMaxLength(500);

            // Relationships
            builder.HasOne(fs => fs.Factura)
                .WithMany(f => f.Sincronizaciones)
                .HasForeignKey(fs => fs.FacturaId);
        }
    }
}
