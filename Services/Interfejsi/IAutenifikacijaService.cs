namespace WebTemplate.Services{

public interface IAutentifikacijaService
{
    Task<string?> LoginAsync(string username, string password);
    Task<bool> RegistracijaAsync(KorisnikDTO dto);
}}