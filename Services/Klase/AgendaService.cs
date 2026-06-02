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

  public async Task AzurirajVremeSaPomakomAsync(int deoId, int pomakMinuta)
{
    var deoKojiSeMenja = await DeoRepo.DbSet
        .Include(d => d.Agenda)
        .Include(d => d.Sesije)
        .FirstOrDefaultAsync(d => d.Id == deoId);

    if (deoKojiSeMenja == null)
        throw new Exception("Deo nije pronađen.");

    var pomak = TimeSpan.FromMinutes(pomakMinuta);

    var deloviZaPomak = await DeoRepo.DbSet
        .Include(d => d.Sesije)
        .Where(d =>
            d.agendaId == deoKojiSeMenja.agendaId &&
            (
                d.Datum > deoKojiSeMenja.Datum ||
                (
                    d.Datum == deoKojiSeMenja.Datum &&
                    d.VremeOd >= deoKojiSeMenja.VremeOd
                )
            )
        )
        .OrderBy(d => d.Datum)
        .ThenBy(d => d.VremeOd)
        .ToListAsync();

    foreach (var deo in deloviZaPomak)
    {
        deo.VremeOd += pomak;
        deo.VremeDo += pomak;

        if (deo.Sesije != null)
        {
            foreach (var sesija in deo.Sesije)
            {
                if (sesija.VremePocetka.HasValue)
                    sesija.VremePocetka += pomak;

                if (sesija.VremeKraja.HasValue)
                    sesija.VremeKraja += pomak;
            }
        }
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