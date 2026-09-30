using LeadSemSite.Application.Interfaces;
using LeadSemSite.Domain.Models;

namespace LeadSemSite.Application.SerperService
{
    public class SerperService : ISerperService
    {
        private readonly IGeocodingClient _geocoding;
        private readonly ISerperClient _serper;

        public SerperService(ISerperClient serper, IGeocodingClient geocoding)
        {
            _serper = serper;
            _geocoding = geocoding;
        }

        public async Task<IReadOnlyList<Lead>?> BuscarLeadsAsync(
            string q, string cidade, int zoom, bool apenasSemSite, CancellationToken ct = default)
        {
            var coord = await _geocoding.ObterCoordenadasAsync(cidade, ct);
            if (coord is null) return null;

            var leads = await _serper.BuscarMapsAsync(q, coord.Value, zoom, ct);

            return apenasSemSite
                ? leads.Where(l => !l.PossuiSite).ToList()
                : leads;
        }
    }
}
