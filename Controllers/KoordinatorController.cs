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

        [HttpGet("PreuzmiSveKoordinatore")]
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

        [HttpGet("VratiKoordinatora/{id}")]
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
//idk da li nam ovo treba jer nema potrebe da se menja tip u realnosti kada neko postane koordiantor ali neka ga za svaki slucaj do krajnjeg roka
      
   //   [Authorize(Roles = "Koordinator")]
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
        }}}
        //fali obrisi


//upisati jednog koordiantora u bazi