[ApiController]
[Route("api/[controller]")]
public class SesijaController : ControllerBase
{
    public SesijaService Service { get; }
    
 //   private readonly IPredavacService PredavacService;
    public SesijaController(SesijaService service) => Service = service;
  
//[Authorize(Roles = "Predavac,Koordinator")]
    [HttpPost("Rezervisi/{deoId}")]
    public async Task<IActionResult> Rezervisi(int deoId, [FromBody] SesijaDTO dto)
    {
        try {
            var uspeh = await Service.DodajSesijuAsync(deoId, dto);
            return uspeh ? Ok("Rezervisano") : BadRequest("Pogrešan tip slota");
        } catch (Exception ex) {
            return Conflict(ex.Message);
        }
    }
    //[Authorize(Roles = "Predavac,Koordinator")]
[HttpDelete("Obrisi/{id}")]
    public async Task<IActionResult> Obrisi(int id)
    {
       
        var sesija = await Service.SesijaRepo.GetByIdAsync(id);
        if (sesija == null) 
        {
            return NotFound("Sesija koju pokušavate da obrišete ne postoji.");
        }

        await Service.ObrisiSesiju(id);
        return Ok("Sesija je uspešno obrisana i termin je ponovo slobodan.");
    }
    [HttpGet("Detalji/{id}")]
    public async Task<IActionResult> Get(int id) => Ok(await Service.PreuzmiDetaljeSesijeAsync(id));
 /*    [HttpGet("{id}/Sesije")]
    public async Task<ActionResult> GetSesije(int id)
    {
        try { return Ok(await PredavacService.GetMojeSesijeAsync(id)); }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }*/
}