namespace WebTemplate.Repositories
{
   public interface INotifikacijaRepository : IGenericRepository<Notifikacija>
{
    Task<IEnumerable<Notifikacija>> GetByKorisnikAsync(int korisnikId);
    Task MarkAsReadAsync(int notifikacijaId);
}
   
}