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
using Microsoft.Extensions.Http;
using static GoDentalAPP.src.GoDentalAPP.INFRAESTRUCTURE.Repositorios.IFacturaRepository;
using System.Net.Http.Headers;


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

            // Cargar configuración de DteSettings desde App.config
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

            // Registrar DteSettings como Singleton
            services.AddSingleton(dteSettings);

            // Configuración de HttpClient para DteService
            services.AddHttpClient<IDteService, DteService>((provider, client) =>
            {
                var settings = provider.GetRequiredService<DteSettings>();
                client.BaseAddress = new Uri(settings.ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            });

            // Otros servicios
            services.AddScoped<IPdfService, PdfService>();
            services.AddScoped<IFacturacionService, FacturacionService>();

            // Repositorios
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IInsumoRepository, InsumoRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IFacturaRepository, FacturaRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IProveedorRepository, ProveedorRepository>();

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

            ServiceProvider = services.BuildServiceProvider();

            var loginView = ServiceProvider.GetRequiredService<LoginView>();
            loginView.Show();
        }


        private string GetConfigValueOrThrow(string key)
        {
            var value = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrEmpty(value))
                throw new ApplicationException($"La configuración requerida '{key}' no se encuentra en App.config");
            return value;
        }
    }
}