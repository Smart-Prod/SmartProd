using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using SmartProd.Models;

namespace SmartProd.Services
{
    public class ViaCepService
    {
        private readonly HttpClient _httpClient;

        public ViaCepService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Endereco?> ConsultarCepAsync(string cep)
        {
            var response = await _httpClient.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
            if (!response.IsSuccessStatusCode) return null;

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Endereco>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}

