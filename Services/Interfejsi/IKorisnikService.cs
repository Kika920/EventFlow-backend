
namespace WebTemplate.Services
{
    public interface IKorisnikService
    {
        Task<IEnumerable<Korisnik>> GetAllAsync();
        Task<Korisnik?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, KorisnikDTO dto);
        Task<bool> ObrisiKorisnikaAsync(int id);
        Task<List<KorisnikDTO>> FiltrirajKorisnikeAsync(string? ime, string? prezime);
    }
}