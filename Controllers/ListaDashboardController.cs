using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebTemplate.DTO;
using WebTemplate.Services;

namespace WebTemplate.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ListaDashboardController : ControllerBase
    {
        private readonly IListaDashboardService _service;

        public ListaDashboardController(
            IListaDashboardService service)
        {
            _service = service;
        }

        private int GetUserId()
        {
            return int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!
                    .Value);
        }


        [HttpGet("VratiMojuListu")]
        public async Task<IActionResult> GetMojaLista()
        {
            var korisnikId = GetUserId();

            var lista =await _service.GetMojaListaAsync(korisnikId);

            return Ok(lista);
        }

        // dodavanje stavke

        [HttpPost("DodajTask")]
        public async Task<IActionResult> Dodaj( [FromBody] ListaDashboardDTO dto)
        {
            var korisnikId = GetUserId();

            var rezultat = await _service.DodajAsync(korisnikId,dto);

            return Ok(rezultat);
        }

        // menja status zavrseno/nezavrseno

        [HttpPut("Ispunjeno/{id}")]
        public async Task<IActionResult> Ispunjeno(int id)
        {
            var uspeh =
                await _service.PromeniStatusAsync(id);

            if (!uspeh)
                return NotFound("Stavka nije pronađena.");

            return Ok();
        }

        // brisanje stavke

        [HttpDelete("{id}")]
        public async Task<IActionResult> Obrisi(int id)
        {
            var uspeh = await _service.ObrisiAsync(id);

            if (!uspeh)
                return NotFound("Stavka nije pronađena.");

            return Ok();
        }
    }
}