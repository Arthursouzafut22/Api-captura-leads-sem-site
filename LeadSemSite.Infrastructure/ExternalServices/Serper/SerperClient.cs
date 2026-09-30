using LeadSemSite.Application.Interfaces;
using System.Globalization;
using System.Text;
using System.Text.Json;
using LeadSemSite.Domain.ValueObjects;

namespace LeadSemSite.Infrastructure.ExternalServices.Serper
{
    public class SerperClient : ISerperClient
    {
        private readonly HttpClient _http;
        const string KEY = "";
        public SerperClient(HttpClient http) => _http = http;

        public async Task<string> BuscarMapsAsync(string q, Coordenada coord, int zoom, CancellationToken ct = default)
        {
            var ll = string.Create(CultureInfo.InvariantCulture,
            $"@{coord.Latitude},{coord.Longitude},{zoom}z");

            var body = JsonSerializer.Serialize(new { q, hl = "pt-br", ll });
            using var content = new StringContent(body, Encoding.UTF8, "application/json");

            using var resp = await _http.PostAsync("maps", content, ct);
            resp.EnsureSuccessStatusCode();

            return await resp.Content.ReadAsStringAsync(ct);
        }
    }
}
