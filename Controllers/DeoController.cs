

namespace WebTemplate.Controllers;
[ApiController]
[Route("api/[controller]")]
public class DeoController : ControllerBase
{
    public IDeoService DeoService { get; }

    public DeoController(IDeoService deoService)
    {
        DeoService = deoService;
    }

    [HttpPost("DodajDeo")]
    public async Task<IActionResult> Kreiraj([FromBody] Deo deo)
    {
        await DeoService.KreirajAsync(deo);
        return Ok();
    }

    [HttpGet("VratiDeo/{id}")]
    public async Task<IActionResult> Vrati(int id)
    {
        var deo = await DeoService.GetByIdAsync(id);

        if (deo == null)
            return NotFound();

        return Ok(deo);
    }

    [HttpPut("IzmeniDeo{id}")]
    public async Task<IActionResult> Izmeni(int id, [FromBody] Deo deo)
    {
        var rezultat = await DeoService.IzmeniAsync(id, deo);

        if (!rezultat)
            return NotFound();

        return Ok();
    }

    [HttpDelete("ObrisiDeo/{id}")]
    public async Task<IActionResult> Obrisi(int id)
    {
        await DeoService.ObrisiAsync(id);
        return Ok();
    }

    [HttpGet("agenda/{agendaId}")]
    public async Task<IActionResult> VratiDeloveAgende(int agendaId)
    {
        var delovi = await DeoService.GetDeloviAgendeAsync(agendaId);
        return Ok(delovi);
    }
}