[ApiController]
[Route("api/[controller]")]
public class PredavacController : ControllerBase
{
    private readonly IPredavacService PredavacService;
    public PredavacController(IPredavacService service) => PredavacService = service;

    [HttpGet("{id}/Sesije")]
    public async Task<ActionResult> GetSesije(int id)
    {
        try { return Ok(await PredavacService.GetMojeSesijeAsync(id)); }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }
}