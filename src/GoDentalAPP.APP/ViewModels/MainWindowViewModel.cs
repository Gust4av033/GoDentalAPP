using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using GoDentalAPP.Helpers;

namespace GoDentalAPP.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private object _currentView;

        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public ICommand ShowClientesCommand { get; }
        public ICommand ShowProductosCommand { get; }
        public ICommand ShowVentasCommand { get; }

        public MainWindowViewModel()
        {
            // Inicializar comandos
            ShowClientesCommand = new RelayCommand(o => CurrentView = new ClientesViewModel());
            ShowProductosCommand = new RelayCommand(o => CurrentView = new ProductosViewModel());
            ShowVentasCommand = new RelayCommand(o => CurrentView = new VentasViewModel());

            // Vista inicial
           CurrentView = new ClientesViewModel();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}