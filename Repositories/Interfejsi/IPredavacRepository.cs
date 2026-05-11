namespace WebTemplate.Repositories
{
    public interface IPredavacRepository : IGenericRepository<Predavac>
    {
        Task<Predavac?> GetByMejlAsync(string mejl);
        Task<IEnumerable<Sesija>> GetSesijeByPredavacIdAsync(int predavacId);
       IQueryable<Predavac> GetQueryable();
        Task<IEnumerable<Predavac>> GetByKomitetAsync(string komitet);

        Task<IEnumerable<Predavac>> GetByImeIPrezimeAsync(
            string ime,
            string prezime);

        Task<IEnumerable<Predavac>> GetByVremeDolaskaAsync(
            DateTime vreme);

        Task<IEnumerable<Predavac>> GetByVremeOdlaskaAsync(DateTime vreme);
    
    Task<IEnumerable<Predavac>> GetAllWithAlergijeAsync();

    }
}