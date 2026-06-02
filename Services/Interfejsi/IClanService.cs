using System.Collections.Generic;
using System.Threading.Tasks;
using WebTemplate.Models;

namespace WebTemplate.Services
{
    public interface IClanService
    {
        // Vraća sve članove, a parametar 'opadajuce' određuje da li su sortirani po broju zadataka
Task<IEnumerable<Clan>> VratiSveClanoveAsync(bool? opadajuce, Status? status);        
        
        // Menja status člana i automatski ažurira/cepa termine u tabeli dostupnosti
        Task PromeniStatusClanaAsync(int clanId, Status noviStatus);
        
        // Vraća jednog člana preko ID-ja sa osveženim trenutnim statusom (za profil)
        Task<Clan?> VratiClanaPoIdAsync(int id);
        
        // Pretraga člana po imenu i prezimenu sa osveženim trenutnim statusom
        Task<Clan?> VratiClanaPoImenuIPrezimenuAsync(string ime, string prezime);
   
    }
}