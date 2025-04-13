using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using GoDentalAPP.Helpers;

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

                // Si la vista actual es ProductosViewModel, cargar los datos automáticamente
                if (value is ProductosViewModel productosViewModel)
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

        public MainWindowViewModel()
        {
            // Inicializar comandos
            MostrarProductosCommand = new RelayCommand(o => CurrentView = new ProductosViewModel()); 
            ShowProveedoresCommand = new RelayCommand(o => CurrentView = new ProveedoresViewModel());
            ShowClientesCommand = new RelayCommand(o => CurrentView = new ClientesViewModel());
            ShowVentasCommand = new RelayCommand(o => CurrentView = new VentasViewModel());
            ShowOrdenesCompraCommand = new RelayCommand(o => CurrentView = new OrdenesCompraViewModel());
            ShowUsuariosCommand = new RelayCommand(o => CurrentView = new UsuariosViewModel());
            ShowCalculadoraCommand = new RelayCommand(o => CurrentView = new CalculadoraViewModel());
            ShowReportesCommand = new RelayCommand(o => CurrentView = new ReportesViewModel());
            ShowCalendarioCommand = new RelayCommand(o => CurrentView = new CalendarioViewModel());

            // Vista inicial
            CurrentView = new ProductosViewModel();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}