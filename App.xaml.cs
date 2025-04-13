using GoDentalAPP.INFRAESTRUCTURE.Repositorios;
using GoDentalAPP.Infrastructure.Persistence;
using GoDentalAPP.src.GoDentalAPP.APP.Views.Login;
using GoDentalAPP.ViewModels;
using GoDentalAPP.Views.Login;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Windows;

namespace GoDentalAPP
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();

            // Configuración de DbContext
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(ConfigurationManager.ConnectionStrings["GoDentalAPP.Properties.Settings.Cconexion"].ConnectionString),
                ServiceLifetime.Transient);

            // Registrar repositorios
            services.AddScoped<IUserRepository, UserRepository>();

            // Registrar ViewModels
            services.AddTransient<LoginViewModel>();
            services.AddTransient<RegisterUserViewModel>();

            // Registrar vistas
            services.AddTransient<LoginView>();
            services.AddTransient<MainWindow>();
            services.AddTransient<RDUsuario>();

            ServiceProvider = services.BuildServiceProvider();

            // Iniciar con la ventana de login
            var loginView = ServiceProvider.GetRequiredService<LoginView>();
            loginView.Show();
        }
    }
}