using System.Collections.Generic;
using System.Threading.Tasks;
using WebTemplate.Models;

namespace WebTemplate.Services
{
    public interface IClanService
    {
        Task<IEnumerable<Clan>> VratiSveClanoveAsync(bool sortirajPoZadatku);
        Task<IEnumerable<Clan>> VratiClanovePoStatusuAsync(Status status);
        Task PromeniStatusClanaAsync(int clanId, Status noviStatus);
    }
}
