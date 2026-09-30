using LeadSemSite.Domain.Models;

namespace LeadSemSite.Application.Interfaces
{
    public interface ISerperService
    {
        Task<string?> BuscarLeadsAsync(string q, string cidade, int zoom, CancellationToken ct = default);
    }
}
