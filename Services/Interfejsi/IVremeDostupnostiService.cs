namespace WebTemplate.Services{
    public interface IVremeDostupnostiService
{
    Task<VremeDostupnosti> DodajDostupnostAsync(int clanId, VremeDostupnosti dto);
    Task<IEnumerable<VremeDostupnosti>> GetByClanIdAsync(int clanId);
    Task<bool> ObrisiDostupnostAsync(int id);
}}