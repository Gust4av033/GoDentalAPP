using Microsoft.EntityFrameworkCore;
using GoDentalAPP.src.GoDentalAPP.CORE.Entities;
using System.Configuration;
using GoDentalAPP.Infrastructure.Persistence.Configurations;

namespace GoDentalAPP.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<InsumoDental> InsumosDentales { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<DetalleFactura> DetallesFactura { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<PedidoProveedor> PedidosProveedores { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<FacturaSincronizacion> FacturasSincronizacion { get; set; }
        public DbSet<TipoDocumento> TiposDocumento { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Elimina los llamados duplicados a base.OnModelCreating
            base.OnModelCreating(modelBuilder);

            // Aplica las configuraciones desde los archivos de configuraci�n
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            // Configuraci�n espec�fica para FacturaSincronizacion
            modelBuilder.Entity<FacturaSincronizacion>(entity =>
            {
                entity.HasKey(f => f.Id);
                entity.HasOne(f => f.Factura)
                    .WithMany()
                    .HasForeignKey(f => f.FacturaId)
                    .OnDelete(DeleteBehavior.Restrict); // Especifica comportamiento de borrado
            });

            // Configuraci�n expl�cita para Cliente
            modelBuilder.Entity<Cliente>(entity =>
            {
                // Configuraci�n de relaciones
                entity.HasOne(c => c.Estado)
                    .WithMany()
                    .HasForeignKey(c => c.EstadoID)
                    .HasConstraintName("FK_Clientes_Estados")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.TipoDocumento)
                    .WithMany()
                    .HasForeignKey(c => c.TiposDocumentoID)
                    .HasConstraintName("FK_Clientes_TiposDocumento")
                    .OnDelete(DeleteBehavior.SetNull); // Permite nulos
            });

            // Configuraci�n expl�cita para TipoDocumento
            modelBuilder.Entity<TipoDocumento>(entity =>
            {
                entity.ToTable("TiposDocumento"); // Nombre exacto de tabla
                entity.HasKey(t => t.TipoDocumentoID);

                // Configura propiedades si es necesario
                entity.Property(t => t.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            // Configuraci�n para evitar el problema de EstadoID1
            modelBuilder.Entity<Estado>(entity =>
            {
                entity.ToTable("Estados");
                entity.HasKey(e => e.EstadoID);

                // Configura propiedades si es necesario
                entity.Property(e => e.NombreEstado)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}