using LeadSemSite.Application.DTOS;

namespace LeadSemSite.Application.Interfaces
{
    public interface ISerperService
    {
        Task<LeadsResponse> BuscarLeadsAsync(
        string q, 
        string cidade, 
        int zoom, bool 
        apenasSemSite, 
        CancellationToken ct = default);
    }
}
