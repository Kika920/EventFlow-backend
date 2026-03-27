namespace WebTemplate.Repositories
{
    public interface IPredavacRepository : IGenericRepository<Predavac>
    {
        Task<Predavac?> GetByMejlAsync(string mejl);
        Task<IEnumerable<Sesija>> GetSesijeByPredavacIdAsync(int predavacId);
        IQueryable<Predavac> GetQueryable();
    }

  
}