using GoDentalAPP.Helpers;
using GoDentalAPP.src.GoDentalAPP.CORE.Interfaces;
using GoDentalAPP.src.GoDentalAPP.INFRAESTRUCTURE.Repositorios;
using MvvmHelpers;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using GoDentalAPP.src.GoDentalAPP.CORE.Entities;

namespace GoDentalAPP.ViewModels
{
    public class FacturacionViewModel : BaseViewModel
    {

        private bool _modoOffline;
        public bool ModoOffline{
        get => _modoOffline;
        set
        {
            SetProperty(ref _modoOffline, value);
            ActualizarEstadoConexion();
        }
    }
        // Repositorios y servicios
        private readonly IFacturacionService _facturacionService;
        private readonly IClienteRepository _clienteRepository;
        private readonly IInsumoRepository _insumoRepository;

        // Colecciones
        public ObservableCollection<Cliente> Clientes { get; set; }
        public ObservableCollection<InsumoDental> Insumos { get; set; }
        public ObservableCollection<DetalleFacturaDto> Detalles { get; set; }
        public ObservableCollection<Factura> Facturas { get; set; }
        


        // Factura actual

        public FacturaDto FacturaActual { get; set; }

        // Comandos
        public ICommand AgregarDetalleCommand { get; }
        public ICommand GenerarFacturaCommand { get; }
        public ICommand GenerarCreditoFiscalCommand { get; }
        public ICommand BuscarFacturasCommand { get; }
        public ICommand LimpiarFiltrosCommand { get; }
        public ICommand VerFacturaCommand { get; }
        public ICommand GenerarPdfCommand { get; }
        public ICommand SincronizarCommand { get; }

        private Cliente _clienteSeleccionadoFiltro;
        public Cliente ClienteSeleccionadoFiltro
        {
            get => _clienteSeleccionadoFiltro;
            set => SetProperty(ref _clienteSeleccionadoFiltro, value);
        }


        // Constructor
        public FacturacionViewModel(
            IFacturacionService facturacionService,
            IClienteRepository clienteRepository,
            IInsumoRepository insumoRepository)
        {
            _facturacionService = facturacionService;
            _clienteRepository = clienteRepository;
            _insumoRepository = insumoRepository;

            Clientes = new ObservableCollection<Cliente>();
            Insumos = new ObservableCollection<InsumoDental>();
            Detalles = new ObservableCollection<DetalleFacturaDto>();
            FacturaActual = new FacturaDto();

            AgregarDetalleCommand = new RelayCommand(AgregarDetalle);
            GenerarFacturaCommand = new RelayCommand(async () => await GenerarFactura());
            GenerarCreditoFiscalCommand = new RelayCommand(async () => await GenerarCreditoFiscal());

            CargarDatosIniciales();
        }

        // Cargar clientes e insumos
        private async void CargarDatosIniciales()
        {
            var clientes = await _clienteRepository.ObtenerTodos();
            var insumos = await _insumoRepository.GetInsumosDentalesAsync();

            Clientes.Clear();
            foreach (var cliente in clientes)
            {
                Clientes.Add((Cliente)cliente);
            }

            Insumos.Clear();
            foreach (var insumo in insumos)
            {
                Insumos.Add(insumo);
            }
        }

        // Agregar detalle a la factura
        private void AgregarDetalle()
        {
            // Lógica para agregar detalles (usando InsumoId en lugar de ProductoId)
        }

        // Generar factura consumidor final
        private async Task GenerarFactura()
        {
            var factura = await _facturacionService.CrearFacturaNormal(FacturaActual);
            // Mostrar resultado al usuario
        }

        // Generar factura crédito fiscal
        private async Task GenerarCreditoFiscal()
        {
            var factura = await _facturacionService.CrearCreditoFiscal(FacturaActual);
            // Mostrar resultado al usuario
        }

        // Método para actualizar el estado de conexión
        private void ActualizarEstadoConexion()
        {
            if (ModoOffline)
            {
                // Implementar lógica para modo offline
            }
            else
            {
                // Implementar lógica para modo online
            }
        }

    }
}
