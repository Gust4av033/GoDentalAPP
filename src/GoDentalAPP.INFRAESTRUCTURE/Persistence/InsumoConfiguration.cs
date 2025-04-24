using GoDentalAPP.src.GoDentalAPP.CORE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoDentalAPP.src.GoDentalAPP.INFRAESTRUCTURE.Persistence
{
    public class InsumoConfiguration : IEntityTypeConfiguration<InsumoDental>
    {
        public void Configure(EntityTypeBuilder<InsumoDental> builder)
        {
            builder.ToTable("InsumosDentales");

            builder.HasKey(i => i.InsumoID);

            builder.Property(i => i.NombreInsumo)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(i => i.Descripcion)
                .HasMaxLength(200);

            builder.Property(i => i.PrecioUnitario)
                .IsRequired()
                .HasColumnType("decimal(10, 2)");

            builder.Property(i => i.CantidadEnStock)
                .IsRequired();

            builder.Property(i => i.FechaRegistro)
                .HasColumnType("datetime");

            builder.Property(i => i.FechaVencimiento)
                .HasColumnType("date");

            builder.Property(i => i.ProveedorID)
                .HasColumnType("int");

            builder.Property(i => i.CategoriaID)
                .HasColumnType("int");

            builder.Property(i => i.CodigoBarras)
                .HasMaxLength(50);

            builder.Property(i => i.TieneImpuesto)
                .HasColumnType("bit");


            // Configuración de la relación con Categoría
            builder.HasOne(i => i.Categoria)
                  .WithMany(c => c.Insumos) // Asegúrate que Categoria tenga la colección Insumos
                  .HasForeignKey(i => i.CategoriaID)
                  .OnDelete(DeleteBehavior.Restrict); // O el comportamiento que prefieras

            // Configuración de la relación con Proveedor
            builder.HasOne(i => i.Proveedor)
                   .WithMany(p => p.Insumos)
                   .HasForeignKey(i => i.ProveedorID)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}