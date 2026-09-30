using LeadSemSite.Domain.Models;

namespace LeadSemSite.Application.Interfaces
{
    public interface ISerperService
    {
        Task<IReadOnlyList<Lead>?> BuscarLeadsAsync(
        string q,
        string cidade,
        int zoom,
        bool apenasSemSite,
        CancellationToken ct = default);
    }
}
