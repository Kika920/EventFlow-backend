namespace WebTemplate.Services
{
    public class ParticipantService : IParticipantService
    {
        private readonly IParticipantRepository Repository;

        public ParticipantService(IParticipantRepository repository)
        {
            Repository = repository;
        }

        public async Task<Participant> KreirajParticipantaAsync(ParticipantDTO dto)
        {
            var participant = Mapper.Map(dto);
            
            // Logika specifična za kreiranje naloga
            participant.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            participant.Role = Role.Participant;

            await Repository.AddAsync(participant);
            await Repository.SaveChangesAsync();
            return participant;
        }

        public async Task<IEnumerable<Participant>> GetByKomitetAsync(string komitet)
        {
            return await Repository.GetByKomitetAsync(komitet);
        }

        public async Task<bool> UpdateIshranaAsync(int id, Ishrana tip, string alergije)
        {
            var participant = await Repository.GetByIdAsync(id);
            if (participant == null) return false;

            participant.Ishrana = tip;
            participant.Alerigije = alergije;

            Repository.Update(participant);
            await Repository.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Participant>> FiltrirajParticipanteAsync(string? pretraga, string? komitet, Ishrana? ishrana)
        {
            var upit = Repository.GetQueryable();

            if (!string.IsNullOrEmpty(pretraga))
            {
                // Pretraga po imenu ili prezimenu (case-insensitive)
                upit = upit.Where(p => p.Ime.ToUpper().Contains(pretraga.ToUpper()) || 
                                     p.Prezime.ToUpper().Contains(pretraga.ToUpper()));
            }

            if (!string.IsNullOrEmpty(komitet))
            {
                upit = upit.Where(p => p.Komitet == komitet);
            }

            if (ishrana.HasValue)
            {
                upit = upit.Where(p => p.Ishrana == ishrana.Value);
            }

            return await upit.ToListAsync();
        }
    }
}