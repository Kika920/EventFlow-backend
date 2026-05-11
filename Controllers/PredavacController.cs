[ApiController]
[Route("api/[controller]")]
public class PredavacController : ControllerBase
{
    private readonly IPredavacService PredavacService;
    public PredavacController(IPredavacService Service) => PredavacService = Service;

    [HttpGet("{id}/Sesije")]
    public async Task<ActionResult> GetSesije(int id)
    {
        try { return Ok(await PredavacService.GetMojeSesijeAsync(id)); }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }
     [HttpGet("VratiSvePredavace")]
        public async Task<ActionResult<IEnumerable<Predavac>>>  GetAll()
        {
            var predavaci = await PredavacService.GetAllAsync();

            return Ok(predavaci);
        }

        [HttpGet("VratiPredavaca{id}")]
        public async Task<ActionResult<Predavac>>
            GetById(int id)
        {
            var predavac =
                await PredavacService.GetByIdAsync(id);

            if (predavac == null)
                return NotFound();

            return Ok(predavac);
        }
           [HttpGet("filterZaVise")]
    public async Task<ActionResult<IEnumerable<Predavac>>> Filtriraj(
        [FromQuery] string? pretraga,
        [FromQuery] string? komitet,
        [FromQuery]Ishrana? ishrana,
        [FromQuery]bool? alergije)
    {
        var result = await PredavacService
            .FiltrirajPredavaceAsync(pretraga, komitet,ishrana,alergije);

        return Ok(result);
    }
    //pojedinacni filteri

        [HttpGet("komitetPredavaca/{komitet}")]
        public async Task<ActionResult<IEnumerable<Predavac>>>
            GetByKomitet(string komitet)
        {
            var predavaci =
                await PredavacService.GetByKomitetAsync(
                    komitet);

            return Ok(predavaci);
        }

        [HttpGet("ime-prezime")]
        public async Task<ActionResult<IEnumerable<Predavac>>>
            GetByImePrezime(
                [FromQuery] string ime,
                [FromQuery] string prezime)
        {
            var predavaci =
                await PredavacService.GetByImeIPrezimeAsync(
                    ime,
                    prezime);

            return Ok(predavaci);
        }

        [HttpGet("dolazakPredavaca")]
        public async Task<ActionResult<IEnumerable<Predavac>>>
            GetByDolazak(
                [FromQuery] DateTime vreme)
        {
            var predavaci =
                await PredavacService
                    .GetByVremeDolaskaAsync(vreme);

            return Ok(predavaci);
        }

        [HttpGet("odlazakPredavaca")]
        public async Task<ActionResult<IEnumerable<Predavac>>>
            GetByOdlazak(
                [FromQuery] DateTime vreme)
        {
            var predavaci =
                await PredavacService
                    .GetByVremeOdlaskaAsync(vreme);

            return Ok(predavaci);
        }
        [HttpGet("alergijePredavaca")]
public async Task<ActionResult<IEnumerable<Participant>>> GetWithAlergije()
{
    return Ok(await PredavacService.GetAllWithAlergijeAsync());
}
    }
