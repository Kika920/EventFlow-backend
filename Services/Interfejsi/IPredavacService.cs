
namespace WebTemplate.Services
{
    public interface IPredavacService
    {
        Task<Predavac?> GetByMejlAsync(string mejl);
        Task<IEnumerable<Sesija>> GetMojeSesijeAsync(int predavacId);
        Task<bool> AzurirajLogistikuAsync(int id, string komitet, DateTime dolazak, DateTime odlazak);
        Task<IEnumerable<Predavac>> FiltrirajPredavaceAsync(string? pretraga, string? komitet, Ishrana? ishrana);
    }
}