namespace WebTemplate.Services
{
    public class PredavacService : IPredavacService
    {
        private readonly IPredavacRepository Repository;

        public PredavacService(IPredavacRepository repository)
        {
            Repository = repository;
        }

        public async Task<Predavac?> GetByMejlAsync(string mejl)
        {
            return await Repository.GetByMejlAsync(mejl);
        }

        public async Task<IEnumerable<Sesija>> GetMojeSesijeAsync(int predavacId)
        {
            return await Repository.GetSesijeByPredavacIdAsync(predavacId);
        }

        public async Task<bool> AzurirajLogistikuAsync(int id, string komitet, DateTime dolazak, DateTime odlazak)
        {
            var predavac = await Repository.GetByIdAsync(id);
            if (predavac == null) return false;

            predavac.Komitet = komitet;
            predavac.VremeDolaska = dolazak;
            predavac.VremeOdlaska = odlazak;

            Repository.Update(predavac);
            await Repository.SaveChangesAsync();
            return true;
        }

       public async Task<IEnumerable<Predavac>> FiltrirajPredavaceAsync(string? pretraga, string? komitet, Ishrana? ishrana,
        bool? imaAlergije)
        {
            var upit = Repository.GetQueryable();

            if (!string.IsNullOrEmpty(pretraga))
            {
                // Koristimo ToUpper() za sigurniju pretragu (case-insensitive)
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
                 if (imaAlergije == true)
    {
        upit = upit.Where(p =>
            !string.IsNullOrWhiteSpace(p.Alerigije));
    }

            return await upit.ToListAsync();
        }
 

        public async Task<Predavac?> GetByIdAsync(int id)
        {
            return await Repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Predavac>>
            GetAllAsync()
        {
            return await Repository.GetAllAsync();
        }

        public async Task<IEnumerable<Predavac>>
            GetByKomitetAsync(string komitet)
        {
            return await Repository
                .GetByKomitetAsync(komitet);
        }

        public async Task<IEnumerable<Predavac>>
            GetByImeIPrezimeAsync(
                string ime,
                string prezime)
        {
            return await Repository
                .GetByImeIPrezimeAsync(
                    ime,
                    prezime);
        }

        public async Task<IEnumerable<Predavac>>
            GetByVremeDolaskaAsync(DateTime vreme)
        {
            return await Repository
                .GetByVremeDolaskaAsync(vreme);
        }

        public async Task<IEnumerable<Predavac>>
            GetByVremeOdlaskaAsync(DateTime vreme)
        {
            return await Repository
                .GetByVremeOdlaskaAsync(vreme);
        }
        public async Task<IEnumerable<Predavac>> GetAllWithAlergijeAsync()
{
    return await Repository.GetAllWithAlergijeAsync();
}
    }
}
