using Microsoft.EntityFrameworkCore;
using WebTemplate.Data;
using WebTemplate.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTemplate.Repositories
{
    // Nasleđujemo GenericRepository da bismo imali Add, Update, Delete
    // Implementiramo IZahtevRepository za specifične metode logistike
    public class ZahtevRepository : GenericRepository<Zahtev>, IZahtevRepository
    {
        // Konstruktor koji šalje context roditelju (GenericRepository)
        public ZahtevRepository(AppDbContext context) : base(context)
        {
        }

        // Vraća sve zahteve sa uključenim podacima o pošiljaocu, članu i događaju
        public async Task<IEnumerable<Zahtev>> GetAllSaNavigacijomAsync()
        {
            // Koristimo Context (veliko slovo, bez donje crte) jer je protected u roditelju
            return await Context.Zahtevi
                .Include(z => z.Posiljalac)      // Spaja tabelu Korisnici
                .Include(z => z.ZaduzeniClan)    // Spaja tabelu Clanovi
                .Include(z => z.Dogadjaj)        // Spaja tabelu Dogadjaji
                .OrderByDescending(z => z.CreatedAt)
                .ToListAsync();
        }

        // Vraća jedan specifičan zahtev sa svim detaljima (korisno za pregled pojedinačnog kvara)
        public async Task<Zahtev?> GetByIdSaNavigacijomAsync(int id)
        {
            return await Context.Zahtevi
                .Include(z => z.Posiljalac)
                .Include(z => z.ZaduzeniClan)
                .Include(z => z.Dogadjaj)
                .FirstOrDefaultAsync(z => z.Id == id);
        }

        // Vraća listu zahteva koji još uvek čekaju da ih koordinator nekome dodeli
        public async Task<IEnumerable<Zahtev>> GetNedodeljeniSaNavigacijomAsync()
        {
            return await Context.Zahtevi
                .Include(z => z.Posiljalac)
                .Include(z => z.Dogadjaj)
                .Where(z => z.ZaduzeniClan == null && z.StatusZahteva == StatusZahteva.cekanje)
                .OrderBy(z => z.CreatedAt)
                .ToListAsync();
        }
    }
}