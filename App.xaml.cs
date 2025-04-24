using GoDentalAPP.INFRAESTRUCTURE.Repositorios;
using GoDentalAPP.Infrastructure.Persistence;
using GoDentalAPP.Infrastructure.Services;
using GoDentalAPP.src.GoDentalAPP.APP.Views.Login;
using GoDentalAPP.src.GoDentalAPP.APP.Views.ViewsProducto;
using GoDentalAPP.src.GoDentalAPP.CORE.Configuration;
using GoDentalAPP.src.GoDentalAPP.CORE.Interfaces;
using GoDentalAPP.src.GoDentalAPP.INFRAESTRUCTURE.Repositorios;
using GoDentalAPP.src.GoDentalAPP.INFRAESTRUCTURE.Services;
using GoDentalAPP.ViewModels;
using GoDentalAPP.Views.Login;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using System.Windows;
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

            // Configuración de DbContext
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(ConfigurationManager.ConnectionStrings["GoDentalAPP.Properties.Settings.Cconexion"].ConnectionString),
                ServiceLifetime.Transient);

            // Configuración de DteSettings desde App.config
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

            // Servicios
            services.AddScoped<IDteService, DteService>();
            services.AddScoped<IPdfService, PdfService>();
            services.AddScoped<IFacturacionService, FacturacionService>();

            // Repositorios
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IInsumoRepository, InsumoRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IFacturaRepository, FacturaRepository>();

            // ViewModels
            services.AddTransient<LoginViewModel>();
            services.AddTransient<RegisterUserViewModel>();
            services.AddTransient<FacturacionViewModel>();
            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<ProductosViewModel>();

            // Vistas
            services.AddTransient<LoginView>();
            services.AddTransient<MainWindow>();
            services.AddTransient<RDUsuario>();
            services.AddTransient<ProductosMainWindow>();

            // Construcción del proveedor de servicios
            ServiceProvider = services.BuildServiceProvider();

            // Iniciar con la ventana de login
            var loginView = ServiceProvider.GetRequiredService<LoginView>();
            loginView.Show();
        }
    }
}
