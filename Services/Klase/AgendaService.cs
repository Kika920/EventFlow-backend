namespace WebTemplate.Services{

public class AgendaService: IAgendaService
{
    public IAgendaRepository AgendaRepo { get; }
    public IDeoRepository DeoRepo { get; }

    public AgendaService(IAgendaRepository agendaRepo, IDeoRepository deoRepo)
    {
        AgendaRepo = agendaRepo;
        DeoRepo = deoRepo;
    }

    public async Task KreirajAgenduAsync(Agenda agenda)
    {
        await AgendaRepo.AddAsync(agenda);
        await AgendaRepo.SaveChangesAsync();
    }

    public async Task ObrisiAgenduSaSadrzajemAsync(int id)
    {
        var agenda = await AgendaRepo.GetAgendaSaDetaljimaAsync(id);
        if (agenda != null)
        {
            AgendaRepo.Delete(agenda);
            await AgendaRepo.SaveChangesAsync();
        }
    }

    public async Task AzurirajVremeSaPomakomAsync(int deoId, TimeSpan novoVremeOd)
    {
        var deoKojiSeMenja = await DeoRepo.GetByIdAsync(deoId);
        if (deoKojiSeMenja == null) return;

        TimeSpan pomak = novoVremeOd - deoKojiSeMenja.VremeOd;

        // Uzimamo sve delove te agende koji su hronološki posle ovog
        var deloviZaPomak = await DeoRepo.DbSet
            .Include(d => d.Sesije)
            .Where(d => d.Agenda!.Id == deoKojiSeMenja.Agenda!.Id && d.VremeOd >= deoKojiSeMenja.VremeOd)
            .OrderBy(d => d.VremeOd)
            .ToListAsync();

        foreach (var deo in deloviZaPomak)
        {
            deo.VremeOd += pomak;
            deo.VremeDo += pomak;

            // Ako u slotu postoji sesija, pomeramo i njeno vreme unutar slota
            if (deo.Sesije != null)
            {
                foreach (var s in deo.Sesije)
                {
                    if (s.VremePocetka.HasValue) s.VremePocetka += pomak;
                    if (s.VremeKraja.HasValue) s.VremeKraja += pomak;
                }
            }
            DeoRepo.Update(deo);
        }
        await DeoRepo.SaveChangesAsync();
    }

    public async Task<IEnumerable<Deo>> PreuzmiSlobodneSlotoveAsync(int agendaId)
    {
        return await DeoRepo.DbSet
            .Include(d => d.Sesije)
            .Where(d => d.Agenda!.Id == agendaId && 
                        d.Tip == TipPodeoka.sesija && 
                        (d.Sesije == null || !d.Sesije.Any()))
            .ToListAsync();
    }
    public async Task<Agenda?> PreuzmiAgenduZaDogadjajAsync(int dogadjajId)
{
    return await AgendaRepo.GetByDogadjajIdAsync(dogadjajId);
}
public async Task<Agenda?> PreuzmiAgenduAsync(int id)
{
    return await AgendaRepo.GetAgendaSaDetaljimaAsync(id);
}
}}