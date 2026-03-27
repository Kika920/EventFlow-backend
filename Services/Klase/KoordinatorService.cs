namespace WebTemplate.Services
{
    public class KoordinatorService : IKoordinatorService
    {
        private readonly IKoordinatorRepository Repository;

        public KoordinatorService(IKoordinatorRepository repository)
        {
            Repository = repository;
        }

        public async Task<IEnumerable<Koordinator>> GetAllAsync()
        {
            return await Repository.GetAllAsync();
        }

        public async Task<Koordinator?> GetByIdAsync(int id)
        {
            return await Repository.GetWithDogadjajAsync(id);
        }

        public async Task<IEnumerable<Koordinator>> FiltrirajKoordinatoreAsync(TipKoordinatora? tip)
        {
            var upit = Repository.GetQueryable();

            if (tip.HasValue)
            {
                upit = upit.Where(k => k.Tip == tip.Value);
            }

            return await upit.ToListAsync();
        }

        public async Task<Koordinator> KreirajKoordinatoraAsync(KoordinatorDTO dto)
        {
            var noviKoordinator = Mapper.Map(dto);
            
            // Logika za bezbednost i uloge
            noviKoordinator.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            noviKoordinator.Role = Role.Koordinator;

            await Repository.AddAsync(noviKoordinator);
            await Repository.SaveChangesAsync();
            return noviKoordinator;
        }

        public async Task<bool> UpdateTipAsync(int id, TipKoordinatora noviTip)
        {
            var koordinator = await Repository.GetByIdAsync(id);
            if (koordinator == null) return false;

            koordinator.Tip = noviTip;
            
            Repository.Update(koordinator);
            await Repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ObrisiKoordinatoraAsync(int id)
        {
            var koordinator = await Repository.GetByIdAsync(id);
            if (koordinator == null) return false;

            Repository.Delete(koordinator);
            return await Repository.SaveChangesAsync() > 0;
        }
    }
}