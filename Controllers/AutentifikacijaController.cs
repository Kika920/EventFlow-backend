
using System.Text.Json.Serialization;

namespace WebTemplate.Controllers{

[ApiController]
[Route("api/[controller]")]
public class AutentifikacijaController : ControllerBase
{
    private readonly IAutentifikacijaService AuthService;

    public AutentifikacijaController(IAutentifikacijaService authService)
    {
        AuthService = authService;
    }

        [HttpPost("Registracija")]
        public async Task<IActionResult> Registracija([FromBody] KorisnikDTO dto)
        {
            var uspeh = await AuthService.RegistracijaAsync(dto);

            if (!uspeh)
                return BadRequest("Korisničko ime je zauzeto.");

            var rezultat = await AuthService.LoginAsync(
                dto.Username,
                dto.Password);

            if (rezultat == null)
                return Unauthorized("Greška pri generisanju tokena nakon registracije.");

            return Ok(rezultat);
        }
 
    [HttpPost("Login")]
public async Task<IActionResult> Login([FromBody] LoginZahtev zahtev)
{
    var rezultat = await AuthService.LoginAsync(
        zahtev.KorisnickoIme,
        zahtev.Lozinka);

    if (rezultat == null)
        return Unauthorized("Neispravni podaci");

    return Ok(rezultat);
}
//1. nacin da preko fronta saljem id
// 2. nacin je da se salje samo token, da se na backu izvuce id
//  var userId = int.Parse(  User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
}


public class LoginZahtev 
{ [JsonPropertyName("KorisnickoIme")] //ovo je dodato
        public string KorisnickoIme { get; set; } = string.Empty;

        [JsonPropertyName("Lozinka")] //ovo je dodato
        public string Lozinka { get; set; } = string.Empty;
        
}}