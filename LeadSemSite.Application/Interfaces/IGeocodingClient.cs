using LeadSemSite.Domain.ValueObjects;

namespace LeadSemSite.Application.Interfaces
{
    public interface IGeocodingClient
    {
        Task<Coordenada?> ObterCoordenadasAsync(string local, CancellationToken ct = default);
    }
}
