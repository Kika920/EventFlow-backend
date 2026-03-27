 namespace WebTemplate.Repositories
{public interface IDogadjajRepository : IGenericRepository<Dogadjaj>
    {
        Task<IEnumerable<Dogadjaj>> PretraziPoImenuAsync(string ime);
        Task<Dogadjaj?> GetWithDetailsAsync(int id);
        Task<IEnumerable<Dogadjaj>> GetAllWithCoordinatorsAsync();
    }}