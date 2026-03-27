using WebTemplate.Models;
using WebTemplate.Repositories;

namespace WebTemplate.Services
{
    public class VremeDostupnostiService : IVremeDostupnostiService
    {
        protected readonly IVremeDostupnostiRepository VremeRepo;
        protected readonly IClanRepository ClanRepo;

        public VremeDostupnostiService(
            IVremeDostupnostiRepository vremeRepo, 
            IClanRepository clanRepo)
        {
            VremeRepo = vremeRepo;
            ClanRepo = clanRepo;
        }

        // Dodavanje novog termina dostupnosti za određenog člana
        public async Task<VremeDostupnosti> DodajDostupnostAsync(int clanId, VremeDostupnosti podaci)
        {
            var clan = await ClanRepo.GetByIdAsync(clanId);
            if (clan == null) 
                throw new Exception($"Član sa ID-jem {clanId} nije pronađen.");

            // Vežemo termin za pronađenog člana
            podaci.Clan = clan;

            await VremeRepo.AddAsync(podaci);
            await VremeRepo.SaveChangesAsync();
            
            return podaci;
        }

        // Dobavljanje svih termina za jednog specifičnog člana
        public async Task<IEnumerable<VremeDostupnosti>> GetByClanIdAsync(int clanId)
        {
            return await VremeRepo.GetByClanIdAsync(clanId);
        }

        // Brisanje termina po njegovom ID-ju
        public async Task<bool> ObrisiDostupnostAsync(int id)
        {
            var stavka = await VremeRepo.GetByIdAsync(id);
            if (stavka == null) return false;

            VremeRepo.Delete(stavka); // Proveri da li se u GenericRepo zove Delete ili Remove
            await VremeRepo.SaveChangesAsync();
            return true;
        }

        // Dobavljanje apsolutno svih termina (korisno za admine/koordinatore)
        public async Task<IEnumerable<VremeDostupnosti>> GetAllAsync()
        {
            return await VremeRepo.GetAllAsync();
        }
    }
}