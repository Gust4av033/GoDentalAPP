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
using ClosedXML.Excel;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.Input;


namespace GoDentalAPP.ViewModels
{
    public class ClientesViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IEstadoRepository _estadoRepository;
        private readonly ITipoDocumentoRepository _tipoDocumentoRepository;

        private string _textoBusqueda;
        private Cliente _clienteSeleccionado;
        private Cliente _clienteDetalles;
        private bool _mostrarDetalles = false;
        private bool _mostrarFormulario = false;
        private bool _esNuevoCliente;

        public ObservableCollection<Cliente> Clientes { get; } = new ObservableCollection<Cliente>();

        private ObservableCollection<Estado> _estados;
        public ObservableCollection<Estado> Estados
        {
            get => _estados;
            set
            {
                _estados = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(NombreEstadoSeleccionado));
            }
        }

        private ObservableCollection<TipoDocumento> _tiposDocumento;
        public ObservableCollection<TipoDocumento> TiposDocumento
        {
            get => _tiposDocumento;
            set
            {
                _tiposDocumento = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(NombreTipoDocumentoSeleccionado));
            }
        }

        // Propiedades calculadas para mostrar nombres
        public string NombreEstadoSeleccionado => ClienteDetalles?.Estado?.NombreEstado ?? "Sin estado";
        public string NombreTipoDocumentoSeleccionado => ClienteDetalles?.TipoDocumento?.Nombre ?? "Sin tipo";

        public string TituloFormulario => EsNuevoCliente ? "Nuevo Cliente" : "Editar Cliente";
        public bool TieneClienteSeleccionado => ClienteSeleccionado != null;

        public string TextoBusqueda
        {
            get => _textoBusqueda;
            set { _textoBusqueda = value; OnPropertyChanged(); }
        }

        public Cliente ClienteSeleccionado
        {
            get => _clienteSeleccionado;
            set
            {
                if (SetProperty(ref _clienteSeleccionado, value))
                {
                    if (_clienteSeleccionado != null)
                    {
                        ClienteDetalles = _clienteSeleccionado;
                        MostrarDetalles = true;
                    }
                    OnPropertyChanged(nameof(TieneClienteSeleccionado));
                }
            }
        }


        public Cliente ClienteDetalles
        {
            get => _clienteDetalles;
            set
            {
                SetProperty(ref _clienteDetalles, value);
                OnPropertyChanged(nameof(NombreEstadoSeleccionado));
                OnPropertyChanged(nameof(NombreTipoDocumentoSeleccionado));
            }
        }

        public bool MostrarDetalles
        {
            get => _mostrarDetalles;
            set { _mostrarDetalles = value; OnPropertyChanged(); }
        }

        public bool EsNuevoCliente
        {
            get => _esNuevoCliente;
            set => SetProperty(ref _esNuevoCliente, value);
        }

        public bool MostrarFormulario
        {
            get => _mostrarFormulario;
            set { _mostrarFormulario = value; OnPropertyChanged(); }
        }

        public ICommand BuscarCommand { get; }
        public ICommand AgregarCommand { get; }
        public ICommand EditarCommand { get; }
        public ICommand EliminarCommand { get; }
        public ICommand DesactivarCommand { get; }
        public IAsyncRelayCommand<Cliente> VerDetallesCommand { get; }
        VerDetallesCommand = new AsyncRelayCommand<Cliente>(VerDetalles);
        public ICommand CerrarDetallesCommand { get; }
        public ICommand ExportarCommand { get; }
        public ICommand GuardarCommand { get; }
        public ICommand CancelarCommand { get; }
        public ICommand AbrirMapaCommand { get; }
        public ICommand AbrirLinkCommand { get; }

        public ClientesViewModel(
            IClienteRepository clienteRepository,
            IEstadoRepository estadoRepository,
            ITipoDocumentoRepository tipoDocumentoRepository)
        {
            _clienteRepository = clienteRepository;
            _estadoRepository = estadoRepository;
            _tipoDocumentoRepository = tipoDocumentoRepository;

            // Inicializar colecciones
            Estados = new ObservableCollection<Estado>();
            TiposDocumento = new ObservableCollection<TipoDocumento>();

            // Inicializar comandos
            BuscarCommand = new RelayCommand(async (param) => await BuscarClientes());
            AgregarCommand = new RelayCommand((param) => PrepararNuevoCliente());
            EditarCommand = new RelayCommand(async (param) => await EditarCliente(), (param) => ClienteSeleccionado != null);
            EliminarCommand = new RelayCommand(async (param) => await EliminarCliente(), (param) => ClienteSeleccionado != null);
            DesactivarCommand = new RelayCommand(async (param) => await DesactivarCliente(), (param) => ClienteSeleccionado != null);
            VerDetallesCommand = new RelayCommand<Cliente>(VerDetalles);
            CerrarDetallesCommand = new RelayCommand((param) => MostrarDetalles = false);
            ExportarCommand = new RelayCommand(async (param) => await ExportarClientes());
            GuardarCommand = new RelayCommand(async (param) => await GuardarCliente());
            CancelarCommand = new RelayCommand((param) => CancelarEdicion());
            AbrirMapaCommand = new RelayCommand(parameter => AbrirMapa(parameter as Cliente));
            AbrirLinkCommand = new RelayCommand(parameter => AbrirLink(parameter as string));

            // Cargar datos iniciales
            Task.Run(async () =>
            {
                await CargarEstados();
                await CargarTiposDocumento();
                await CargarClientes();
            }).ConfigureAwait(false);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task CargarEstados()
        {
            try
            {
                var estados = await _estadoRepository.GetAllAsync();
                Estados = new ObservableCollection<Estado>(estados.DistinctBy(e => e.EstadoID));
                OnPropertyChanged(nameof(Estados));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar estados: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async Task CargarTiposDocumento()
        {
            try
            {
                var tipos = await _tipoDocumentoRepository.GetAllAsync();
                TiposDocumento = new ObservableCollection<TipoDocumento>(tipos.DistinctBy(t => t.TipoDocumentoID));
                OnPropertyChanged(nameof(TiposDocumento));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar tipos de documento: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async Task CargarClientes()
        {
            try
            {
                IsBusy = true;
                var clientes = await _clienteRepository.GetClientesActivosAsync();

                Application.Current.Dispatcher.Invoke(() =>
                {
                    Clientes.Clear();
                    foreach (var cliente in clientes)
                    {
                        Clientes.Add(cliente);
                    }
                });
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


        public async Task CargarDetallesClienteAsync(int clienteId)
        {
            try
            {
                var cliente = await _clienteRepository.GetClienteDetallesAsync(clienteId);
                if (cliente != null)
                {
                    ClienteDetalles = cliente;
                    OnPropertyChanged(nameof(NombreEstadoSeleccionado));
                    OnPropertyChanged(nameof(NombreTipoDocumentoSeleccionado));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al cargar detalles del cliente {clienteId}: {ex.Message}");
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

        private void PrepararNuevoCliente()
        {
            ClienteDetalles = new Cliente
            {
                FechaRegistro = DateTime.Now,
                EstadoID = Estados.FirstOrDefault()?.EstadoID ?? 1, // Estado activo por defecto
                TiposDocumentoID = TiposDocumento.FirstOrDefault()?.TipoDocumentoID
            };
            EsNuevoCliente = true;
            MostrarFormulario = true;
            OnPropertyChanged(nameof(TituloFormulario));
        }


        private async Task GuardarCliente()
        {
            try
            {
                if (EsNuevoCliente)
                {
                    await _clienteRepository.CreateClienteAsync(ClienteDetalles);
                    MessageBox.Show("Cliente agregado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    await _clienteRepository.UpdateClienteAsync(ClienteDetalles.ClienteID, ClienteDetalles);
                    MessageBox.Show("Cliente actualizado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                await CargarClientes();
                MostrarFormulario = false;
                ClienteDetalles = null;
                EsNuevoCliente = false;
                OnPropertyChanged(nameof(TituloFormulario));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar cliente: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void CancelarEdicion()
        {
            MostrarFormulario = false;
            ClienteDetalles = null;
            EsNuevoCliente = false;
            OnPropertyChanged(nameof(TituloFormulario));
        }


        public async Task VerDetalles(Cliente cliente)
        {
            try
            {
                if (cliente != null)
                {
                    // Asegura que las colecciones estén cargadas
                    if (Estados == null || Estados.Count == 0)
                        await CargarEstados();
                    if (TiposDocumento == null || TiposDocumento.Count == 0)
                        await CargarTiposDocumento();

                    // Carga el cliente completo desde la base de datos
                    await CargarDetallesClienteAsync(cliente.ClienteID);

                    MostrarDetalles = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al mostrar detalles: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private async Task EditarCliente()
        {
            try
            {
                if (ClienteSeleccionado != null)
                {
                    // Usar el cliente seleccionado directamente
                    ClienteDetalles = ClienteSeleccionado;

                    // Asegurar que las colecciones estén cargadas
                    if (Estados == null || Estados.Count == 0) await CargarEstados();
                    if (TiposDocumento == null || TiposDocumento.Count == 0) await CargarTiposDocumento();

                    EsNuevoCliente = false;
                    MostrarFormulario = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al preparar edición: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                var saveFileDialog = new Microsoft.Win32.SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx",
                    FileName = $"Clientes_{DateTime.Now:yyyyMMddHHmmss}.xlsx"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Clientes");

                        // Encabezados
                        worksheet.Cell(1, 1).Value = "Nombre";
                        worksheet.Cell(1, 2).Value = "Teléfono";
                        worksheet.Cell(1, 3).Value = "Correo";
                        worksheet.Cell(1, 4).Value = "Dirección";
                        worksheet.Cell(1, 5).Value = "NIT";
                        worksheet.Cell(1, 6).Value = "NRC";
                        worksheet.Cell(1, 7).Value = "Tipo Documento";
                        worksheet.Cell(1, 8).Value = "Número Documento";
                        worksheet.Cell(1, 9).Value = "Estado";

                        // Datos
                        int row = 2;
                        foreach (var cliente in Clientes)
                        {
                            worksheet.Cell(row, 1).Value = cliente.NombreCompleto;
                            worksheet.Cell(row, 2).Value = cliente.Telefono;
                            worksheet.Cell(row, 3).Value = cliente.CorreoElectronico;
                            worksheet.Cell(row, 4).Value = cliente.Direccion;
                            worksheet.Cell(row, 5).Value = cliente.NIT;
                            worksheet.Cell(row, 6).Value = cliente.NRC;
                            worksheet.Cell(row, 7).Value = cliente.TipoDocumento?.Nombre ?? "N/A";
                            worksheet.Cell(row, 8).Value = cliente.NumeroDocumento;
                            worksheet.Cell(row, 9).Value = cliente.Estado?.NombreEstado ?? "N/A";
                            row++;
                        }

                        workbook.SaveAs(saveFileDialog.FileName);
                    }

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

        public void AbrirMapa(Cliente cliente)
        {
            if (cliente == null)
            {
                MessageBox.Show("No hay cliente seleccionado.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Si hay un link directo, úsalo
            if (!string.IsNullOrWhiteSpace(cliente.LinkDireccion))
            {
                try
                {
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {cliente.LinkDireccion}") { CreateNoWindow = true });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al abrir el enlace de dirección: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                return;
            }

            // Si no hay link, usa la dirección para buscar en Google Maps
            if (!string.IsNullOrWhiteSpace(cliente.Direccion))
            {
                string direccionUrl = Uri.EscapeDataString(cliente.Direccion);
                string url = $"https://www.google.com/maps/search/?api=1&query={direccionUrl}";
                try
                {
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al abrir Google Maps: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                return;
            }

            MessageBox.Show("El cliente no tiene dirección registrada.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
        }


        private void AbrirLink(string url)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(url))
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = url,
                        UseShellExecute = true
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir enlace: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

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