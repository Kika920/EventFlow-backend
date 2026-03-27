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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Clan>>> GetSve([FromQuery] bool sortiraj = false)
        {
            var rezultati = await ClanService.VratiSveClanoveAsync(sortiraj);
            return Ok(rezultati);
        }

        // Vraća članove po statusu (Slobodan=0, Zauzet=1, Nedostupan=2)
        // Poziv: GET /api/Clan/status/0
        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<Clan>>> GetPoStatusu(Status status)
        {
            var rezultati = await ClanService.VratiClanovePoStatusuAsync(status);
            return Ok(rezultati);
        }

        // Menja status člana
        // Poziv: PATCH /api/Clan/5/promeni-status?noviStatus=1
        [HttpPatch("{id}/promeni-status")]
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
    }
}
//fali vrati clana po imenu i prezimenu mada to moze da bude univerzalno za korisnika samo da se usput proveri i role