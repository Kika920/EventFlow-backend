
namespace WebTemplate.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgendaController : ControllerBase
    {
    
        public IAgendaService AgendaService { get; }

        public AgendaController(IAgendaService agendaService)
        {
            AgendaService = agendaService;
        }
/*
        [HttpPost("kreiraj")]
        public async Task<IActionResult> KreirajAgendu([FromBody] Agenda agenda)
        {
            if (agenda == null) 
                return BadRequest("Podaci o agendi su neispravni.");

            await AgendaService.KreirajAgenduAsync(agenda);
            return Ok(new { Poruka = "Agenda je uspešno kreirana." });
        }
        */

        [HttpDelete("obrisi/{id}")]
        public async Task<IActionResult> ObrisiAgendu(int id)
        {
            try
            {
                await AgendaService.ObrisiAgenduSaSadrzajemAsync(id);
                return Ok(new { Poruka = $"Agenda sa ID-jem {id} je obrisana." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    [HttpPut("pomeri-vreme/{deoId}")]
public async Task<IActionResult> AzurirajVreme(int deoId, [FromBody] int pomakMinuta)
{
    try
    {
        await AgendaService.AzurirajVremeSaPomakomAsync(deoId, pomakMinuta);
        return Ok(new
        {
            Poruka = $"Agenda je pomerena za {pomakMinuta} minuta."
        });
    }
    catch (Exception ex)
    {
        return BadRequest(ex.Message);
    }
}
        [HttpGet("slobodni-slotovi/{agendaId}")]
        public async Task<IActionResult> GetSlobodniSlotovi(int agendaId)
        {
            var Slotovi = await AgendaService.PreuzmiSlobodneSlotoveAsync(agendaId);
            return Ok(Slotovi);
        }
        [HttpGet("AgendaZaDogadjaj/{dogadjajId}")]
public async Task<IActionResult> GetAgendaZaDogadjaj(int dogadjajId)
{
    var agenda = await AgendaService.PreuzmiAgenduZaDogadjajAsync(dogadjajId);

    if (agenda == null)
        return NotFound();

    return Ok(agenda);
}
[HttpGet("VratiAgendu{id}")]
public async Task<IActionResult> GetAgenda(int id)

{
    var agenda = await AgendaService.PreuzmiAgenduAsync(id);

    if (agenda == null)
        return NotFound();

    return Ok(agenda);
}
    }
}
    
