using LeadSemSite.Application.DTOS;
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

        public async Task<LeadsResponse> BuscarLeadsAsync(
         string q, string cidade, int zoom, bool apenasSemSite, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(q) || string.IsNullOrWhiteSpace(cidade))
                throw new ArgumentException("Informe q e cidade.");

            var coord = await _geocoding.ObterCoordenadasAsync(cidade, ct);
            if (coord is null)
                throw new KeyNotFoundException("Cidade não encontrada.");

            var leads = await _serper.BuscarMapsAsync(q, coord.Value, zoom, ct);

            var dtos = leads
                .Where(l => !apenasSemSite || !l.PossuiSite)
                .Select(l => new LeadDto(
                    l.Id, l.Nome, l.Endereco, l.Telefone, l.Categoria,
                    l.Avaliacao, l.TotalAvaliacoes, l.UrlImagemEmpresa,
                    l.Latitude, l.Longitude, l.PossuiSite))
                .ToList();

            return new LeadsResponse(cidade, q, dtos.Count, dtos);
        }
    }
}
