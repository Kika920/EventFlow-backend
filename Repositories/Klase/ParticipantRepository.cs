 namespace WebTemplate.Repositories
{public class ParticipantRepository : GenericRepository<Participant>, IParticipantRepository
    {
        public ParticipantRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Participant>> GetByKomitetAsync(string komitet)
        {
            return await DbSet.Where(p => p.Komitet == komitet).ToListAsync();
        }

        public IQueryable<Participant> GetQueryable()
        {
            return DbSet.AsQueryable();
        }
    }}