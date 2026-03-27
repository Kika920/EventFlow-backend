  
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
//da li treba po imenu ili id-u
        public IQueryable<Predavac> GetQueryable()
        {
            return DbSet.AsQueryable();
        }}
    }