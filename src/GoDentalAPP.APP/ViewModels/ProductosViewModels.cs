using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using GoDentalAPP.Helpers;
using GoDentalAPP.Infrastructure.Persistence;
using System.Threading.Tasks;
using System;
using GoDentalAPP.src.GoDentalAPP.CORE.Entities;
using GoDentalAPP.src.GoDentalAPP.INFRAESTRUCTURE.Repositorios;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Windows;
using GoDentalAPP.src.GoDentalAPP.APP.Views.ViewsProducto;

namespace GoDentalAPP.ViewModels
{
    public class ProductosViewModel : INotifyPropertyChanged
    {
        private readonly IInsumoRepository _insumoRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IProveedorRepository _proveedorRepository;


        private ObservableCollection<InsumoDental> _productos;
        private ObservableCollection<InsumoDental> _productosOriginal;
        private ObservableCollection<Categoria> _categorias;
        private ObservableCollection<Proveedor> _proveedores;
        private bool _isLoading;



        public ObservableCollection<InsumoDental> Productos
        {
            get => _productos;
            set { _productos = value; OnPropertyChanged(); }
        }

        private InsumoDental _insumoSeleccionado;
        public InsumoDental InsumoSeleccionado
        {
            get => _insumoSeleccionado;
            set { _insumoSeleccionado = value; OnPropertyChanged(); }
        }


