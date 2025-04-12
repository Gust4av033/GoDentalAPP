using Microsoft.EntityFrameworkCore;
using GoDentalAPP.Core.Entities;
using System.Configuration; // Necesario para ConfigurationManager

namespace GoDentalAPP.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public AppDbContext()
        {
        }

        // DbSets para las entidades
        public DbSet<User> Users { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        

        protected override void     OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Accede directamente a la cadena de conexión
                string connectionString = ConfigurationManager.ConnectionStrings["GoDentalAPP.Properties.Settings.Cconexion"].ConnectionString;
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }
    }
}