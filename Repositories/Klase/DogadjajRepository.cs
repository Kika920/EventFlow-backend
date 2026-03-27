namespace WebTemplate.Repositories
{
   

    public class DogadjajRepository : GenericRepository<Dogadjaj>, IDogadjajRepository
    {
        public DogadjajRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Dogadjaj>> PretraziPoImenuAsync(string ime)
        {
            return await DbSet
                .Where(d => d.Ime.ToUpper().Contains(ime.ToUpper()))
                .ToListAsync();
        }

        public async Task<Dogadjaj?> GetWithDetailsAsync(int id)
        {
            return await DbSet
                .Include(d => d.Agenda)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<Dogadjaj>> GetAllWithCoordinatorsAsync()
        {
            return await DbSet
                .Include(d => d.Koordinatori)
                .ToListAsync();
        }
    }
}