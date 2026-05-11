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
    [HttpGet("GostId{id}")]
    public async Task<ActionResult<GostDTO>> GetById(int id)
    {
        var gost = await _service.GetByIdAsync(id);
        if (gost == null) return NotFound();
        return Ok(gost);
    }

  

    // FILTER
    [HttpGet("filter")]
    public async Task<ActionResult<IEnumerable<GostDTO>>> Filter( [FromQuery] string? ime,[FromQuery] string? prezime,[FromQuery] string? komitet,
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

    //pojedinacni filteri

    // SVI KOMITETI
    [HttpGet("komiteti")]
    public async Task<ActionResult<IEnumerable<string>>> GetKomiteti()
    {
        return Ok(await _service.GetKomitetiAsync());
    }

    // FILTER PO IMENU i prezimenu
 [HttpGet("pretraga")]
public async Task<ActionResult<IEnumerable<GostDTO>>> Pretraga(
    [FromQuery] string tekst)
{
    return Ok(await _service.PretragaAsync(tekst));
}

    // FILTER PO KOMITETU
    [HttpGet("komitet")]
    public async Task<ActionResult<IEnumerable<GostDTO>>> GetByKomitet(
        [FromQuery] string komitet)
    {
        return Ok(await _service.GetByKomitetAsync(komitet));
    }

    // FILTER PO ULOZI
    [HttpGet("role")]
    public async Task<ActionResult<IEnumerable<GostDTO>>> GetByRole(
        [FromQuery] Role role)
    {
        return Ok(await _service.GetByRoleAsync(role));
    }

    // SAMO SA ALERGIJAMA
    [HttpGet("alergije")]
    public async Task<ActionResult<IEnumerable<GostDTO>>> GetSaAlergijama()
    {
        return Ok(await _service.GetSaAlergijamaAsync());
    }

    // FILTER PO ISHRANI
    [HttpGet("ishrana")]
    public async Task<ActionResult<IEnumerable<GostDTO>>> GetByIshrana(
        [FromQuery] Ishrana ishrana)
    {
        return Ok(await _service.GetByIshranaAsync(ishrana));
    }
    // PO DOLASKU
[HttpGet("dolazak")]
public async Task<ActionResult<IEnumerable<GostDTO>>> GetByDolazak(
    [FromQuery] DateTime datum)
{
    return Ok(await _service.GetByDolazakAsync(datum));
}

// PO ODLASKU
[HttpGet("odlazak")]
public async Task<ActionResult<IEnumerable<GostDTO>>> GetByOdlazak(
    [FromQuery] DateTime datum)
{
    return Ok(await _service.GetByOdlazakAsync(datum));
}
}
