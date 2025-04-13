using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using GoDentalAPP.Core.Entities;
using GoDentalAPP.Helpers;
using GoDentalAPP.Infrastructure.Persistence;
using System.Threading.Tasks;
using System;
using GoDentalAPP.src.GoDentalAPP.INFRAESTRUCTURE.Repositorios;

namespace GoDentalAPP.ViewModels
{
    public class ProductosViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<InsumoDental> _productos;
        private readonly IInsumoRepository _insumoRepository;
        private bool _isLoading;

        public ObservableCollection<InsumoDental> Productos
        {
            get => _productos;
            set { _productos = value; OnPropertyChanged(); }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }

        public ICommand MostrarProductosCommand { get; }
        public ICommand RefreshCommand { get; }

        public ProductosViewModel()
        {
            // Inicializa el repositorio
            _insumoRepository = new InsumoRepository(new AppDbContext());

            Productos = new ObservableCollection<InsumoDental>();
            MostrarProductosCommand = new RelayCommand(async (param) => await MostrarProductosAsync());
            RefreshCommand = new RelayCommand(async (param) => await MostrarProductosAsync());

            // Carga inicial
            _ = MostrarProductosAsync();
        }

        private async Task MostrarProductosAsync()
        {
            try
            {
                IsLoading = true;

                var productos = await _insumoRepository.GetInsumosDentalesAsync();

                Productos = new ObservableCollection<InsumoDental>(productos);
            }
            catch (Exception ex)
            {
                // Manejo de errores (puedes implementar un servicio de logging)
                Console.WriteLine($"Error al cargar productos: {ex.Message}");

                // Opcional: mostrar mensaje al usuario
                Productos = new ObservableCollection<InsumoDental>
                {
                    new InsumoDental
                    {
                        NombreInsumo = "Error al cargar datos",
                        Descripcion = ex.Message
                    }
                };
            }
            finally
            {
                IsLoading = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}