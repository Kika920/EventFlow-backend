using Microsoft.AspNetCore.Mvc;
using WebTemplate.Services;
using WebTemplate.Models;

namespace WebTemplate.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VremeDostupnostiController : ControllerBase
    {
        private readonly IVremeDostupnostiService VremeDostupnostiService;

        public VremeDostupnostiController(IVremeDostupnostiService vremeDostupnostiService)
        {
            VremeDostupnostiService = vremeDostupnostiService;
        }

        [HttpGet("Clan/{clanId}")]
        [Authorize(Roles = "Clan")]
        public async Task<ActionResult> GetByClan(int clanId)
        {
            try
            {
                var Tabela = await VremeDostupnostiService.GetByClanIdAsync(clanId);
                return Ok(Tabela);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Dodaj/{clanId}")]
        public async Task<IActionResult> AddDostupnost(int clanId, [FromBody] VremeDostupnosti podaci)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var Rezultat = await VremeDostupnostiService.DodajDostupnostAsync(clanId, podaci);
                return Ok(Rezultat);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Obrisi/{id}")]
        public async Task<ActionResult> DeleteDostupnost(int id)
        {
            try
            {
                var Uspeh = await VremeDostupnostiService.ObrisiDostupnostAsync(id);
                if (!Uspeh) return NotFound("Stavka dostupnosti nije pronađena.");
                
                return Ok("Dostupnost uspešno obrisana.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}