 namespace WebTemplate.Repositories
{public class ParticipantRepository : GenericRepository<Participant>, IParticipantRepository
    {
        public ParticipantRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Participant>> GetByKomitetAsync(string komitet)
        {
            return await DbSet.Where(p => p.Komitet == komitet).ToListAsync();
        }

        public IQueryable<Participant> GetQueryable()
        {
            return DbSet.AsQueryable();
        }

    public async Task<IEnumerable<Participant>> GetByIshranaAsync(Ishrana ishrana)
    {
        return await Context.Participanti
            .Where(p => p.Ishrana == ishrana)
            .ToListAsync();
    }

    public async Task<IEnumerable<Participant>> GetByImeIPrezimeAsync(
        string ime,
        string prezime)
    {
        return await Context.Participanti
            .Where(p =>
                p.Ime.ToLower() == ime.ToLower() &&
                p.Prezime.ToLower() == prezime.ToLower())
            .ToListAsync();
    }


    public async Task<IEnumerable<Participant>> GetByVremeDolaskaAsync(DateTime vreme)
    {
        return await Context.Participanti
            .Where(p => p.VremeDolaska.Date == vreme.Date)
            .ToListAsync();
    }

    public async Task<IEnumerable<Participant>> GetByVremeOdlaskaAsync(DateTime vreme)
    {
        return await Context.Participanti
            .Where(p => p.VremeOdlaska.Date == vreme.Date)
            .ToListAsync();
    }
public async Task<IEnumerable<Participant>> GetAllWithAlergijeAsync()
{
    return await Context.Participanti
        .Where(p => p.Alerigije != null && p.Alerigije != "")
        .ToListAsync();
}
}
    }