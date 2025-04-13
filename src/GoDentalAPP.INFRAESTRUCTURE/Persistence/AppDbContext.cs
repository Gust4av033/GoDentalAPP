using Microsoft.EntityFrameworkCore;
using GoDentalAPP.Core.Entities;
using System.Configuration; // Necesario para ConfigurationManager

namespace GoDentalAPP.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        // Constructor para DI (opcional en WPF)
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Constructor sin parámetros (necesario para WPF)
        public AppDbContext() { }

        public DbSet<User> Users { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<InsumoDental> InsumosDentales { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["GoDentalAPP.Properties.Settings.Cconexion"].ConnectionString;
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}