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
    public class ControlVentasMensualesConfiguration
    {
        public void Configure(EntityTypeBuilder<ControlVentasMensuales> builder)
        {
            builder.ToTable("ControlVentasMensuales");

            builder.HasKey(cvm => cvm.ControlID);

            builder.Property(cvm => cvm.Mes)
                .IsRequired();

            builder.Property(cvm => cvm.Anio)
                .IsRequired();

            builder.Property(cvm => cvm.TotalVentas)
                .HasColumnType("decimal(10, 2)")
                .IsRequired();
        }
    }
}
