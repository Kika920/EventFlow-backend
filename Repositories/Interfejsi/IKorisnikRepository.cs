
namespace WebTemplate.Repositories
{
public interface IKorisnikRepository : IGenericRepository<Korisnik>
{
    Task<Korisnik?> GetByUsernameAsync(string username);
    Task<IEnumerable<Korisnik>> SearchByNameAsync(string ime, string prezime);
    Task<IEnumerable<Korisnik>> GetByRoleAsync(Role role);
    Task<List<Korisnik>> GetFiltriraniKorisniciAsync(string? ime, string? prezime);
}}