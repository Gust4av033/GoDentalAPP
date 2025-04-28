using GoDentalAPP.src.GoDentalAPP.CORE.Configuration;
using GoDentalAPP.src.GoDentalAPP.CORE.Entities;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GoDentalAPP.src.GoDentalAPP.CORE.Interfaces
{
    public class DteService : IDteService
    {
        private readonly HttpClient _httpClient;
        private readonly DteSettings _settings;

        // Constructor debe ser público
        public DteService(HttpClient httpClient, DteSettings settings)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            
            // Configuración adicional si es necesaria
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<DteResponse> EnviarDte(DteData dte, string token)
        {
            var json = JsonSerializer.Serialize(dte);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PostAsync("recepciondte", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al enviar DTE: {error}");
            }

            return await response.Content.ReadFromJsonAsync<DteResponse>();
        }

        public async Task<string> ObtenerTokenAutenticacion()
        {
            var authData = new
            {
                nit = _settings.Nit,
                usuario = _settings.Usuario,
                clave = _settings.Clave
            };

            var response = await _httpClient.PostAsJsonAsync("autenticacion", authData);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<AuthResponse>();
            return result.Token;
        }
    }
    public interface IDteService
    {
        Task<DteResponse> EnviarDte(DteData dte, string token);
        Task<string> ObtenerTokenAutenticacion();
    }
}