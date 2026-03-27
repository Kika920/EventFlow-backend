[ApiController]
[Route("api/[controller]")]
public class DogadjajController : ControllerBase
{
    private readonly IDogadjajService Service;

    public DogadjajController(IDogadjajService service)
    {
        Service = service;
    }

    [HttpGet]
    public async Task<ActionResult> GetAll(){ 
             try
            {
                var Rezultat = await Service.GetAllAsync();
                return Ok(Rezultat);
            }
            catch (Exception ex)
            {return StatusCode(500, ex.Message);}
                
        }

    [HttpGet("{id}")]
    public async Task<ActionResult> Get(int id)
    {
        var res = await Service.GetByIdAsync(id);
        return res != null ? Ok(res) : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult> Create(DogadjajDTO dto)
    { try{
       
        var noviDogadjaj = await Service.KreirajDogadjajAsync(dto);
        return Ok($"Uspešno sam kreirao događaj {noviDogadjaj.Ime}");}
        catch (Exception ex) 
    { 
        return BadRequest(ex.Message); 
    }
       
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, DogadjajDTO dto)
    {
        var success = await Service.UpdateAsync(id, dto);
        return success ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Koordinator")]
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
}