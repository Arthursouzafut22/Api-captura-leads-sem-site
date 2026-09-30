using LeadSemSite.Domain.Models;
using LeadSemSite.Domain.ValueObjects;

namespace LeadSemSite.Application.Interfaces
{
    public interface ISerperClient
    {
        Task<string> BuscarMapsAsync(string q, Coordenada coord, int zoom, CancellationToken ct = default);
    }
}
