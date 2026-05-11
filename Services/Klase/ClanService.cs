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


    public async Task<IEnumerable<Clan>> VratiSveClanoveAsync(bool opadajuce)
{
    if (opadajuce)
    {
        return await ClanRepo.GetAllSortiranoPoZadacimaAsync(); 
        // DESC (najviše zadataka prvo)
    }
    else
    {
        return await ClanRepo.GetAllAsync()
            .ContinueWith(t => t.Result
                .OrderBy(c => c.BrojIzvrsenihZahteva));
        // ASC (najmanje zadataka prvo)
    }

}
        
        //salje nacin soritarnja i onda se soritra opadajuce/rastauce u zavisnosti od toga

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
        public async Task<Clan?> VratiClanaPoIdAsync(int id)
{
    return await ClanRepo.GetByIdAsync(id);
}

public async Task<Clan?> VratiClanaPoImenuIPrezimenuAsync(string ime, string prezime)
{
    return await ClanRepo.GetClanByImenuIPrezimenuAsync(ime, prezime);
}


    }
    }
