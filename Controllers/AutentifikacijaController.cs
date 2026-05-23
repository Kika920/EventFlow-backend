
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
            return BadRequest("Korisničko ime je zauzeto");
        var token = await AuthService.LoginAsync(dto.Username, dto.Password);

        if (token == null) 
            return Unauthorized("Greška pri generisanju tokena nakon registracije");
        return Ok(new { TokenString = token }); 
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