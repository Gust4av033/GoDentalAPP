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
    public class ImpuestoConfiguration
    {
        public void Configure(EntityTypeBuilder<Impuesto> builder)
        {
            builder.ToTable("Impuestos");

            builder.HasKey(i => i.ImpuestoID);

            builder.Property(i => i.Codigo)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(i => i.Nombre)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(i => i.Porcentaje)
                .HasColumnType("decimal(5, 2)")
                .IsRequired();

            builder.Property(i => i.Descripcion)
                .HasMaxLength(255);
        }
    }
}
