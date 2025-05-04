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
    public class TipoPagoConfiguration
    {
        public void Configure(EntityTypeBuilder<TipoPago> builder)
        {
            builder.ToTable("TiposPago");

            builder.HasKey(tp => tp.TipoPagoID);

            builder.Property(tp => tp.NombreTipoPago)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