        public ObservableCollection<Categoria> Categorias
        {
            get => _categorias;
            set { _categorias = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Proveedor> Proveedores
        {
            get => _proveedores;
            set { _proveedores = value; OnPropertyChanged(); }
        }

        private string _precioUnitarioTexto;
        public string PrecioUnitarioTexto
        {
            get => _precioUnitarioTexto;
            set
            {
                _precioUnitarioTexto = value;
                OnPropertyChanged();

                if (decimal.TryParse(value, out var result))
                {
                    NuevoInsumo.PrecioUnitario = result;
                }
            }
        }


        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }

        public InsumoDental NuevoInsumo { get; set; }

        private decimal _totalInventario;
        public decimal TotalInventario
        {
            get => _totalInventario;
            set { _totalInventario = value; OnPropertyChanged(); }
        }

        private string _textoBusqueda;
        public string TextoBusqueda
        {
            get => _textoBusqueda;
            set
            {
                _textoBusqueda = value;
                OnPropertyChanged();
                FiltrarProductos(); // se llama cada vez que se cambia el texto
            }
        }


        public ICommand MostrarProductosCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand AgregarCommand { get; }
        public ICommand EditarCommand { get; }
        public ICommand EliminarCommand { get; }
        public ICommand BuscarCommand { get; }
        public ICommand AtrasCommand { get; }
        public ICommand ExportarCommand { get; }
        public ICommand GuardarCommand { get; }


        public ProductosViewModel(IInsumoRepository insumoRepository, ICategoriaRepository categoriaRepository,
            IProveedorRepository proveedorRepository)
        {
            _insumoRepository = insumoRepository;
            _categoriaRepository = categoriaRepository;
            _proveedorRepository = proveedorRepository;


            NuevoInsumo = new InsumoDental();
            Productos = new ObservableCollection<InsumoDental>();
            _productosOriginal = new ObservableCollection<InsumoDental>();
            Categorias = new ObservableCollection<Categoria>();
            Proveedores = new ObservableCollection<Proveedor>();

            MostrarProductosCommand = new RelayCommand(async _ => await MostrarProductosAsync());
            RefreshCommand = new RelayCommand(async _ => await MostrarProductosAsync());
            ExportarCommand = new RelayCommand(_ => ExportarAExcel());
            AgregarCommand = new RelayCommand(_ => AbrirVentanaAgregar());
            EditarCommand = new RelayCommand(_ => AbrirVentanaEditar(InsumoSeleccionado));
            EliminarCommand = new RelayCommand(_ => EliminarProducto(InsumoSeleccionado));
            BuscarCommand = new RelayCommand(_ => BuscarProducto());
            GuardarCommand = new RelayCommand(async _ => await GuardarNuevoInsumoAsync());
            AtrasCommand = new RelayCommand(_ => Atras());

            // Carga inicial
            _ = MostrarProductosAsync();
        }

        private async Task GuardarNuevoInsumoAsync()
        {
            try
            {
                if (NuevoInsumo.InsumoID == 0)
                {
                    // Agregar nuevo insumo
                    await _insumoRepository.CreateInsumoDentalAsync(NuevoInsumo);
                    MessageBox.Show("Insumo agregado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // Editar insumo existente
                    await _insumoRepository.UpdateInsumoDentalAsync(NuevoInsumo.InsumoID, NuevoInsumo);
                    MessageBox.Show("Insumo editado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                await MostrarProductosAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar insumo: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task MostrarProductosAsync()
        {
            try
            {
                IsLoading = true;

                // Cargar productos, categorías y proveedores en paralelo
                var t1 = _insumoRepository.GetInsumosDentalesAsync();
                var t2 = _categoriaRepository.GetAllAsync();
                var t3 = _proveedorRepository.GetAllAsync();

                await Task.WhenAll(t1, t2, t3);

                Application.Current.Dispatcher.Invoke(() =>
                {
                    // Productos
                    var productos = t1.Result;
                    Productos = new ObservableCollection<InsumoDental>(productos);
                    _productosOriginal = new ObservableCollection<InsumoDental>(productos);
                    TotalInventario = productos.Sum(p => p.PrecioUnitario * p.CantidadEnStock);

                    // Categorías
                    Categorias = new ObservableCollection<Categoria>(t2.Result);

                    // Proveedores
                    Proveedores = new ObservableCollection<Proveedor>(t3.Result);
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos iniciales: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void FiltrarProductos()
        {
            if (string.IsNullOrWhiteSpace(TextoBusqueda))
            {
                Productos = new ObservableCollection<InsumoDental>(_productosOriginal);
            }
            else
            {
                var filtrados = _productosOriginal
                    .Where(p => p.NombreInsumo.Contains(TextoBusqueda, StringComparison.OrdinalIgnoreCase) ||
                               p.Descripcion?.Contains(TextoBusqueda, StringComparison.OrdinalIgnoreCase) == true ||
                               p.Categoria?.NombreCategoria?.Contains(TextoBusqueda, StringComparison.OrdinalIgnoreCase) == true)
                    .ToList();

                Productos = new ObservableCollection<InsumoDental>(filtrados);
            }
        }

        public void ExportarAExcel()
        {
            using var workbook = new ClosedXML.Excel.XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Productos");

            // Encabezados
            worksheet.Cell(1, 1).Value = "Nombre";
            worksheet.Cell(1, 2).Value = "Descripci�n";
            worksheet.Cell(1, 3).Value = "Precio";
            worksheet.Cell(1, 4).Value = "Stock";

            for (int i = 0; i < Productos.Count; i++)
            {
                var p = Productos[i];
                worksheet.Cell(i + 2, 1).Value = p.NombreInsumo;
                worksheet.Cell(i + 2, 2).Value = p.Descripcion;
                worksheet.Cell(i + 2, 3).Value = p.PrecioUnitario;
                worksheet.Cell(i + 2, 4).Value = p.CantidadEnStock;
            }

            var ruta = "ProductosExportados.xlsx";
            workbook.SaveAs(ruta);
            System.Diagnostics.Process.Start("explorer.exe", ruta); // abre el archivo autom�ticamente
        }

        /*private void AgregarProducto()
{
    var agregarInsumoView = new AgregarInsumoView
    {
        DataContext = this // Pasamos el mismo ViewModel
    };

    var window = new Window
    {
        Content = agregarInsumoView,
        Title = "Agregar Insumo Dental",
        SizeToContent = SizeToContent.WidthAndHeight,
        WindowStartupLocation = WindowStartupLocation.CenterScreen
    };

    window.ShowDialog();
}*/

        
        private async void EliminarProducto(InsumoDental insumoSeleccionado)
        {
            if (Productos.Count == 0 || Productos == null)
            {
                MessageBox.Show("No hay productos seleccionados para eliminar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var productoSeleccionado = InsumoSeleccionado; // Selección simulada
                if (productoSeleccionado == null) return;

                var confirmacion = MessageBox.Show($"¿Está seguro de eliminar el producto '{productoSeleccionado.NombreInsumo}'?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (confirmacion == MessageBoxResult.Yes)
                {
                    await _insumoRepository.DeleteInsumoDentalAsync(productoSeleccionado.InsumoID);
                    MessageBox.Show("Producto eliminado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    await MostrarProductosAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar producto: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AbrirVentanaAgregar()
        {
            NuevoInsumo = new InsumoDental(); // Crear un nuevo insumo vacío
            AbrirVentanaInsumo();
        }

        private void AbrirVentanaEditar(InsumoDental insumoSeleccionado)
        {
            if (insumoSeleccionado == null)
            {
                MessageBox.Show("Seleccione un insumo para editar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            NuevoInsumo = insumoSeleccionado; // Cargar los datos del insumo seleccionado
            PrecioUnitarioTexto = insumoSeleccionado.PrecioUnitario.ToString("F2"); // Formatear el precio
            AbrirVentanaInsumo();
        }

        private void AbrirVentanaInsumo()
        {
            var agregarInsumoView = new AgregarInsumoView
            {
                DataContext = this // Pasamos el mismo ViewModel
            };

            var window = new Window
            {
                Content = agregarInsumoView,
                Title = NuevoInsumo.InsumoID == 0 ? "Agregar Insumo Dental" : "Editar Insumo Dental",
                SizeToContent = SizeToContent.WidthAndHeight,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            window.ShowDialog();
        }




        private void BuscarProducto() { /* implementación */ }
        private void GuardarCambios() { /* implementación */ }
        private void Atras() { /* implementación */ }
    }
}