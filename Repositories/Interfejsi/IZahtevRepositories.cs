using System.Collections.Generic;
using System.Threading.Tasks;
using WebTemplate.Models;

namespace WebTemplate.Repositories
{
    // Nasleđuje IGenericRepository da bi imao Add, Update, Delete...
    // Ali dodajemo specifične metode koje trebaju koordinatoru
    public interface IZahtevRepository : IGenericRepository<Zahtev>
    {
        // Metoda za dobijanje svih zahteva sa podacima o članu, pošiljaocu i događaju
        Task<IEnumerable<Zahtev>> GetAllSaNavigacijomAsync();

        // Metoda za dobijanje jednog zahteva sa svim detaljima
        Task<Zahtev?> GetByIdSaNavigacijomAsync(int id);

        // Metoda koja filtrira samo one zahteve koji čekaju na dodelu
        Task<IEnumerable<Zahtev>> GetNedodeljeniSaNavigacijomAsync();
    }
}