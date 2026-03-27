namespace WebTemplate.Repositories
{
    public interface IParticipantRepository : IGenericRepository<Participant>
    {
        Task<IEnumerable<Participant>> GetByKomitetAsync(string komitet);
        IQueryable<Participant> GetQueryable(); // Za napredno filtriranje u servisu
    }

   
}