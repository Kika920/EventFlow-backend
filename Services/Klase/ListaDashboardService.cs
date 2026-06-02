using WebTemplate.DTO;
using WebTemplate.Models;
using WebTemplate.Repositories;

namespace WebTemplate.Services
{
    public class ListaDashboardService : IListaDashboardService
    {
        private readonly IListaDashboardRepository _repo;

        public ListaDashboardService(
            IListaDashboardRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ListaDashboard>>
            GetMojaListaAsync(int korisnikId)
        {
            return await _repo.GetByKorisnikIdAsync(korisnikId);
        }

        public async Task<ListaDashboard> DodajAsync(
            int korisnikId,
            ListaDashboardDTO dto)
        {
            var zadatak = new ListaDashboard
            {
                Tekst = dto.Tekst,
                KorisnikId = korisnikId,
                Zavrsen = false
            };

            await _repo.AddAsync(zadatak);
            await _repo.SaveChangesAsync();

            return zadatak;
        }

        public async Task<bool> PromeniStatusAsync(int id)
        {
            try
            {
                var zadatak = await _repo.GetByIdAsync(id);

                zadatak.Zavrsen = !zadatak.Zavrsen;

                _repo.Update(zadatak);

                await _repo.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ObrisiAsync(int id)
        {
            try
            {
                var zadatak = await _repo.GetByIdAsync(id);

                _repo.Delete(zadatak);

                await _repo.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}