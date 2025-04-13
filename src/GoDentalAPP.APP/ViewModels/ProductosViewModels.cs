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
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        private ObservableCollection<InsumoDental> _productosOriginal;


        public ICommand MostrarProductosCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand AgregarCommand { get; }
        public ICommand EditarCommand { get; }
        public ICommand EliminarCommand { get; }
        public ICommand BuscarCommand { get; }
        public ICommand GuardarCommand { get; }
        public ICommand AtrasCommand { get; }
        public ICommand ExportarCommand { get; }

        public ProductosViewModel()
        {
            // Inicializa el repositorio
            _insumoRepository = new InsumoRepository(new AppDbContext());
            ExportarCommand = new RelayCommand(o => ExportarAExcel());
            AgregarCommand = new RelayCommand(AgregarProducto);
            EditarCommand = new RelayCommand(EditarProducto);
            EliminarCommand = new RelayCommand(EliminarProducto);
            BuscarCommand = new RelayCommand(BuscarProducto);
            GuardarCommand = new RelayCommand(GuardarCambios);
            AtrasCommand = new RelayCommand(Atras);

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
                TotalInventario = productos.Sum(p => p.PrecioUnitario * p.CantidadEnStock);
                _productosOriginal = new ObservableCollection<InsumoDental>(productos);
                Productos = new ObservableCollection<InsumoDental>(_productosOriginal);

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

        private void FiltrarProductos()
        {
            if (string.IsNullOrWhiteSpace(TextoBusqueda))
            {
                Productos = new ObservableCollection<InsumoDental>(_productosOriginal);
            }
            else
            {
                var filtrados = _productosOriginal
                    .Where(p => p.NombreInsumo.Contains(TextoBusqueda, StringComparison.OrdinalIgnoreCase)
                             || p.Descripcion.Contains(TextoBusqueda, StringComparison.OrdinalIgnoreCase))
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
            worksheet.Cell(1, 2).Value = "Descripción";
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
            System.Diagnostics.Process.Start("explorer.exe", ruta); // abre el archivo automáticamente
        }


        private void AgregarProducto(object obj) { /* abrir modal, por ejemplo */ }
        private void EditarProducto(object obj) { /* obtener seleccionado y editar */ }
        private void EliminarProducto(object obj) { /* eliminar seleccionado */ }
        private void BuscarProducto(object obj) { /* abrir búsqueda o filtrar */ }
        private void GuardarCambios(object obj) { /* guardar al repositorio */ }
        private void Atras(object obj) { /* navegar hacia atrás */ }
    }
}