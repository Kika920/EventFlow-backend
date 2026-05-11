
namespace WebTemplate.Services
{
    public interface IPredavacService
    {
        Task<Predavac?> GetByMejlAsync(string mejl);
        Task<IEnumerable<Sesija>> GetMojeSesijeAsync(int predavacId);
        Task<bool> AzurirajLogistikuAsync(int id, string komitet, DateTime dolazak, DateTime odlazak);
    Task<IEnumerable<Predavac>> FiltrirajPredavaceAsync(string? pretraga, string? komitet, Ishrana? ishrana, bool? imaAlergije);
    Task<Predavac?> GetByIdAsync(int id);

        Task<IEnumerable<Predavac>> GetAllAsync();

        Task<IEnumerable<Predavac>> GetByKomitetAsync(string komitet);

        Task<IEnumerable<Predavac>>  GetByImeIPrezimeAsync(
                string ime,
                string prezime);

        Task<IEnumerable<Predavac>> GetByVremeDolaskaAsync(DateTime vreme);

        Task<IEnumerable<Predavac>> GetByVremeOdlaskaAsync(DateTime vreme);
        Task<IEnumerable<Predavac>> GetAllWithAlergijeAsync();
    }
}