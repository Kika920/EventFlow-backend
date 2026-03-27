using System.Collections.Generic;
using System.Threading.Tasks;
using WebTemplate.Models;

namespace WebTemplate.Services
{
    public interface IAgendaService
    {
        Task KreirajAgenduAsync(Agenda agenda);
        Task ObrisiAgenduSaSadrzajemAsync(int id);
        Task AzurirajVremeSaPomakomAsync(int deoId, TimeSpan novoVremeOd);
        Task<IEnumerable<Deo>> PreuzmiSlobodneSlotoveAsync(int agendaId);
    }
     //   Task<Sesija> DodajSesijuAsync(int deoId, Sesija sesija, int predavacId);
     
    
}