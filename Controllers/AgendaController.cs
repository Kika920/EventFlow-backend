
namespace WebTemplate.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgendaController : ControllerBase
    {
        // Koristimo javni ili zaštićeni Property sa velikim početnim slovom
        public IAgendaService AgendaService { get; }

        public AgendaController(IAgendaService agendaService)
        {
            AgendaService = agendaService;
        }

        [HttpPost("kreiraj")]
        public async Task<IActionResult> KreirajAgendu([FromBody] Agenda agenda)
        {
            if (agenda == null) 
                return BadRequest("Podaci o agendi su neispravni.");

            await AgendaService.KreirajAgenduAsync(agenda);
            return Ok(new { Poruka = "Agenda je uspešno kreirana." });
        }

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
        public async Task<IActionResult> AzurirajVreme(int deoId, [FromBody] TimeSpan novoVremeOd)
        {
            try
            {
                await AgendaService.AzurirajVremeSaPomakomAsync(deoId, novoVremeOd);
                return Ok(new { Poruka = "Vreme uspešno ažurirano sa pomakom za sve naredne stavke." });
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
    }
}
    
