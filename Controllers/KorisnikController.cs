using Microsoft.AspNetCore.Mvc;
using WebTemplate.Services;
using WebTemplate.DTO;

namespace WebTemplate.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KorisnikController : ControllerBase
    {
        private readonly IKorisnikService KorisnikService;

        public KorisnikController(IKorisnikService korisnikService)
        {
            KorisnikService = korisnikService;
        }
//mozda ne treba
        [HttpGet("PreuzmiSve")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var Rezultat = await KorisnikService.GetAllAsync();
                return Ok(Rezultat);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
//vrati korsinika po imenu i prezimenu-vracam dto preko role
        [HttpGet("PreuzmiPoId/{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                var Korisnik = await KorisnikService.GetByIdAsync(id);
                if (Korisnik == null) return NotFound("Korisnik nije pronađen.");
                return Ok(Korisnik);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

       

//[Authorize(Roles = "Koordinator,Predavac,Participant")]
        [HttpPut("Izmeni/{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] KorisnikDTO dto)
        {
            try
            {
                var Uspeh = await KorisnikService.UpdateAsync(id, dto);
                if (!Uspeh) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
//[Authorize(Roles = "Koordinator")]
        [HttpDelete("Obrisi/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var Uspeh = await KorisnikService.ObrisiKorisnikaAsync(id);
                if (!Uspeh) return NotFound();
                return Ok("Korisnik uspešno obrisan.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

[HttpGet("filtriraj")]
public async Task<IActionResult> Filtriraj([FromQuery] string? ime, [FromQuery] string? prezime)
{
    try 
    {
        var rezultati = await KorisnikService.FiltrirajKorisnikeAsync(ime, prezime);
        return Ok(rezultati);
    }
    catch (Exception ex)
    {
        return BadRequest(ex.Message);
    }
}
    }

}