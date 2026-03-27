using Microsoft.AspNetCore.Mvc;
using WebTemplate.Services;
using WebTemplate.Models;
using WebTemplate.Enum;
/*
namespace WebTemplate.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotifikacijaController : ControllerBase
    {
        protected readonly INotifikacijaService NotifikacijaService;

        public NotifikacijaController(INotifikacijaService notifikacijaService)
        {
            NotifikacijaService = notifikacijaService;
        }

        // Dobavljanje svih notifikacija za ulogovanog korisnika
        [HttpGet("moje/{korisnikId}")]
        public async Task<IActionResult> GetMojeNotifikacije(int korisnikId)
        {
            var notifikacije = await NotifikacijaService.GetMojeNotifikacijeAsync(korisnikId);
            return Ok(notifikacije);
        }

        // Označavanje notifikacije kao pročitane
        [HttpPatch("procitano/{id}")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var uspeh = await NotifikacijaService.MarkAsReadAsync(id);
            if (!uspeh) return NotFound(new { poruka = "Notifikacija nije pronađena." });

            return Ok(new { status = "Uspeh", poruka = "Notifikacija označena kao pročitana." });
        }

        // Ručno kreiranje notifikacije (npr. za testiranje ili sistemske poruke)
        [HttpPost("kreiraj")]
        public async Task<IActionResult> KreirajNotifikaciju([FromQuery] int primalacId, [FromQuery] string naslov, [FromQuery] TipNotifikacije tip)
        {
            await NotifikacijaService.KreirajNotifikacijuAsync(primalacId, naslov, tip);
            return Ok(new { status = "Uspeh", poruka = "Notifikacija poslata." });
        }
    }
}*/