using Microsoft.AspNetCore.Mvc;
using WebTemplate.Services;
using WebTemplate.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
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

    //objedinjeni filteri za status i broj izvresnih zadataka tako da moze da se vrati i po oba kriterijuma
   [HttpGet("vratiSveClanove")]
public async Task<ActionResult<IEnumerable<Clan>>> GetSve(
    [FromQuery] bool? opadajuce,
    [FromQuery] Status? status)
{
    var rezultati =
        await ClanService.VratiSveClanoveAsync(
            opadajuce,
            status);

    return Ok(rezultati);
}

    

        // Omogućava promenu statusa u bilo kom trenutku.
        // Pored izmene na samom članu, automatski cepa i prilagođava termine u tabeli dostupnosti.
        // [Authorize(Roles = "Clan")]
        [HttpPatch("PromeniStatus/{id}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromQuery] Status noviStatus)
        {
            try 
            {
                await ClanService.PromeniStatusClanaAsync(id, noviStatus);
                return Ok(new { poruka = "Status uspešno promenjen i sinhronizovan sa satnicom." });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // Vraća profil ulogovanog člana sa osveženim trenutnim statusom
        [HttpGet("MojProfil")]
        public async Task<IActionResult> MojProfil()
        {
            try
            {
                var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdStr))
                    return Unauthorized("Korisnik nije autorizovan.");

                var userId = int.Parse(userIdStr);
                var clan = await ClanService.VratiClanaPoIdAsync(userId);

                if (clan == null)
                    return NotFound("Član nije pronađen.");

                return Ok(clan);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

       
        [HttpGet("pretragaClanovaPoImenuIPrezimenu")]
        public async Task<ActionResult<Clan>> GetPoImenuIPrezimenu(
            [FromQuery] string ime,
            [FromQuery] string prezime)
        {
            var clan = await ClanService.VratiClanaPoImenuIPrezimenuAsync(ime, prezime);

            if (clan == null)
                return NotFound("Član nije pronađen.");

            return Ok(clan);
        }
     
    }
}