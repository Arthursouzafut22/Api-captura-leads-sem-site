using Microsoft.AspNetCore.Mvc;
using LeadSemSite.Application.Interfaces;

namespace LeadSemSite.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeadsController : ControllerBase
    {
        private readonly ISerperService _service;
        public LeadsController(ISerperService service) => _service = service;

        [HttpGet("consultar-empresas-sem-site")]
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
