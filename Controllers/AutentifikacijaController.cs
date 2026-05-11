
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
        var Rezultat = await AuthService.RegistracijaAsync(dto);
        return Rezultat ? Ok("Uspešna registracija") : BadRequest("Korisničko ime je zauzeto");
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginZahtev zahtev)
    {
        var Token = await AuthService.LoginAsync(zahtev.KorisnickoIme, zahtev.Lozinka);
        if (Token == null) return Unauthorized("Neispravni podaci");

        return Ok(new { TokenString = Token });
    }
}


public class LoginZahtev 
{ [JsonPropertyName("KorisnickoIme")] //ovo je dodato
        public string KorisnickoIme { get; set; } = string.Empty;

        [JsonPropertyName("Lozinka")] //ovo je dodato
        public string Lozinka { get; set; } = string.Empty;
}}