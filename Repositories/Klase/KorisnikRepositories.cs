namespace WebTemplate.Repositories
{
    public class KorisnikRepository : GenericRepository<Korisnik>, IKorisnikRepository
{
    public KorisnikRepository(AppDbContext context) : base(context) { }

    public async Task<Korisnik?> GetByUsernameAsync(string username) =>
        await DbSet.FirstOrDefaultAsync(u => u.Username == username);

    public async Task<IEnumerable<Korisnik>> SearchByNameAsync(string ime, string prezime) =>
        await DbSet.Where(u => u.Ime.Contains(ime) && u.Prezime.Contains(prezime)).ToListAsync();

    public async Task<IEnumerable<Korisnik>> GetByRoleAsync(Role role) =>
        await DbSet.Where(u => u.Role == role).ToListAsync();
        public async Task<List<Korisnik>> GetFiltriraniKorisniciAsync(string? ime, string? prezime)
        {
            var query = Context.Korisnici.AsQueryable();

            if (!string.IsNullOrWhiteSpace(ime))
                query = query.Where(k => k.Ime.Contains(ime));

            if (!string.IsNullOrWhiteSpace(prezime))
                query = query.Where(k => k.Prezime.Contains(prezime));

            return await query.ToListAsync();
        }
      
}}