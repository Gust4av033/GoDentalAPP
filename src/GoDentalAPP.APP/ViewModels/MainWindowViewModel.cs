using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;
using GoDentalAPP.Helpers;
using GoDentalAPP.src.GoDentalAPP.APP.Views.ViewsProducto;
using GoDentalAPP.src.GoDentalAPP.CORE.Interfaces;
using GoDentalAPP.src.GoDentalAPP.INFRAESTRUCTURE.Repositorios;
using Microsoft.Extensions.DependencyInjection;


namespace GoDentalAPP.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private object _currentView = new object();

        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();

                // Si la vista actual contiene ProductosViewModel, cargar los datos
                if (value is ContentControl contentControl &&
                    contentControl.DataContext is ProductosViewModel productosViewModel)
                {
                    productosViewModel.MostrarProductosCommand.Execute(null);
                }
            }
        }

        public ICommand MostrarProductosCommand { get; }
        public ICommand ShowProveedoresCommand { get; }
        public ICommand ShowClientesCommand { get; }
        public ICommand ShowVentasCommand { get; }
        public ICommand ShowOrdenesCompraCommand { get; }
        public ICommand ShowUsuariosCommand { get; }
        public ICommand ShowCalculadoraCommand { get; }
        public ICommand ShowReportesCommand { get; }
        public ICommand ShowCalendarioCommand { get; }
        public ICommand ShowFacturasCommand { get; } // Comando para mostrar la vista de facturas

        public MainWindowViewModel()
        {
            // Inicializar comandos con la vista correcta
            MostrarProductosCommand = new RelayCommand(o =>
            {
                var productosView = App.ServiceProvider.GetRequiredService<ProductosMainWindow>();
                CurrentView = productosView;
            });

            ShowProveedoresCommand = new RelayCommand(o => CurrentView = new ProveedoresViewModel());
            ShowClientesCommand = new RelayCommand(o => CurrentView = new ClientesViewModel());
            ShowVentasCommand = new RelayCommand(o => CurrentView = new VentasViewModel());
            ShowOrdenesCompraCommand = new RelayCommand(o => CurrentView = new OrdenesCompraViewModel());
            ShowUsuariosCommand = new RelayCommand(o => CurrentView = new UsuariosViewModel());
            ShowCalculadoraCommand = new RelayCommand(o => CurrentView = new CalculadoraViewModel());
            ShowReportesCommand = new RelayCommand(o => CurrentView = new ReportesViewModel());
            ShowCalendarioCommand = new RelayCommand(o => CurrentView = new CalendarioViewModel());
            ShowFacturasCommand = new RelayCommand(o =>
            {
                var facturacionService = App.ServiceProvider.GetRequiredService<IFacturacionService>();
                var clienteRepository = App.ServiceProvider.GetRequiredService<IClienteRepository>();
                var insumoRepository = App.ServiceProvider.GetRequiredService<IInsumoRepository>();
                CurrentView = new FacturacionViewModel(facturacionService, clienteRepository, insumoRepository);
            });

            // Vista inicial
            MostrarProductosCommand.Execute(null);
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}