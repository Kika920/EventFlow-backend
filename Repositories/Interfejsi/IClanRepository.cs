
namespace WebTemplate.Repositories{
public interface IClanRepository : IGenericRepository<Clan>
{
    Task<IEnumerable<Clan>> GetAllClanoviAsync();
    Task<IEnumerable<Clan>> GetAllSortiranoPoZadacimaAsync();
    // Sada filtriramo po tvom Status enumu
    Task<IEnumerable<Clan>> GetClanoviPoStatusuAsync(Status status);
}}