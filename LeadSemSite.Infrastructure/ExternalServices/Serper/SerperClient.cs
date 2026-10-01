using LeadSemSite.Application.Interfaces;
using LeadSemSite.Infrastructure.ExternalServices.DTOS;
using LeadSemSite.Domain.Models;
using LeadSemSite.Domain.ValueObjects;
using System.Globalization;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace LeadSemSite.Infrastructure.ExternalServices.Serper
{
    public class SerperClient : ISerperClient
    {
        private readonly HttpClient _http;
        public SerperClient(HttpClient http) => _http = http;

        public async Task<IReadOnlyList<Lead>> BuscarMapsAsync(
            string q, Coordenada coord, int zoom, int pagina, CancellationToken ct = default)
        {
            var ll = string.Create(CultureInfo.InvariantCulture, $"@{coord.Latitude},{coord.Longitude},{zoom}z");

            var body = JsonSerializer.Serialize(new { q, hl = "pt-br", ll, page = pagina });
            using var content = new StringContent(body, Encoding.UTF8, "application/json");

            using var resp = await _http.PostAsync("maps", content, ct);
            resp.EnsureSuccessStatusCode();

            var dto = await resp.Content.ReadFromJsonAsync<SerperMapsResponse>(cancellationToken: ct);

            return dto?.Places?.Select(p => new Lead
            {
                Id = p.Cid ?? p.PlaceId ?? Guid.NewGuid().ToString(),
                Nome = p.Title ?? "",
                Endereco = p.Address,
                Telefone = p.PhoneNumber,
                Categoria = p.Type,
                Avaliacao = p.Rating,
                TotalAvaliacoes = p.RatingCount,
                Latitude = p.Latitude,
                Longitude = p.Longitude,
                UrlImagemEmpresa = p.thumbnailUrl,
                Site = p.Website
            }).ToList() ?? new List<Lead>();
        }
    }
}

