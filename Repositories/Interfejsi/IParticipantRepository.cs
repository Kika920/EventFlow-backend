namespace WebTemplate.Repositories
{
    public interface IParticipantRepository : IGenericRepository<Participant>
    {
        Task<IEnumerable<Participant>> GetByKomitetAsync(string komitet);
        IQueryable<Participant> GetQueryable(); // Za napredno filtriranje u servisu
    
Task<IEnumerable<Participant>> GetByIshranaAsync(Ishrana ishrana);

Task<IEnumerable<Participant>> GetByImeIPrezimeAsync(
    string ime,
    string prezime);

Task<IEnumerable<Participant>> GetByVremeDolaskaAsync(DateTime vreme);

Task<IEnumerable<Participant>> GetByVremeOdlaskaAsync(DateTime vreme);
Task<IEnumerable<Participant>> GetAllWithAlergijeAsync();

    }
}