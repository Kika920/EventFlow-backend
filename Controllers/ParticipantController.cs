[ApiController]
[Route("api/[controller]")]
public class ParticipantController : ControllerBase
{
    private readonly IParticipantService ParticipantService;
    public ParticipantController(IParticipantService service) => ParticipantService = service;


    [HttpPut("AzurirajIshranuParticipanta/{id}")]
    public async Task<ActionResult> UpdateIshrana(int id, Ishrana tip, string alergije)
    {
        try 
        { 
            var Uspeh = await ParticipantService.UpdateIshranaAsync(id, tip, alergije);
            return Uspeh ? NoContent() : NotFound();
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }
    [HttpGet("VratiParticipanta/{id}")]
public async Task<ActionResult<Participant>> GetById(int id)
{
    var participant = await ParticipantService.GetByIdAsync(id);

    if (participant == null)
        return NotFound();

    return Ok(participant);
}

[HttpGet("VratiSveParticipante")]
public async Task<ActionResult<IEnumerable<Participant>>> GetAll()
{
    var participants = await ParticipantService.GetAllAsync();
    return Ok(participants);
}
//mozes da korsitis i ovaj filter i mozes one dole pojedinacno ili one iz kontrolera gosti gde je objedinjeno
//[Authorize(Roles = "Koordinator")]
    [HttpGet("filterZaVise")]
    public async Task<ActionResult<IEnumerable<Participant>>> Filtriraj(
        [FromQuery] string? pretraga,
        [FromQuery] string? komitet,
        [FromQuery] Ishrana? ishrana ,
        [FromQuery]bool? alergije)
    {
        var result = await ParticipantService
            .FiltrirajParticipanteAsync(pretraga, komitet, ishrana,alergije);

        return Ok(result);
    }
//[Authorize(Roles = "Koordinator")]
[HttpGet("ishrana/{ishrana}")]
public async Task<ActionResult<IEnumerable<Participant>>> GetByIshrana(
    Ishrana ishrana)
{
    return Ok(await ParticipantService.GetByIshranaAsync(ishrana));
}
//[Authorize(Roles = "Koordinator")]
[HttpGet("ime-prezime")]
public async Task<ActionResult<IEnumerable<Participant>>> GetByImePrezime(
    [FromQuery] string ime,
    [FromQuery] string prezime)
{
    return Ok(await ParticipantService
        .GetByImeIPrezimeAsync(ime, prezime));
}
//[Authorize(Roles = "Koordinator")]
[HttpGet("komitetParticipanta/{komitet}")]
public async Task<ActionResult<IEnumerable<Participant>>> GetByKomitet(
    string komitet)
{
    return Ok(await ParticipantService
        .GetByKomitetAsync(komitet));
}
//[Authorize(Roles = "Koordinator")]
[HttpGet("dolazakParticipanta")]
public async Task<ActionResult<IEnumerable<Participant>>> GetByDolazak(
    [FromQuery] DateTime vreme)
{
    return Ok(await ParticipantService
        .GetByVremeDolaskaAsync(vreme));
}
[Authorize(Roles = "Koordinator")]
[HttpGet("odlazakParticipanta")]
public async Task<ActionResult<IEnumerable<Participant>>> GetByOdlazak(
    [FromQuery] DateTime vreme)
{
    return Ok(await ParticipantService
        .GetByVremeOdlaskaAsync(vreme));
}
[HttpGet("alergijeParticipanata")]
public async Task<ActionResult<IEnumerable<Participant>>> GetWithAlergije()
{
    return Ok(await ParticipantService.GetAllWithAlergijeAsync());
}
}