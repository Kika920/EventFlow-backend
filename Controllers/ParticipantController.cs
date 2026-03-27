[ApiController]
[Route("api/[controller]")]
public class ParticipantController : ControllerBase
{
    private readonly IParticipantService ParticipantService;
    public ParticipantController(IParticipantService service) => ParticipantService = service;

    [HttpPut("Ishrana/{id}")]
    public async Task<ActionResult> UpdateIshrana(int id, Ishrana tip, string alergije)
    {
        try 
        { 
            var Uspeh = await ParticipantService.UpdateIshranaAsync(id, tip, alergije);
            return Uspeh ? NoContent() : NotFound();
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }
}