using Microsoft.EntityFrameworkCore;
using WebTemplate.Models;

namespace WebTemplate.Repositories
{
public class AgendaRepository : GenericRepository<Agenda>, IAgendaRepository
{
    public AgendaRepository(AppDbContext context) : base(context) { }

    public async Task<Agenda?> GetAgendaSaDetaljimaAsync(int id)
    {
        return await DbSet
            .Include(a => a.Delovi!)
                .ThenInclude(d => d.Sesije)
            .FirstOrDefaultAsync(a => a.Id == id);
    }
}
/*
        public async Task<List<Deo>> GetDeloviNakonVremenaAsync(int agendaId, DateTime datum, TimeSpan vremeOd)
        {
            // Koristimo Context.Set<Deo>() jer GenericRepository radi sa Agendom, a ovde nam trebaju Delovi
            return await Context.Set<Deo>()
                .Where(d => d.Agenda.Id == agendaId && 
                            d.Datum.Date == datum.Date && 
                            d.VremeOd >= vremeOd)
                .OrderBy(d => d.VremeOd)
                .ToListAsync();
        }

        public async Task<Deo?> GetDeoSaSesijamaAsync(int deoId)
        {
            return await Context.Set<Deo>()
                .Include(d => d.Agenda)
                    .ThenInclude(a => a.Dogadjaj)
                .Include(d => d.Sesije)
                .FirstOrDefaultAsync(d => d.Id == deoId);
        }
    }
    */
}