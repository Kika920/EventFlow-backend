using WebTemplate.DTO;
using WebTemplate.Models;

namespace WebTemplate.Services
{
    public interface IListaDashboardService
    {
        Task<IEnumerable<ListaDashboard>> GetMojaListaAsync(int korisnikId);

        Task<ListaDashboard> DodajAsync(
            int korisnikId,
            ListaDashboardDTO dto);

        Task<bool> PromeniStatusAsync(int id);

        Task<bool> ObrisiAsync(int id);
    }
}