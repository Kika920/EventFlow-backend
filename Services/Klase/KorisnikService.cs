using Microsoft.EntityFrameworkCore;
using WebTemplate.Models;
using WebTemplate.DTO;

namespace WebTemplate.Services
{
    public class KorisnikService : IKorisnikService
    {
        
        private readonly IKorisnikRepository Repozitorijum; 

      
        public KorisnikService( IKorisnikRepository repozitorijum)
        {
            
            Repozitorijum = repozitorijum;
        }

        // Vrati sve korisnike iz baze
        public async Task<IEnumerable<Korisnik>> GetAllAsync()
        {
            return await Repozitorijum.GetAllAsync();
        }

        // Pronađi korisnika po ID-ju
        public async Task<Korisnik?> GetByIdAsync(int id)
        {
         return await Repozitorijum.GetByIdAsync(id);
        }

        // Ažuriranje podataka korisnika
        public async Task<bool> UpdateAsync(int id, KorisnikDTO dto)
        {
            var Postojeci =  await Repozitorijum.GetByIdAsync(id);

            if (Postojeci == null) return false;

            // Ažuriramo zajednička polja
            Postojeci.Ime = dto.Ime;
            Postojeci.Prezime = dto.Prezime;
            Postojeci.Mejl = dto.Mejl;
            Postojeci.BrojTelefona = dto.BrojTelefona;
            Postojeci.ImageUrl = dto.ImageUrl;

            // Napomena: Username i Role se obično ne menjaju kroz običan Update profil
            // Lozinka se menja kroz posebnu ResetPassword metodu zbog heširanja

            return await Repozitorijum.SaveChangesAsync() > 0;
        }

        // Potpuno brisanje korisnika iz baze (Hard Delete)
        public async Task<bool> ObrisiKorisnikaAsync(int id)
        {
            try
            {
                var KorisnikZaBrisanje = await Repozitorijum.GetByIdAsync(id);

                if (KorisnikZaBrisanje == null) return false;

                // Entity Framework će automatski prepoznati da li je u pitanju 
                // Clan, Predavac ili Participant i obrisati ga iz odgovarajućih tabela
                Repozitorijum.Delete(KorisnikZaBrisanje);
                
                return await Repozitorijum.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                // Ako baza baci grešku zbog Foreign Key-a (npr. Predavač je na Sesiji)
                throw new Exception($"Ne mogu da obrišem korisnika jer je povezan sa drugim podacima. Detalji: {ex.Message}");
            }
        }
    
public async Task<List<KorisnikDTO>> FiltrirajKorisnikeAsync(string? ime, string? prezime)
{
    var korisnici = await Repozitorijum.GetFiltriraniKorisniciAsync(ime, prezime);
    var listaDto = new List<KorisnikDTO>();

    foreach (var k in korisnici)
    {
        switch (k)
        {
            case Koordinator koord:
                listaDto.Add(new WebTemplate.DTO.KoordinatorDTO {
                    Ime = koord.Ime, Prezime = koord.Prezime, Tip = koord.Tip,
                    Username = koord.Username, Mejl = koord.Mejl, Role = koord.Role,
                    BrojTelefona = koord.BrojTelefona, ImageUrl = koord.ImageUrl
                });
                break;

            case Participant part:
                listaDto.Add(new ParticipantDTO {
                    Ime = part.Ime, Prezime = part.Prezime, Komitet = part.Komitet,
                    Ishrana = part.Ishrana, VremeDolaska = part.VremeDolaska,
                    VremeOdlaska = part.VremeOdlaska, Username = part.Username,
                    Mejl = part.Mejl, Role = part.Role, BrojTelefona = part.BrojTelefona
                });
                break;

            case Predavac pred:
                listaDto.Add(new PredavacDTO {
                    Ime = pred.Ime, Prezime = pred.Prezime, Komitet = pred.Komitet,
                    VremeDolaska = pred.VremeDolaska, VremeOdlaska = pred.VremeOdlaska,
                    Username = pred.Username, Mejl = pred.Mejl, Role = pred.Role,
                    BrojTelefona = pred.BrojTelefona
                });
                break;

            case Clan clan:
                // Pretpostavka da si dodala ClanDTO u prethodnom koraku
                listaDto.Add(new ClanDTO {
                    Ime = clan.Ime, Prezime = clan.Prezime, 
                    BrojIzvrsenihZahteva = clan.BrojIzvrsenihZahteva,
                    Username = clan.Username, Mejl = clan.Mejl, Role = clan.Role,
                    BrojTelefona = clan.BrojTelefona
                });
                break;

            default:
                listaDto.Add(new KorisnikDTO {
                    Ime = k.Ime, Prezime = k.Prezime, Username = k.Username,
                    Mejl = k.Mejl, Role = k.Role, BrojTelefona = k.BrojTelefona
                });
                break;
        }
    }
    return listaDto;
}
    }
}