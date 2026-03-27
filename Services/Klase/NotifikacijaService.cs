namespace WebTemplate.Services{
public class NotifikacijaService : INotifikacijaService
{
    private readonly INotifikacijaRepository _notifRepo;
    private readonly IKorisnikRepository _korisnikRepo;
    private readonly IHubContext<NotificationHub> _hubContext;

    public NotifikacijaService(
        INotifikacijaRepository notifRepo, 
        IKorisnikRepository korisnikRepo,
        IHubContext<NotificationHub> hubContext)
    {
        _notifRepo = notifRepo;
        _korisnikRepo = korisnikRepo;
        _hubContext = hubContext;
    }
public async Task<IEnumerable<Notifikacija>> PreuzmiSveZaKorisnikaAsync(int korisnikId) => 
        await _notifRepo.GetByKorisnikAsync(korisnikId);
        public async Task ObrisiSveProcistaneZaKorisnika(int korisnikId)
{
    var zaBrisanje = await _notifRepo.DbSet
        .Where(n => n.Primalac!.Id == korisnikId && n.Read == true)
        .ToListAsync();

    foreach (var n in zaBrisanje)
    {
        _notifRepo.Delete(n);
    }
    await _notifRepo.SaveChangesAsync();
}
    public async Task PosaljiNotifikacijuAsync(int primalacId, string naslov, TipNotifikacije tip)
    {
        var primalac = await _korisnikRepo.GetByIdAsync(primalacId);
        if (primalac == null) return;

        // 1. Kreiranje objekta za bazu
        var notifikacija = new Notifikacija
        {
            Primalac = primalac,
            Title = naslov,
            Tip = tip,
            CreatedAt = DateTime.Now,
            Read = false
        };

        // 2. Upis u bazu preko repozitorijuma
        await _notifRepo.AddAsync(notifikacija);
        await _notifRepo.SaveChangesAsync();

        // 3. Slanje u realnom vremenu preko SignalR-a (Hub-a)
        // Koristimo IHubContext da bismo mogli slati notifikacije izvan samog Hub-a
        await _hubContext.Clients.Group($"User_{primalacId}")
            .SendAsync("ReceiveNotification", new 
            { 
                Id = notifikacija.Id,
                Title = naslov, 
                Tip = tip.ToString(),
                CreatedAt = notifikacija.CreatedAt 
            });
    }

    public async Task MarkirajKaoProcitanuAsync(int id)
    {
        await _notifRepo.MarkAsReadAsync(id);
    }
}}