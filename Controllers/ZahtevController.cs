
namespace WebTemplate.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ZahtevController : ControllerBase
    {
        protected readonly IZahtevService ZahtevService;

        public ZahtevController(IZahtevService zahtevService)
        {
            ZahtevService = zahtevService;
        }

        // 1. KREIRANJE NOVOG ZAHTEVA
        // Poziv: POST /api/Zahtev?posiljalacId=1&dogadjajId=5
        //[Authorize(Roles = "Koordinator")]
        [HttpPost("KreirajZahtev")]
        public async Task<ActionResult<Zahtev>> Create([FromBody] ZahtevDTO dto, [FromQuery] int posiljalacId, [FromQuery] int dogadjajId)
        {
            try
            {
                var noviZahtev = await ZahtevService.KreirajZahtevAsync(dto, posiljalacId, dogadjajId);
                return CreatedAtAction(nameof(GetSveSaDetaljima), new { id = noviZahtev.Id }, noviZahtev);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // 2. SVI ZAHTEVI (Sa svim Include-ovima za tabelu)
       // [Authorize(Roles = "Koordinator")]
        [HttpGet("detalji")]
        public async Task<ActionResult<IEnumerable<Zahtev>>> GetSveSaDetaljima()
        {
            var rezultati = await ZahtevService.GetAllSaDetaljimaAsync();
            return Ok(rezultati);
        }

        // 3. SAMO NEDODELJENI (Za koordinatorov "Inbox")
       // [Authorize(Roles = "Koordinator")]
        [HttpGet("nedodeljeni")]
        public async Task<ActionResult<IEnumerable<Zahtev>>> GetNedodeljeni()
        {
            var rezultati = await ZahtevService.GetNedodeljeniAsync();
            return Ok(rezultati);
        }

        // 4. ZAHTEVI ZA SPECIFIČAN DOGAĐAJ
        [HttpGet("dogadjaj/{dogadjajId}")]
        public async Task<ActionResult<IEnumerable<Zahtev>>> GetPoDogadjaju(int dogadjajId)
        {
            var rezultati = await ZahtevService.GetZahteviPoDogadjajuAsync(dogadjajId);
            return Ok(rezultati);
        }

        //  [Authorize(Roles = "Koordinator")]
        [HttpPatch("{zahtevId}/dodeli")]
        public async Task<IActionResult> Dodeli(int zahtevId, [FromQuery] List<int> clanId, [FromQuery] int koordinatorId)
        {
            try
            {
                await ZahtevService.DodeliZahtevAsync(zahtevId, clanId, koordinatorId);
                return Ok(new { poruka = "Član uspešno zadužen za zadatak." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // 6. POTVRDA IZVRŠENJA (Završetak posla)
        //  [Authorize(Roles = "Clan")]
        [HttpPatch("{id}/izvrsi")]
        public async Task<IActionResult> MarkAsCompleted(int id)
        {
            try
            {
                await ZahtevService.IzvrsiZahtevAsync(id);
                return Ok(new { poruka = "Zadatak je uspešno izvršen!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // 7. PROMENA STATUSA (Ručna, ako zatreba)
        //  [Authorize(Roles = "Clan")]
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromQuery] StatusZahteva noviStatus)
        {
            await ZahtevService.PromeniStatusAsync(id, noviStatus);
            return Ok();
        }
    }
}