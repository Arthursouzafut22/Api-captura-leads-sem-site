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

        [HttpGet()]
        public async Task<IActionResult> BuscarEmpresasSemSite([FromQuery] string q,
        [FromQuery] string cidade,
        [FromQuery] int zoom = 13,
        CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(q) || string.IsNullOrWhiteSpace(cidade))
                return BadRequest(new { erro = "Informe q e cidade" });

            try
            {
                var json = await _service.BuscarLeadsAsync(q, cidade, zoom, ct);
                if (json is null)
                    return NotFound(new { erro = "Cidade não encontrada" });

                return Content(json, "application/json");
            }
            catch (HttpRequestException)
            {
                return StatusCode(502, new { erro = "Falha ao consultar serviço externo" });
            }
        }
    }
}
