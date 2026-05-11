    namespace WebTemplate.Services
{public class GostService : IGostService
{
    private readonly IParticipantRepository _participantRepo;
    private readonly IPredavacRepository _predavacRepo;

    public GostService(
        IParticipantRepository participantRepo,
        IPredavacRepository predavacRepo)
    {
        _participantRepo = participantRepo;
        _predavacRepo = predavacRepo;
    }

    private async Task<List<GostDTO>> GetMergedAsync()
    {
        var participants = await _participantRepo.GetAllAsync();
        var predavaci = await _predavacRepo.GetAllAsync();

        var p = participants.Select(x => new GostDTO
        {
            Id = x.Id,
            Ime = x.Ime,
            Prezime = x.Prezime,
            Komitet = x.Komitet,
            Role = Role.Participant,
            Ishrana = x.Ishrana,
            Alergije = x.Alerigije,
            VremeDolaska = x.VremeDolaska,
            VremeOdlaska = x.VremeOdlaska
        });

        var pr = predavaci.Select(x => new GostDTO
        {
            Id = x.Id,
            Ime = x.Ime,
            Prezime = x.Prezime,
            Komitet = x.Komitet,
            Role = Role.Predavac,
            Ishrana = x.Ishrana,
            Alergije = x.Alerigije,
            VremeDolaska = x.VremeDolaska,
            VremeOdlaska = x.VremeOdlaska
        });

        return p.Concat(pr).ToList();
    }

    public async Task<IEnumerable<GostDTO>> GetAllAsync()
        => await GetMergedAsync();

    public async Task<GostDTO?> GetByIdAsync(int id)
    {
        var all = await GetMergedAsync();
        return all.FirstOrDefault(x => x.Id == id);
    }

    public async Task<IEnumerable<string>> GetKomitetiAsync()
    {
        var all = await GetMergedAsync();

        return all
            .Select(x => x.Komitet)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Distinct()
            .ToList();
    }

    public async Task<IEnumerable<GostDTO>> FilterAsync(
        string? ime,
        string? prezime,
        string? komitet,
        Role? role,
        bool? imaAlergije,
        Ishrana? ishrana)
    {
        var all = (await GetMergedAsync()).AsQueryable();

        if (!string.IsNullOrWhiteSpace(ime))
            all = all.Where(x => x.Ime.ToLower().Contains(ime.ToLower()));

        if (!string.IsNullOrWhiteSpace(prezime))
            all = all.Where(x => x.Prezime.ToLower().Contains(prezime.ToLower()));

        if (!string.IsNullOrWhiteSpace(komitet))
            all = all.Where(x => x.Komitet.ToLower().Contains(komitet.ToLower()));

        if (role.HasValue)
            all = all.Where(x => x.Role == role);

        if (ishrana.HasValue)
            all = all.Where(x => x.Ishrana == ishrana);

        if (imaAlergije.HasValue)
        {
            if (imaAlergije.Value)
                all = all.Where(x => !string.IsNullOrEmpty(x.Alergije));
            else
                all = all.Where(x => string.IsNullOrEmpty(x.Alergije));
        }

        return all.ToList();
    }
    public async Task<IEnumerable<GostDTO>> GetByKomitetAsync(string komitet)
{
    var svi = await GetMergedAsync();

    return svi.Where(x =>
        x.Komitet.ToLower().Contains(komitet.ToLower()));
}
    public async Task<IEnumerable<GostDTO>> PretragaAsync(string tekst)
{
    var svi = await GetMergedAsync();

    tekst = tekst.ToLower();

    return svi.Where(x =>
        x.Ime.ToLower().Contains(tekst) ||
        x.Prezime.ToLower().Contains(tekst));
}public async Task<IEnumerable<GostDTO>> GetByRoleAsync(Role role)
{
    var svi = await GetMergedAsync();

    return svi.Where(x => x.Role == role);
}public async Task<IEnumerable<GostDTO>> GetSaAlergijamaAsync()
{
    var svi = await GetMergedAsync();

    return svi.Where(x =>
        !string.IsNullOrWhiteSpace(x.Alergije));
}public async Task<IEnumerable<GostDTO>> GetByIshranaAsync(Ishrana ishrana)
{
    var svi = await GetMergedAsync();

    return svi.Where(x => x.Ishrana == ishrana);
}
public async Task<IEnumerable<GostDTO>> GetByDolazakAsync(DateTime datum)
{
    var svi = await GetMergedAsync();

    return svi.Where(x =>
        x.VremeDolaska.HasValue &&
        x.VremeDolaska.Value.Date == datum.Date);
}public async Task<IEnumerable<GostDTO>> GetByOdlazakAsync(DateTime datum)
{
    var svi = await GetMergedAsync();

    return svi.Where(x =>
        x.VremeOdlaska.HasValue &&
        x.VremeOdlaska.Value.Date == datum.Date);
}
}}