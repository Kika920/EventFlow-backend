 
 namespace WebTemplate.Repositories{
public class NotifikacijaRepository : GenericRepository<Notifikacija>, INotifikacijaRepository
{
    public NotifikacijaRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Notifikacija>> GetByKorisnikAsync(int korisnikId)
    {
        return await DbSet
            .Where(n => n.Primalac!.Id == korisnikId)
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync();
    }

    public async Task MarkAsReadAsync(int notifikacijaId)
    {
        var notif = await GetByIdAsync(notifikacijaId);
        if (notif != null)
        {
            notif.Read = true;
            await Context.SaveChangesAsync();
        }
    }

    }}