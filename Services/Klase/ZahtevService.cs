

namespace WebTemplate.Services
{
    public class ZahtevService : IZahtevService
    {
        
        protected readonly IZahtevRepository ZahtevRepo;
        protected readonly IClanRepository ClanRepo;
       // protected readonly IHubContext<NotificationHub> HubContext;
        protected readonly IGenericRepository<Korisnik> KorisnikRepo;
        protected readonly IGenericRepository<Dogadjaj> DogadjajRepo;
        protected readonly IGenericRepository<Koordinator> KoordinatorRepo;

        public ZahtevService(
            IZahtevRepository zahtevRepo, 
            IClanRepository clanRepo, 
          //  IHubContext<NotificationHub> hubContext,//u kontroler
            IGenericRepository<Korisnik> korisnikRepo,
            IGenericRepository<Dogadjaj> dogadjajRepo,
            IGenericRepository<Koordinator> koordinatorRepo)
        {
            ZahtevRepo = zahtevRepo;
            ClanRepo = clanRepo;
            //HubContext = hubContext;
            KorisnikRepo = korisnikRepo;
            DogadjajRepo = dogadjajRepo;
            KoordinatorRepo = koordinatorRepo;
        }

        public async Task<Zahtev> KreirajZahtevAsync(ZahtevDTO dto, int posiljalacId, int dogadjajId)
        {
            var posiljalac = await KorisnikRepo.GetByIdAsync(posiljalacId);
            var dogadjaj = await DogadjajRepo.GetByIdAsync(dogadjajId);

            var zahtev = new Zahtev
            {
                Opis = dto.Opis,
                StatusZahteva = StatusZahteva.cekanje,
                CreatedAt = DateTime.Now,
                Posiljalac = posiljalac,
                Dogadjaj = dogadjaj
            };

            await ZahtevRepo.AddAsync(zahtev);
            await ZahtevRepo.SaveChangesAsync();
            return zahtev;
        }

   
       public async Task<Zahtev> DodeliZahtevAsync(int zahtevId, List<int> clanoviIds, int koordinatorId)
{
    // 1. Učitaj zahtev sa njegovom trenutnom listom članova (Eager loading je bitan!)
    var zahtev = await ZahtevRepo.GetByIdSaNavigacijomAsync(zahtevId);
    var koordinator = await KoordinatorRepo.GetByIdAsync(koordinatorId);

    if (zahtev == null) throw new Exception("Zahtev nije pronađen.");

    // Inicijalizuj listu ako je null
    zahtev.ZaduzeniClan ??= new List<Clan>();
    zahtev.Koordinator = koordinator;
    zahtev.StatusZahteva = StatusZahteva.prihvaceno;

    foreach (var id in clanoviIds)
    {
        var clan = await ClanRepo.GetByIdAsync(id);
        if (clan != null)
        {
            // Dodaj člana u listu zahteva
            if (!zahtev.ZaduzeniClan.Any(c => c.Id == id))
            {
                zahtev.ZaduzeniClan.Add(clan);
            }
            
            // Postavi status člana na zauzet
            clan.Status = Status.Zauzet;
            ClanRepo.Update(clan);
        }
    }

    ZahtevRepo.Update(zahtev);
    await ZahtevRepo.SaveChangesAsync();
    return zahtev;
}

    
        public async Task<IEnumerable<Zahtev>> GetAllSaDetaljimaAsync()
        {
            return await ZahtevRepo.GetAllSaNavigacijomAsync();
        }

      
        public async Task<IEnumerable<Zahtev>> GetNedodeljeniAsync()
        {
            return await ZahtevRepo.GetNedodeljeniSaNavigacijomAsync();
        }

       
        public async Task<IEnumerable<Zahtev>> GetZahteviPoDogadjajuAsync(int dogadjajId)
        {
            var svi = await ZahtevRepo.GetAllSaNavigacijomAsync();
            return svi.Where(z => z.Dogadjaj != null && z.Dogadjaj.Id == dogadjajId);
        }

     
        public async Task PromeniStatusAsync(int zahtevId, StatusZahteva noviStatus)
        {
            var zahtev = await ZahtevRepo.GetByIdAsync(zahtevId);
            if (zahtev != null)
            {
                zahtev.StatusZahteva = noviStatus;
                ZahtevRepo.Update(zahtev);
                await ZahtevRepo.SaveChangesAsync();
            }
        }

       public async Task IzvrsiZahtevAsync(int zahtevId)
{
    // Obavezno koristi Include(z => z.ZaduzeniClan) u repozitorijumu!
    var zahtev = await ZahtevRepo.GetByIdSaNavigacijomAsync(zahtevId);
    
    if (zahtev != null && zahtev.ZaduzeniClan != null)
    {
        zahtev.StatusZahteva = StatusZahteva.izvrseno;
        zahtev.CompletedAt = DateTime.Now;

        foreach (var clan in zahtev.ZaduzeniClan)
        {
            // Povećaj broj izvršenih (pazi na ime propertija u modelu je BrojIzvrsenihZahteva)
            clan.BrojIzvrsenihZahteva = (clan.BrojIzvrsenihZahteva ?? 0) + 1;
            clan.Status = Status.Slobodan;
            ClanRepo.Update(clan);
        }

        ZahtevRepo.Update(zahtev);
        await ZahtevRepo.SaveChangesAsync();
    }

        }
    }
}