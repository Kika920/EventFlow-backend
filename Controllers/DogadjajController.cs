namespace WebTemplate.Controllers;
[ApiController]
[Route("api/[controller]")]
public class DogadjajController : ControllerBase
{
    private readonly IDogadjajService Service;

    public DogadjajController(IDogadjajService service)
    {
        Service = service;
    }

    [HttpGet("VratiSveDogadjaje")]
    public async Task<ActionResult> GetAll(){ 
             try
            {
                var Rezultat = await Service.GetAllAsync();
                return Ok(Rezultat);
            }
            catch (Exception ex)
            {return StatusCode(500, ex.Message);}
                
        }

    [HttpGet("VratiDogadjaj{id}")]
    public async Task<ActionResult> Get(int id)
    {
        var res = await Service.GetByIdAsync(id);
        return res != null ? Ok(res) : NotFound();
    }
//[Authorize(Roles = "Koordinator")]
    [HttpPost("KreirajDogadjaj")]
    public async Task<ActionResult> Create(DogadjajDTO dto)
    { try{
       
        var noviDogadjaj = await Service.KreirajDogadjajAsync(dto);
        return Ok($"Uspešno sam kreirao događaj {noviDogadjaj.Ime}");}
        catch (Exception ex) 
    { 
        return BadRequest(ex.Message); 
    }
       
    }
//[Authorize(Roles = "Koordinator")]
    [HttpPut("IzmeniDogadjaj/{id}")]
    public async Task<IActionResult> Update(int id, DogadjajDTO dto)
    {
        var success = await Service.UpdateAsync(id, dto);
        return success ? NoContent() : NotFound();
    }
// [Authorize(Roles = "Koordinator")]
    [HttpDelete("IzbrisiDogadjaj{id}")]
   
    public async Task<ActionResult> Delete(int id)
    {
      try 
    { 
        var uspeh = await Service.ObrisiDogadjajAsync(id);
        
        if (!uspeh) return NotFound($"Događaj sa ID-jem {id} nije pronađen.");

        return Ok($"Uspešno obrisan događaj sa ID-jem {id}"); 
    }
    catch (Exception ex) 
    { 
        return StatusCode(500, $"Greška pri brisanju: {ex.Message}"); 
    }
    }
    [HttpGet("AgendaStatus/{id}")]
public async Task<IActionResult> AgendaStatus(int id)
{
    try
    {
        bool prazna = await Service.DaLiJeAgendaPraznaAsync(id);

        return Ok(new
        {
            DogadjajId = id,
            AgendaPrazna = prazna,
            Status = prazna ? "Prazna" : "Popunjena"
        });
    }
    catch(Exception ex)
    {
        return BadRequest(ex.Message);
    }
}
}