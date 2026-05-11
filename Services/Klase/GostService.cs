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
}}