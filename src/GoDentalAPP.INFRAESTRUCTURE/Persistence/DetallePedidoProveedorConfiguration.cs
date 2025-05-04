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
    public class DetallePedidoProveedorConfiguration
    {
        public void Configure(EntityTypeBuilder<DetallePedidoProveedor> builder)
        {
            builder.ToTable("DetallePedidoProveedor");

            builder.HasKey(dpp => dpp.DetallePedidoID);

            builder.Property(dpp => dpp.Cantidad)
                .IsRequired();

            builder.Property(dpp => dpp.PrecioUnitario)
                .HasColumnType("decimal(10, 2)")
                .IsRequired();

            builder.Property(dpp => dpp.Total)
                .HasColumnType("decimal(10, 2)")
                .IsRequired();

            // Relationships
            builder.HasOne(dpp => dpp.PedidoProveedor)
                .WithMany(pp => pp.Detalles)
                .HasForeignKey(dpp => dpp.PedidoID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(dpp => dpp.InsumoDental)
                .WithMany()
                .HasForeignKey(dpp => dpp.InsumoID);
        }
    }
}
