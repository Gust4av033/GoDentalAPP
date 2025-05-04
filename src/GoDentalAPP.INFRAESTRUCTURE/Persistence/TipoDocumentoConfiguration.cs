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
    public class TipoDocumentoConfiguration
    {
        public void Configure(EntityTypeBuilder<TipoDocumento> builder)
        {
            builder.ToTable("TiposDocumento");

            builder.HasKey(td => td.TipoDocumentoID);

            builder.Property(td => td.Codigo)
                .HasMaxLength(5);

            builder.Property(td => td.Nombre)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(td => td.Descripcion)
                .HasMaxLength(255);
        }
    }
}
