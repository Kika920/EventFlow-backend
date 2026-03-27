namespace WebTemplate.Repositories
{
    public interface IVremeDostupnostiRepository : IGenericRepository<VremeDostupnosti>
    {
     
        Task<IEnumerable<VremeDostupnosti>> GetByClanIdAsync(int clanId);
    }
}