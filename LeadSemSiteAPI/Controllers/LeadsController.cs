using LeadSemSite.Application.DTOS;
using LeadSemSite.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LeadSemSite.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeadsController : ControllerBase
    {
        private readonly ISerperService _service;
        public LeadsController(ISerperService service) => _service = service;

        /// <summary>Busca empresas no Google Maps por termo e cidade.</summary>
        /// <param name="q">Termo da busca (ex.: dentistas).</param>
        /// <param name="cidade">Cidade selecionada (ex.: Belo Horizonte, MG).</param>
        /// <param name="zoom">Zoom do mapa, de 1 a 21. Padrão 13.</param>
        /// <param name="apenasSemSite">Se true, retorna só empresas sem site.</param>
        /// <param name="pagina">Número da página, a partir de 1.</param>
        
        [HttpGet("consultar-empresas-sem-site")]
        [ProducesResponseType(typeof(LeadsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]

        public async Task<IActionResult> BuscarEmpresasSemSite(
        [FromQuery] string q,
        [FromQuery] string cidade,
        [FromQuery] int zoom = 13,
        [FromQuery] bool apenasSemSite = true,
        [FromQuery] int pagina = 1,
        CancellationToken ct = default)
        {
            try
            {
                var resposta = await _service.BuscarLeadsAsync(q!, cidade!, zoom, apenasSemSite, pagina, ct);
                return Ok(resposta);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { erro = ex.Message });
            }
            catch (HttpRequestException)
            {
                return StatusCode(502, new { erro = "Falha ao consultar serviço externo" });
            }
        }
    }
}
