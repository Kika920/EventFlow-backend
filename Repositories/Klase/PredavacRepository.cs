  
  namespace WebTemplate.Repositories{
  public class PredavacRepository : GenericRepository<Predavac>, IPredavacRepository
    {
        public PredavacRepository(AppDbContext context) : base(context) { }

        public async Task<Predavac?> GetByMejlAsync(string mejl)
        {
            return await DbSet.FirstOrDefaultAsync(p => p.Mejl == mejl);
        }

        public async Task<IEnumerable<Sesija>> GetSesijeByPredavacIdAsync(int predavacId)
        {
            return await Context.Sesije
                .Where(s => s.Predavac.Id == predavacId)
                .ToListAsync();
        }

       public IQueryable<Predavac> GetQueryable()
        {
            return DbSet.AsQueryable();
        }
           public async Task<IEnumerable<Predavac>>
            GetByKomitetAsync(string komitet)
        {
            return await Context.Predavaci
                .Where(p =>
                    p.Komitet.ToLower() ==
                    komitet.ToLower())
                .ToListAsync();
        }

        public async Task<IEnumerable<Predavac>>
            GetByImeIPrezimeAsync(
                string ime,
                string prezime)
        {
            return await Context.Predavaci
                .Where(p =>
                    p.Ime.ToLower() == ime.ToLower() &&
                    p.Prezime.ToLower() ==
                    prezime.ToLower())
                .ToListAsync();
        }

        public async Task<IEnumerable<Predavac>>
            GetByVremeDolaskaAsync(DateTime vreme)
        {
            return await Context.Predavaci
                .Where(p =>
                    p.VremeDolaska.Date ==
                    vreme.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<Predavac>>
            GetByVremeOdlaskaAsync(DateTime vreme)
        {
            return await Context.Predavaci
                .Where(p =>
                    p.VremeOdlaska.Date ==
                    vreme.Date)
                .ToListAsync();
        }
public async Task<IEnumerable<Predavac>> GetAllWithAlergijeAsync()
{
    return await Context.Predavaci
        .Where(p => p.Alerigije != null && p.Alerigije != "")
        .ToListAsync();
}
    }
    }