   namespace WebTemplate.Repositories{
     public class KoordinatorRepository : GenericRepository<Koordinator>, IKoordinatorRepository
    {
        public KoordinatorRepository(AppDbContext context) : base(context) { }

        public async Task<Koordinator?> GetWithDogadjajAsync(int id)
        {
            return await DbSet
                .Include(k => k.Dogadjaj)
                .FirstOrDefaultAsync(k => k.Id == id);
        }

        public IQueryable<Koordinator> GetQueryable()
        {
            return DbSet.AsQueryable();
        }
    }
}