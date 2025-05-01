using GoDentalAPP.src.GoDentalAPP.CORE.Configuration;
using GoDentalAPP.src.GoDentalAPP.CORE.Entities;
using GoDentalAPP.src.GoDentalAPP.CORE.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.IO;
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
        private readonly bool _isOnlineMode;

        public PdfService(IOptions<DteSettings> settings)
        {
            _settings = settings?.Value;
            _isOnlineMode = _settings != null && !string.IsNullOrEmpty(_settings.ApiUrl);

            if (_isOnlineMode)
            {
                try
                {
                    _httpClient = new HttpClient
                    {
                        BaseAddress = new Uri(_settings.ApiUrl)
                    };
                }
                catch (Exception)
                {
                    _isOnlineMode = false;
                    _httpClient = null;
                }
            }
        }

        public async Task<string> GenerarPdfFactura(Factura factura)
        {
            if (_isOnlineMode)
            {
                try
                {
                    // Intenta generar el PDF a través de la API
                    return await GenerarPdfFacturaOnline(factura);
                }
                catch (Exception ex)
                {
                    // Si falla, cae al modo offline
                    Console.WriteLine($"Error al generar PDF online: {ex.Message}. Cambiando a modo offline.");
                    return GenerarPdfFacturaOffline(factura);
                }
            }
            else
            {
                // Modo offline desde el principio
                return GenerarPdfFacturaOffline(factura);
            }
        }

        private async Task<string> GenerarPdfFacturaOnline(Factura factura)
        {
            var json = JsonSerializer.Serialize(factura);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("generarpdf", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al generar PDF online: {error}");
            }

            var pdfUrl = await response.Content.ReadAsStringAsync();
            return pdfUrl;
        }

        private string GenerarPdfFacturaOffline(Factura factura)
        {
            // Implementa aquí la lógica para generar el PDF localmente
            // Aquí deberías usar la plantilla local para generar el PDF

            try
            {
                // Ejemplo: utilizando la ruta a una plantilla local y generando un PDF
                string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "FacturaTemplate.html");

                if (!File.Exists(templatePath))
                {
                    throw new FileNotFoundException("No se encontró la plantilla de factura", templatePath);
                }

                // Leer la plantilla
                string template = File.ReadAllText(templatePath);

                // Rellenar la plantilla con los datos de la factura
               // string filledTemplate = RellenarPlantilla(template, factura);

                // Guardar el PDF en una ubicación temporal
                string outputPath = Path.Combine(Path.GetTempPath(), $"Factura_{factura.NumeroFactura}_{DateTime.Now:yyyyMMddHHmmss}.pdf");

                // Aquí usarías alguna biblioteca para convertir HTML a PDF
                // Por ejemplo, usando iText, PdfSharp, etc.
               // ConvertirHtmlAPdf(filledTemplate, outputPath);

                return outputPath; // Devuelve la ruta del archivo generado
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al generar PDF offline: {ex.Message}", ex);
            }
        }

        /*private string RellenarPlantilla(string template, Factura factura)
        {
            // Sustituir los marcadores en la plantilla con los datos de la factura
            // Este es un ejemplo simple, puedes adaptarlo según tu estructura de plantilla
            return template
                .Replace("{{NumeroFactura}}", factura.NumeroFactura)
                .Replace("{{FechaEmision}}", factura.FechaEmision.ToString("dd/MM/yyyy"))
                .Replace("{{NombreCliente}}", factura.Cliente.Nombre)
                .Replace("{{DireccionCliente}}", factura.Cliente.Direccion)
                // Continúa con todos los campos necesarios
                ;
        }*/

        private void ConvertirHtmlAPdf(string html, string outputPath)
        {
            // Implementa aquí la conversión de HTML a PDF
            // Puedes usar bibliotecas como iText, PdfSharp, etc.

            // Ejemplo conceptual (necesitarás implementarlo con una biblioteca real):
            // HtmlToPdfConverter converter = new HtmlToPdfConverter();
            // converter.ConvertHtmlToPdf(html, outputPath);

            // Para fines de este ejemplo, simularemos que el archivo se creó correctamente
            // guardando el HTML como un archivo temporal
            File.WriteAllText(outputPath.Replace(".pdf", ".html"), html);

            // NOTA: Esta es solo una simulación. En una implementación real,
            // necesitarás usar una biblioteca para generar PDFs a partir de HTML
        }
    }
}