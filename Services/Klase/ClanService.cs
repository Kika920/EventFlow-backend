using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebTemplate.Models;
using WebTemplate.Repositories;

namespace WebTemplate.Services
{
    public class ClanService : IClanService
    {
        protected readonly IClanRepository ClanRepo;

        public ClanService(IClanRepository clanRepo)
        {
            ClanRepo = clanRepo;
        }

        public async Task<IEnumerable<Clan>> VratiSveClanoveAsync(bool sortirajPoZadatku)
        {
            if (sortirajPoZadatku)
            {
                return await ClanRepo.GetAllSortiranoPoZadacimaAsync();
            }
            return await ClanRepo.GetAllClanoviAsync();
        }

        public async Task<IEnumerable<Clan>> VratiClanovePoStatusuAsync(Status status)
        {
            return await ClanRepo.GetClanoviPoStatusuAsync(status);
        }

        public async Task PromeniStatusClanaAsync(int clanId, Status noviStatus)
        {
            var clan = await ClanRepo.GetByIdAsync(clanId);
            if (clan == null)
            {
                throw new Exception($"Član sa ID-jem {clanId} nije pronađen.");
            }

            clan.Status = noviStatus;
            
            ClanRepo.Update(clan);
            await ClanRepo.SaveChangesAsync();
        }
    }
}