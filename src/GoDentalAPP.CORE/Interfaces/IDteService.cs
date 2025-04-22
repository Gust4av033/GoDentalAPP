using DevExpress.Utils.Design;
using GoDentalAPP.Core.Entities;
using GoDentalAPP.src.GoDentalAPP.CORE.Configuration;
using System;
using System.Collections;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GoDentalAPP.src.GoDentalAPP.CORE.Interfaces
{
    public interface IDteService
    {
        Task<DteResponse> EnviarDte(DteData dte, string token);
        Task<string> ObtenerTokenAutenticacion();
    }

    public class DteService : IDteService
    {
        private readonly HttpClient _httpClient;
        private readonly DteSettings _settings;

        public DteService(DteSettings settings)
        {
            _settings = settings;
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_settings.ApiUrl)
            };
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

            var result = await response.Content.ReadFromJsonAsync<DteResponse>();
            return result;
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

    public class DteResponse
    {
        public string SelloRecibido { get; set; }
        public string Estado { get; set; }
    }

    public class AuthResponse
    {
        public string Token { get; set; }
    }
}
