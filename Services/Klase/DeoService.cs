using Microsoft.EntityFrameworkCore;
using WebTemplate.Models;
using WebTemplate.Repositories;

namespace WebTemplate.Services
{
    public class DeoService : IDeoService
    {
        public IDeoRepository DeoRepo { get; }

        public DeoService(IDeoRepository deoRepo)
        {
            DeoRepo = deoRepo;
        }

        public async Task KreirajAsync(Deo deo)
        {
            await DeoRepo.AddAsync(deo);
            await DeoRepo.SaveChangesAsync();
        }

        public async Task<Deo?> GetByIdAsync(int id)
        {
            return await DeoRepo.DbSet
                .Include(d => d.Sesije)
                .Include(d => d.Agenda)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<bool> IzmeniAsync(int id, Deo noviDeo)
        {
            var postojeci = await DeoRepo.GetByIdAsync(id);

            if (postojeci == null)
                return false;

            postojeci.Tip = noviDeo.Tip;
            postojeci.Datum = noviDeo.Datum;
            postojeci.VremeOd = noviDeo.VremeOd;
            postojeci.VremeDo = noviDeo.VremeDo;

            DeoRepo.Update(postojeci);
            await DeoRepo.SaveChangesAsync();

            return true;
        }

        public async Task ObrisiAsync(int id)
        {
            var deo = await DeoRepo.GetByIdAsync(id);

            if (deo != null)
            {
                DeoRepo.Delete(deo);
                await DeoRepo.SaveChangesAsync();
            }
        }
         public async Task<IEnumerable<Deo>> GetDeloviAgendeAsync(int agendaId)
        {
                return await DeoRepo.DbSet 
                    .Include(d => d.Sesije)
                    .Where(d => d.agendaId == agendaId)
                    .OrderBy(d => d.Datum)
                    .ThenBy(d => d.VremeOd)
                    .ToListAsync();
        }
    }
}