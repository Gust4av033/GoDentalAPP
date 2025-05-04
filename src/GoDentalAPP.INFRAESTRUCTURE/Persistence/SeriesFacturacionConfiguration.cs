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
    public class SeriesFacturacionConfiguration
    {
        public void Configure(EntityTypeBuilder<SeriesFacturacion> builder)
        {
            builder.ToTable("SeriesFacturacion");

            builder.HasKey(sf => sf.SerieId);

            builder.Property(sf => sf.TipoDocumento)
                .IsRequired()
                .HasMaxLength(2);

            builder.Property(sf => sf.Serie)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(sf => sf.NumeroActual)
                .IsRequired();

            builder.Property(sf => sf.Activa)
                .IsRequired()
                .HasDefaultValue(true);
        }
    }
}
