
namespace WebTemplate.Services;
public interface IDeoService
{
    Task KreirajAsync(Deo deo);
    Task<Deo?> GetByIdAsync(int id);
    Task<bool> IzmeniAsync(int id, Deo noviDeo);
    Task ObrisiAsync(int id);
    Task<IEnumerable<Deo>> GetDeloviAgendeAsync(int agendaId);
}