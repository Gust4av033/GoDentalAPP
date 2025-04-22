using GoDentalAPP.Core.Entities;
using GoDentalAPP.src.GoDentalAPP.CORE.Configuration;
using GoDentalAPP.src.GoDentalAPP.CORE.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GoDentalAPP.src.GoDentalAPP.INFRAESTRUCTURE.Services
{
    public class PdfService : IPdfService
    {
        private readonly HttpClient _httpClient;
        private readonly DteSettings _settings;

        public PdfService(IOptions<DteSettings> settings)
        {
            _settings = settings.Value;
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_settings.ApiUrl) // Puede ser distinta si es otra API
            };
        }

        public async Task<string> GenerarPdfFactura(Factura factura)
        {
            var json = JsonSerializer.Serialize(factura);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("generarpdf", content); // Ruta según tu API

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al generar PDF: {error}");
            }

            // Supongamos que la API devuelve una URL o base64 del PDF
            var pdfUrl = await response.Content.ReadAsStringAsync();
            return pdfUrl;
        }
    }
}
