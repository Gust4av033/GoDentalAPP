using Microsoft.EntityFrameworkCore;
using GoDentalAPP.src.GoDentalAPP.CORE.Entities; 
using System.Configuration; // Necesario para ConfigurationManager
using GoDentalAPP.src.GoDentalAPP.INFRAESTRUCTURE.Persistence;

namespace GoDentalAPP.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        // Constructor para DI (opcional en WPF)
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
        public DbSet<FacturaSincronizacion> FacturasSincronizacion { get; set; } // Cambiado a DbSet


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {          
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            // Configuración adicional para FacturaSincronizacion si es necesaria
            modelBuilder.Entity<FacturaSincronizacion>(entity =>
            {
                entity.HasKey(f => f.Id);
                entity.HasOne(f => f.Factura)
                      .WithMany()
                      .HasForeignKey(f => f.FacturaId);
            });
        }
    }
}