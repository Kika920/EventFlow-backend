
namespace WebTemplate.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext Context;
        
  
        public DbSet<T> DbSet { get; }

        public GenericRepository(AppDbContext context)
        {
            Context = context;
           
            DbSet = context.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id)
{
    var entity = await DbSet.FindAsync(id);

    if (entity == null)
        throw new Exception("Entity not found");

    return entity;
}
        public async Task<IEnumerable<T>> GetAllAsync() => await DbSet.ToListAsync();
        public async Task AddAsync(T entity) => await DbSet.AddAsync(entity);
        public void Update(T entity) => DbSet.Update(entity);
        public void Delete(T entity) => DbSet.Remove(entity);
        public async Task<int> SaveChangesAsync() => await Context.SaveChangesAsync();
    }
}