using System.Collections.Generic;
using System.Threading.Tasks;
using WebTemplate.Models;

namespace WebTemplate.Services
{
    public interface IAgendaService
    {
        Task KreirajAgenduAsync(Agenda agenda);
        Task ObrisiAgenduSaSadrzajemAsync(int id);
        Task AzurirajVremeSaPomakomAsync(int deoId, int pomakMinuta);
        Task<IEnumerable<Deo>> PreuzmiSlobodneSlotoveAsync(int agendaId);
     Task<Agenda?> PreuzmiAgenduZaDogadjajAsync(int dogadjajId);
     Task<Agenda?> PreuzmiAgenduAsync(int id); //id agende
    }
     //   Task<Sesija> DodajSesijuAsync(int deoId, Sesija sesija, int predavacId);
     
    
}