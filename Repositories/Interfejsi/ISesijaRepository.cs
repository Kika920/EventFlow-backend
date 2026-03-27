
namespace WebTemplate.Repositories{
public interface ISesijaRepository : IGenericRepository<Sesija>
{
    Task<Sesija?> GetSesijaSaPredavacem(int id);
}
}