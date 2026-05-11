using Microsoft.AspNetCore.Mvc;
using WebTemplate.Services;
using WebTemplate.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace WebTemplate.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClanController : ControllerBase
    {
        protected readonly IClanService ClanService;

        public ClanController(IClanService clanService)
        {
            ClanService = clanService;
        }

        // Vraća sve članove (opciono sortirane po broju zadataka)
        // Poziv: GET /api/Clan?sortiraj=true
        [HttpGet("vratiSveClanove")]
        public async Task<ActionResult<IEnumerable<Clan>>> GetSve([FromQuery] bool opadajuce)
        {
            var rezultati = await ClanService.VratiSveClanoveAsync(opadajuce);
            return Ok(rezultati);
        }

        // Vraća članove po statusu (Slobodan=0, Zauzet=1, Nedostupan=2)
        // Poziv: GET /api/Clan/status/0
        [HttpGet("statusClanovi/{status}")]
        public async Task<ActionResult<IEnumerable<Clan>>> GetPoStatusu(Status status)
        {
            var rezultati = await ClanService.VratiClanovePoStatusuAsync(status);
            return Ok(rezultati);
        }

        //   [Authorize(Roles = "Clan")]
       
        [HttpPatch("PromeniStatus/{id}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromQuery] Status noviStatus)
        {
            try 
            {
                await ClanService.PromeniStatusClanaAsync(id, noviStatus);
                return Ok(new { poruka = "Status uspešno promenjen." });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("VratiClana{id}")]
public async Task<ActionResult<Clan>> GetPoId(int id)
{
    var clan = await ClanService.VratiClanaPoIdAsync(id);

    if (clan == null)
        return NotFound("Član nije pronađen.");

    return Ok(clan);
}

[HttpGet("pretragaClanovaPoImenuIPrezimenu")]
public async Task<ActionResult<Clan>> GetPoImenuIPrezimenu(
    [FromQuery] string ime,
    [FromQuery] string prezime)
{
    var clan = await ClanService
        .VratiClanaPoImenuIPrezimenuAsync(ime, prezime);

    if (clan == null)
        return NotFound("Član nije pronađen.");

    return Ok(clan);
}
    }
}
