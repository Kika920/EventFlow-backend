using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebTemplate.Models;

namespace WebTemplate.Repositories
{
    public class ClanRepository : GenericRepository<Clan>, IClanRepository
    {
        public ClanRepository(AppDbContext context) : base(context)
        {
        }

        // 1. Vuče sve članove iz baze i ODMAH lepi njihovu tabelu dostupnosti
        public async Task<IEnumerable<Clan>> VratiSveSaTabelomAsync()
        {
            return await Context.Clanovi
                .Include(c => c.Tabela)
                .ToListAsync();
        }

        // 2. Vuče jednog konkretnog člana i lepi njegovu tabelu dostupnosti
        public async Task<Clan?> VratiPoIdSaTabelomAsync(int id)
        {
            return await Context.Clanovi
                .Include(c => c.Tabela)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        // 3. Pretraga po imenu i prezimenu (takođe uključujemo tabelu da bi servis sračunao status)
        public async Task<Clan?> GetClanByImenuIPrezimenuAsync(string ime, string prezime)
        {
            return await Context.Clanovi
                .Include(c => c.Tabela)
                .FirstOrDefaultAsync(c =>
                    c.Ime.ToLower() == ime.ToLower() &&
                    c.Prezime.ToLower() == prezime.ToLower());
        }
    }
}