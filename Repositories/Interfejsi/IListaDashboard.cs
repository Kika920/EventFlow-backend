using WebTemplate.Models;

namespace WebTemplate.Repositories
{
    public interface IListaDashboardRepository
        : IGenericRepository<ListaDashboard>
    {
        Task<IEnumerable<ListaDashboard>> GetByKorisnikIdAsync(int korisnikId);
    }
}