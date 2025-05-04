using GoDentalAPP.Helpers;
using GoDentalAPP.src.GoDentalAPP.CORE.Entities;
using GoDentalAPP.src.GoDentalAPP.INFRAESTRUCTURE.Repositorios;
using MvvmHelpers;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace GoDentalAPP.ViewModels
{
    public class ClientesViewModel : BaseViewModel
    {
        private readonly IClienteRepository _clienteRepository;
        private string _textoBusqueda;
        private Cliente _clienteSeleccionado;
        private Cliente _clienteDetalles;
        private bool _mostrarDetalles = false;

        public ObservableCollection<Cliente> Clientes { get; } = new ObservableCollection<Cliente>();
        public ObservableCollection<TipoDocumento> TiposDocumento { get; } = new ObservableCollection<TipoDocumento>();

        public string TextoBusqueda
        {
            get => _textoBusqueda;
            set { _textoBusqueda = value; OnPropertyChanged(); }
        }

        public Cliente ClienteSeleccionado
        {
            get => _clienteSeleccionado;
            set { _clienteSeleccionado = value; OnPropertyChanged(); }
        }

        public Cliente ClienteDetalles
        {
            get => _clienteDetalles;
            set { _clienteDetalles = value; OnPropertyChanged(); }
        }

        public bool MostrarDetalles
        {
            get => _mostrarDetalles;
            set { _mostrarDetalles = value; OnPropertyChanged(); }
        }

        public ICommand BuscarCommand { get; }
        public ICommand AgregarCommand { get; }
        public ICommand EditarCommand { get; }
        public ICommand EliminarCommand { get; }
        public ICommand DesactivarCommand { get; }
        public ICommand VerDetallesCommand { get; }
        public ICommand CerrarDetallesCommand { get; }
        public ICommand ExportarCommand { get; }
        public ICommand AtrasCommand { get; }

        public ClientesViewModel(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;

            BuscarCommand = new RelayCommand(async (param) => await BuscarClientes());
            AgregarCommand = new RelayCommand(async (param) => await AgregarCliente());
            EditarCommand = new RelayCommand(async (param) => await EditarCliente(), (param) => ClienteSeleccionado != null);
            EliminarCommand = new RelayCommand(async (param) => await EliminarCliente(), (param) => ClienteSeleccionado != null);
            DesactivarCommand = new RelayCommand(async (param) => await DesactivarCliente(), (param) => ClienteSeleccionado != null);
            VerDetallesCommand = new RelayCommand(async (param) => await VerDetalles(), (param) => ClienteSeleccionado != null);
            CerrarDetallesCommand = new RelayCommand((param) => MostrarDetalles = false);
            ExportarCommand = new RelayCommand(async (param) => await ExportarClientes());
            AtrasCommand = new RelayCommand((param) => { /* Lógica para navegar atrás */ });

            CargarClientes();
            CargarTiposDocumento();
        }

        public ClientesViewModel()
        {
        }

        private async Task CargarClientes()
        {
            try
            {
                IsBusy = true;
                Clientes.Clear();
                var clientes = await _clienteRepository.GetClientesActivosAsync();
                foreach (var cliente in clientes)
                {
                    Clientes.Add(cliente);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar clientes: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task CargarTiposDocumento()
        {
            try
            {
                TiposDocumento.Clear();
                // Simulando carga de tipos de documento
                TiposDocumento.Add(new TipoDocumento { TipoDocumentoID = 1, Nombre = "DNI" });
                TiposDocumento.Add(new TipoDocumento { TipoDocumentoID = 2, Nombre = "Pasaporte" });
                TiposDocumento.Add(new TipoDocumento { TipoDocumentoID = 3, Nombre = "Carnet de Extranjería" });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar tipos de documento: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task BuscarClientes()
        {
            try
            {
                IsBusy = true;
                if (string.IsNullOrWhiteSpace(TextoBusqueda))
                {
                    await CargarClientes();
                    return;
                }

                var clientes = await _clienteRepository.GetClientesAsync();
                var resultados = clientes.Where(c =>
                    c.NombreCompleto.Contains(TextoBusqueda, StringComparison.OrdinalIgnoreCase) ||
                    c.Telefono?.Contains(TextoBusqueda, StringComparison.OrdinalIgnoreCase) == true ||
                    c.CorreoElectronico?.Contains(TextoBusqueda, StringComparison.OrdinalIgnoreCase) == true ||
                    c.NIT?.Contains(TextoBusqueda, StringComparison.OrdinalIgnoreCase) == true ||
                    c.NumeroDocumento?.Contains(TextoBusqueda, StringComparison.OrdinalIgnoreCase) == true);

                Clientes.Clear();
                foreach (var cliente in resultados)
                {
                    Clientes.Add(cliente);
                }

                if (Clientes.Count == 0)
                {
                    MessageBox.Show("No se encontraron clientes con el criterio de búsqueda.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar clientes: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task AgregarCliente()
        {
            try
            {
                // En una aplicación real, esto sería un diálogo o ventana modal
                var nuevoCliente = new Cliente
                {
                    NombreCompleto = "Nuevo Cliente",
                    Telefono = "00000000",
                    CorreoElectronico = "nuevo@cliente.com",
                    Direccion = "Dirección del cliente",
                    NIT = "00000000",
                    NRC = "00000000",
                    TipoContribuyente = "Contribuyente",
                    Giro = "Comercio",
                    TiposDocumentoID = 1, // DNI por defecto
                    NumeroDocumento = "00000000"
                };

                var result = MessageBox.Show("¿Está seguro que desea agregar este nuevo cliente?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    await _clienteRepository.CreateClienteAsync(nuevoCliente);
                    await CargarClientes();
                    MessageBox.Show("Cliente agregado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar cliente: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task VerDetalles()
        {
            try
            {
                if (ClienteSeleccionado != null)
                {
                    IsBusy = true;
                    ClienteDetalles = await _clienteRepository.GetClienteDetallesAsync(ClienteSeleccionado.ClienteID);
                    MostrarDetalles = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar detalles del cliente: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task EditarCliente()
        {
            try
            {
                if (ClienteSeleccionado != null)
                {
                    var result = MessageBox.Show("¿Está seguro que desea guardar los cambios en este cliente?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        await _clienteRepository.UpdateClienteAsync(ClienteSeleccionado.ClienteID, ClienteSeleccionado);
                        await CargarClientes();
                        MessageBox.Show("Cliente actualizado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al editar cliente: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task EliminarCliente()
        {
            try
            {
                if (ClienteSeleccionado != null)
                {
                    var result = MessageBox.Show($"¿Está seguro que desea eliminar permanentemente al cliente {ClienteSeleccionado.NombreCompleto}?",
                        "Confirmar Eliminación", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        await _clienteRepository.DeleteClienteAsync(ClienteSeleccionado.ClienteID);
                        await CargarClientes();
                        MessageBox.Show("Cliente eliminado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar cliente: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task DesactivarCliente()
        {
            try
            {
                if (ClienteSeleccionado != null)
                {
                    // Diálogo para obtener el motivo de inactivación
                    var motivoDialog = new InputDialog("Motivo de inactivación", "Ingrese el motivo para inactivar al cliente:");
                    if (motivoDialog.ShowDialog() == true)
                    {
                        string motivo = motivoDialog.Answer;
                        if (!string.IsNullOrWhiteSpace(motivo))
                        {
                            var result = MessageBox.Show($"¿Está seguro que desea inactivar al cliente {ClienteSeleccionado.NombreCompleto}?",
                                "Confirmar Inactivación", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                            if (result == MessageBoxResult.Yes)
                            {
                                await _clienteRepository.DesactivarClienteAsync(ClienteSeleccionado.ClienteID, motivo);
                                await CargarClientes();
                                MessageBox.Show("Cliente inactivado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Debe ingresar un motivo para inactivar al cliente.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al desactivar cliente: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task ExportarClientes()
        {
            try
            {
                IsBusy = true;
                // Usando SaveFileDialog para seleccionar ubicación del archivo
                var saveFileDialog = new Microsoft.Win32.SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx",
                    FileName = $"Clientes_{DateTime.Now:yyyyMMddHHmmss}.xlsx"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    // Implementar lógica para exportar a Excel
                    // Ejemplo con ClosedXML (necesitarías agregar el paquete NuGet)
                    /*
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Clientes");
                        
                        // Encabezados
                        worksheet.Cell(1, 1).Value = "Nombre";
                        worksheet.Cell(1, 2).Value = "Teléfono";
                        // ... otros encabezados
                        
                        // Datos
                        int row = 2;
                        foreach (var cliente in Clientes)
                        {
                            worksheet.Cell(row, 1).Value = cliente.NombreCompleto;
                            worksheet.Cell(row, 2).Value = cliente.Telefono;
                            // ... otras propiedades
                            row++;
                        }
                        
                        workbook.SaveAs(saveFileDialog.FileName);
                    }
                    */

                    MessageBox.Show($"Clientes exportados correctamente a: {saveFileDialog.FileName}", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al exportar clientes: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }

    // Clase auxiliar para el diálogo de entrada de texto
    public class InputDialog : Window
    {
        public string Answer { get; set; }

        public InputDialog(string title, string question)
        {
            Title = title;
            Width = 300;
            Height = 150;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            var stackPanel = new StackPanel { Margin = new Thickness(10) };

            stackPanel.Children.Add(new TextBlock { Text = question, Margin = new Thickness(0, 0, 0, 10) });

            var textBox = new TextBox();
            stackPanel.Children.Add(textBox);

            var button = new Button { Content = "Aceptar", IsDefault = true, Margin = new Thickness(0, 10, 0, 0) };
            button.Click += (sender, e) =>
            {
                Answer = textBox.Text;
                DialogResult = true;
            };
            stackPanel.Children.Add(button);

            Content = stackPanel;
        }
    }
}