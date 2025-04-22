using GoDentalAPP.Core.Entities;
using GoDentalAPP.INFRAESTRUCTURE.Repositorios;
using GoDentalAPP.Infrastructure.Persistence;
using GoDentalAPP.Infrastructure.Services;
using GoDentalAPP.src.GoDentalAPP.APP.Views.Login;
using GoDentalAPP.src.GoDentalAPP.CORE.Configuration;
using GoDentalAPP.src.GoDentalAPP.INFRAESTRUCTURE.Repositorios;
using GoDentalAPP.ViewModels;
using GoDentalAPP.Views.Login;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Windows;
using GoDentalAPP.src.GoDentalAPP.CORE.Interfaces;
using GoDentalAPP.src.GoDentalAPP.INFRAESTRUCTURE.Services;
using static GoDentalAPP.src.GoDentalAPP.INFRAESTRUCTURE.Repositorios.IFacturaRepository;

namespace GoDentalAPP
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();

            // 1. Configuración de DbContext
            ConfigureDbContext(services);

            // 2. Configuración de servicios de aplicación
            ConfigureApplicationServices(services);

            // 3. Registrar repositorios
            ConfigureRepositories(services);

            // 4. Registrar ViewModels
            ConfigureViewModels(services);

            // 5. Registrar vistas
            ConfigureViews(services);

            ServiceProvider = services.BuildServiceProvider();

            // Iniciar con la ventana de login
            var loginView = ServiceProvider.GetRequiredService<LoginView>();
            loginView.Show();
        }

        private void ConfigureDbContext(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(ConfigurationManager.ConnectionStrings["GoDentalAPP.Properties.Settings.Cconexion"].ConnectionString),
                ServiceLifetime.Transient);
        }

        private void ConfigureApplicationServices(IServiceCollection services)
        {
            // Configurar DteSettings desde App.config
            var dteSettings = new DteSettings
            {
                ApiUrl = ConfigurationManager.AppSettings["DteSettings.ApiUrl"],
                Nit = ConfigurationManager.AppSettings["DteSettings.Nit"],
                Nrc = ConfigurationManager.AppSettings["DteSettings.Nrc"],
                Usuario = ConfigurationManager.AppSettings["DteSettings.Usuario"],
                Clave = ConfigurationManager.AppSettings["DteSettings.Clave"],
                NombreEmpresa = ConfigurationManager.AppSettings["DteSettings.NombreEmpresa"],
                CodigoActividad = ConfigurationManager.AppSettings["DteSettings.CodigoActividad"]
            };
            services.AddSingleton(dteSettings);
            

            // Servicios de la aplicación
            services.AddScoped<IDteService, DteService>();
            services.AddScoped<IPdfService, PdfService>();
            services.AddScoped<IFacturacionService, FacturacionService>();
        }

        private void ConfigureRepositories(IServiceCollection services)
        {
            // Repositorios de datos
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IInsumoRepository, InsumoRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IFacturaRepository, FacturaRepository>();
        }

        private void ConfigureViewModels(IServiceCollection services)
        {
            // ViewModels
            services.AddTransient<LoginViewModel>();
            services.AddTransient<RegisterUserViewModel>();
            services.AddTransient<FacturacionViewModel>();
        }

        private void ConfigureViews(IServiceCollection services)
        {
            // Vistas
            services.AddTransient<LoginView>();
            services.AddTransient<MainWindow>();
            services.AddTransient<RDUsuario>();
        }
    }
}