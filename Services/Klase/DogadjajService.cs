namespace WebTemplate.Services
{
    public class DogadjajService : IDogadjajService
    {
        private readonly IDogadjajRepository Repository;

        public DogadjajService(IDogadjajRepository repository)
        {
            Repository = repository;
        }

        public async Task<IEnumerable<Dogadjaj>> GetAllAsync()
        {
            return await Repository.GetAllWithCoordinatorsAsync();
        }

        public async Task<Dogadjaj?> GetByIdAsync(int id)
        {
            return await Repository.GetWithDetailsAsync(id);
        }

        public async Task<IEnumerable<Dogadjaj>> PretraziPoImenuAsync(string ime)
        {
            return await Repository.PretraziPoImenuAsync(ime);
        }

        public async Task<Dogadjaj> KreirajDogadjajAsync(DogadjajDTO dto)
        {
            var noviDogadjaj = Mapper.Map(dto);
            
            noviDogadjaj.Agenda = new Agenda 
            { 
                Dogadjaj = noviDogadjaj,
                Delovi = new List<Deo>() 
            };

            await Repository.AddAsync(noviDogadjaj);
            await Repository.SaveChangesAsync();
            return noviDogadjaj;
        }

        public async Task<bool> UpdateAsync(int id, DogadjajDTO dto)
        {
            var postojeci = await Repository.GetByIdAsync(id);
            if (postojeci == null) return false;

            // Rucno azuriranje polja (jer staticki Mapper pravi novi objekat)
            postojeci.Ime = dto.Ime;
            postojeci.Opis = dto.Opis;
            postojeci.DatumOd = dto.DatumOd;
            postojeci.DatumDo = dto.DatumDo;
            postojeci.ImageUrl = dto.ImageUrl;

            Repository.Update(postojeci);
            await Repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ObrisiDogadjajAsync(int dogadjajId)
        {
            var dogadjaj = await Repository.GetByIdAsync(dogadjajId);
            if (dogadjaj == null) return false;

            Repository.Delete(dogadjaj);
            return await Repository.SaveChangesAsync() > 0;
        }
    }
}