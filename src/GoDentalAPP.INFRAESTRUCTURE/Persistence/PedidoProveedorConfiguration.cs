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
    public class PedidoProveedorConfiguration
    {
        public void Configure(EntityTypeBuilder<PedidoProveedor> builder)
        {
            builder.ToTable("PedidosProveedores");

            builder.HasKey(pp => pp.PedidoID);

            builder.Property(pp => pp.FechaPedido)
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(pp => pp.FechaEntrega)
                .HasColumnType("datetime");

            builder.Property(pp => pp.TotalPedido)
                .HasColumnType("decimal(10, 2)")
                .IsRequired();

            // Relationships
            builder.HasOne(pp => pp.Proveedor)
                .WithMany(p => p.Pedidos)
                .HasForeignKey(pp => pp.ProveedorID)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(pp => pp.Usuario)
                .WithMany()
                .HasForeignKey(pp => pp.UsuarioID)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(pp => pp.Estado)
                .WithMany()
                .HasForeignKey(pp => pp.EstadoID);

        }
    }
}
