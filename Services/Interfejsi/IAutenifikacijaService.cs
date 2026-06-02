namespace WebTemplate.Services{

public interface IAutentifikacijaService
{
Task<LoginResponseDTO?> LoginAsync(string username, string password);
    Task<bool> RegistracijaAsync(KorisnikDTO dto);
}}