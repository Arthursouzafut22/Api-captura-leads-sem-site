using Microsoft.AspNetCore.Mvc;
using LeadSemSite.Application.Interfaces;
using LeadSemSite.Application.DTOS;

namespace LeadSemSite.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeadsController : ControllerBase
    {
        private readonly ISerperService _service;
        public LeadsController(ISerperService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> BuscarEmpresasSemSite(
        [FromQuery] string q,
        [FromQuery] string cidade,
        [FromQuery] int zoom = 13,
        [FromQuery] bool apenasSemSite = true,
        CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(q) || string.IsNullOrWhiteSpace(cidade))
                return BadRequest(new { erro = "Informe q e cidade" });

            try
            {
                var leads = await _service.BuscarLeadsAsync(q, cidade, zoom, apenasSemSite, ct);
                if (leads is null)
                    return NotFound(new { erro = "Cidade não encontrada" });

                var dtos = leads.Select(l => new LeadDto(
                    l.Id, l.Nome, l.Endereco, l.Telefone, l.Categoria,
                    l.Avaliacao, l.TotalAvaliacoes, l.Latitude, l.Longitude, l.PossuiSite));

                return Ok(new LeadsResponse(cidade, q, leads.Count, dtos));
            }
            catch (HttpRequestException)
            {
                return StatusCode(502, new { erro = "Falha ao consultar serviço externo" });
            }
        }
    }
}
