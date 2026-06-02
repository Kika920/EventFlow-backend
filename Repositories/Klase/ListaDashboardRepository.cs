using Microsoft.EntityFrameworkCore;
using WebTemplate.Data;
using WebTemplate.Models;

namespace WebTemplate.Repositories
{
    public class ListaDashboardRepository
        : GenericRepository<ListaDashboard>,
          IListaDashboardRepository
    {
        public ListaDashboardRepository(AppDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<ListaDashboard>>
            GetByKorisnikIdAsync(int korisnikId)
        {
            return await DbSet
                .Where(x => x.KorisnikId == korisnikId)
                .OrderByDescending(x => x.DatumKreiranja)
                .ToListAsync();
        }
    }
}