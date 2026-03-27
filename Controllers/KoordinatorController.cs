using Microsoft.AspNetCore.Mvc;
using WebTemplate.Services;
using WebTemplate.Enum;

namespace WebTemplate.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KoordinatorController : ControllerBase
    {
        private readonly IKoordinatorService KoordinatorService;

        public KoordinatorController(IKoordinatorService koordinatorService)
        {
            KoordinatorService = koordinatorService;
        }

        [HttpGet("PreuzmiSve")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var Rezultat = await KoordinatorService.GetAllAsync();
                return Ok(Rezultat);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("Profil/{id}")]
        public async Task<IActionResult> GetProfile(int id)
        {
            try
            {
                var Koordinator = await KoordinatorService.GetByIdAsync(id);
                if (Koordinator == null) return NotFound("Koordinator nije pronađen.");
                return Ok(Koordinator);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("IzmeniTip/{id}")]
        public async Task<IActionResult> UpdateType(int id, [FromQuery] TipKoordinatora noviTip)
        {
            try
            {
                var Uspeh = await KoordinatorService.UpdateTipAsync(id, noviTip);
                return Uspeh ? Ok("Tip koordinatora uspešno ažuriran.") : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

      /*  [HttpGet("{id}/Dogadjaji")]
        public async Task<ActionResult> GetEvents(int id)
        {
            try
            {
                var Dogadjaji = await KoordinatorService.GetMojiDogadjajiAsync(id);
                return Ok(Dogadjaji);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
 */   }
}
//upisati jednog koordiantora u bazi