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

namespace GoDentalAPP.ViewModels
{
    public class ProductosViewModel : INotifyPropertyChanged
    {
        private readonly IInsumoRepository _insumoRepository;
        private ObservableCollection<InsumoDental> _productos;
        private ObservableCollection<InsumoDental> _productosOriginal;
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
        public ICommand GuardarCommand { get; }
        public ICommand AtrasCommand { get; }
        public ICommand ExportarCommand { get; }


        public ProductosViewModel(IInsumoRepository insumoRepository)
        {
            _insumoRepository = insumoRepository;

            Productos = new ObservableCollection<InsumoDental>();
            _productosOriginal = new ObservableCollection<InsumoDental>();

            MostrarProductosCommand = new RelayCommand(async _ => await MostrarProductosAsync());
            RefreshCommand = new RelayCommand(async _ => await MostrarProductosAsync());
            ExportarCommand = new RelayCommand(_ => ExportarAExcel());
            AgregarCommand = new RelayCommand(_ => AgregarProducto());
            EditarCommand = new RelayCommand(_ => EditarProducto());
            EliminarCommand = new RelayCommand(_ => EliminarProducto());
            BuscarCommand = new RelayCommand(_ => BuscarProducto());
            GuardarCommand = new RelayCommand(_ => GuardarCambios());
            AtrasCommand = new RelayCommand(_ => Atras());

            // Carga inicial
            _ = MostrarProductosAsync();
        }


        private async Task MostrarProductosAsync()
        {
            try
            {
                IsLoading = true;
                var productos = await _insumoRepository.GetInsumosDentalesAsync();

                if (productos == null || !productos.Any())
                {
                    MessageBox.Show("No se encontraron insumos en la base de datos.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                Application.Current.Dispatcher.Invoke(() =>
                {
                    Productos = new ObservableCollection<InsumoDental>(productos);
                    _productosOriginal = new ObservableCollection<InsumoDental>(productos);
                    TotalInventario = productos.Sum(p => p.PrecioUnitario * p.CantidadEnStock);
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar productos: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                MessageBox.Show($"Ocurrió un error: {ex.Message}\n{ex.StackTrace}");
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


        private void AgregarProducto() { /* implementación */ }
        private void EditarProducto() { /* implementación */ }
        private void EliminarProducto() { /* implementación */ }
        private void BuscarProducto() { /* implementación */ }
        private void GuardarCambios() { /* implementación */ }
        private void Atras() { /* implementación */ }
    }
}