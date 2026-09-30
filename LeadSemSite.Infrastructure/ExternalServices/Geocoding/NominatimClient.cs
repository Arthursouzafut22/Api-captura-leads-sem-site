using System.Globalization;
using System.Text.Json;
using LeadSemSite.Application.Interfaces;
using LeadSemSite.Domain.ValueObjects;
using Microsoft.Extensions.Caching.Memory;

namespace LeadSemSite.Infrastructure.ExternalServices.Geocoding;

public class NominatimClient : IGeocodingClient
{
    private readonly HttpClient _http;
    private readonly IMemoryCache _cache;

    public NominatimClient(HttpClient http, IMemoryCache cache)
    {
        _http = http;
        _cache = cache;
    }

    public async Task<Coordenada?> ObterCoordenadasAsync(string local, CancellationToken ct = default)
    {
        var chave = $"geo:{local.Trim().ToLowerInvariant()}";

        if (_cache.TryGetValue(chave, out Coordenada emCache))
            return emCache;

        var url = $"search?q={Uri.EscapeDataString(local)}&format=json&limit=1&countrycodes=br";

        using var resp = await _http.GetAsync(url, ct);
        resp.EnsureSuccessStatusCode();

        using var doc = JsonDocument.Parse(await resp.Content.ReadAsStringAsync(ct));
        if (doc.RootElement.GetArrayLength() == 0) return null;

        var item = doc.RootElement[0];
        var lat = double.Parse(item.GetProperty("lat").GetString()!, CultureInfo.InvariantCulture);
        var lon = double.Parse(item.GetProperty("lon").GetString()!, CultureInfo.InvariantCulture);

        var coord = new Coordenada(lat, lon);
        _cache.Set(chave, coord, TimeSpan.FromDays(30));
        return coord;
    }
}