using System.Collections.Generic;
using System.Threading.Tasks;
using WebTemplate.Models;

namespace WebTemplate.Repositories
{
    public interface IClanRepository : IGenericRepository<Clan>
    {
        // Nove metode koje su nam neophodne za sinhronizaciju sa satnicom
        Task<IEnumerable<Clan>> VratiSveSaTabelomAsync();
        Task<Clan?> VratiPoIdSaTabelomAsync(int id);
        
        // Metode koje si već imala, prilagođene za rad sa servisom
        Task<Clan?> GetClanByImenuIPrezimenuAsync(string ime, string prezime);
    }
}