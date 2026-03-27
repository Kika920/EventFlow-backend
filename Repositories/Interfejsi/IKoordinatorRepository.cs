namespace WebTemplate.Repositories
{
    public interface IKoordinatorRepository : IGenericRepository<Koordinator>
    {
        Task<Koordinator?> GetWithDogadjajAsync(int id);
        IQueryable<Koordinator> GetQueryable();
    }

}