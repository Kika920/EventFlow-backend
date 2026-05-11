[ApiController]
[Route("api/[controller]")]
public class GostiController : ControllerBase
{
    private readonly IGostService _service;

    public GostiController(IGostService service)
    {
        _service = service;
    }

    // SVI
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GostDTO>>> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    // PO ID
    [HttpGet("{id}")]
    public async Task<ActionResult<GostDTO>> GetById(int id)
    {
        var gost = await _service.GetByIdAsync(id);
        if (gost == null) return NotFound();
        return Ok(gost);
    }

    // KOMITETI
    [HttpGet("komiteti")]
    public async Task<ActionResult<IEnumerable<string>>> GetKomiteti()
    {
        return Ok(await _service.GetKomitetiAsync());
    }

    // FILTER
    [HttpGet("filter")]
    public async Task<ActionResult<IEnumerable<GostDTO>>> Filter(
        [FromQuery] string? ime,
        [FromQuery] string? prezime,
        [FromQuery] string? komitet,
        [FromQuery] Role? role,
        [FromQuery] bool? imaAlergije,
        [FromQuery] Ishrana? ishrana)
    {
        var result = await _service.FilterAsync(
            ime,
            prezime,
            komitet,
            role,
            imaAlergije,
            ishrana);

        return Ok(result);
    }
}