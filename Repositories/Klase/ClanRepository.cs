

namespace WebTemplate.Repositories
{
    
    public class ClanRepository : GenericRepository<Clan>, IClanRepository
    {

        public ClanRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Clan>> GetAllClanoviAsync()
        {

            return await Context.Clanovi.ToListAsync();
        }

        public async Task<IEnumerable<Clan>> GetAllSortiranoPoZadacimaAsync()
        {
            return await Context.Clanovi
                .OrderByDescending(c => c.BrojIzvrsenihZahteva)
                .ToListAsync();
        }

        public async Task<IEnumerable<Clan>> GetClanoviPoStatusuAsync(Status status)
        {
            return await Context.Clanovi
                .Where(c => c.Status == status)
                .ToListAsync();
        }
        public async Task<Clan?> GetClanByImenuIPrezimenuAsync(string ime, string prezime)
{
    return await Context.Clanovi
        .FirstOrDefaultAsync(c =>
            c.Ime.ToLower() == ime.ToLower() &&
            c.Prezime.ToLower() == prezime.ToLower());
}
    }
}