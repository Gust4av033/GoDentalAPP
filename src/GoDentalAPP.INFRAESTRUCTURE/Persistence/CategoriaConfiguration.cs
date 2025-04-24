using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GoDentalAPP.src.GoDentalAPP.CORE.Entities;

namespace GoDentalAPP.INFRAESTRUCTURE.Persistence
{
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categorias");

            builder.HasKey(c => c.CategoriaID);

            builder.Property(c => c.NombreCategoria)
                .IsRequired()
                .HasMaxLength(100);

            // Configura otras propiedades aquí...
        }
    }
}