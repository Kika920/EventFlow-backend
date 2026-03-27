using Microsoft.EntityFrameworkCore;
using WebTemplate.Models;
using WebTemplate.Data;

namespace WebTemplate.Repositories
{
    public class VremeDostupnostiRepository : GenericRepository<VremeDostupnosti>, IVremeDostupnostiRepository
    {
        public VremeDostupnostiRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<VremeDostupnosti>> GetByClanIdAsync(int clanId)
        {
            return await Context.TabeleDostupnosti
                .Where(v => v.Clan != null && v.Clan.Id == clanId)
                .ToListAsync();
        }
    }
}