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
    public class InsumosDesactivadosConfiguration
    {
        public void Configure(EntityTypeBuilder<InsumosDesactivados> builder)
        {
            builder.ToTable("InsumosDesactivados");

            builder.HasKey(id => id.InsumoID);

            builder.Property(id => id.MotivoDesactivacion)
                .HasMaxLength(255);

            builder.Property(id => id.FechaDesactivacion)
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()");

            // Relationships
            builder.HasOne(id => id.InsumoDental)
                .WithOne()
                .HasForeignKey<InsumosDesactivados>(id => id.InsumoID);
        }   
    }
}
