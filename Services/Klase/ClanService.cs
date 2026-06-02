using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTemplate.Models;
using WebTemplate.Repositories;

namespace WebTemplate.Services
{
    public class ClanService : IClanService
    {
        protected readonly IClanRepository ClanRepo;
        protected readonly IVremeDostupnostiRepository VremeRepo;

        public ClanService(IClanRepository clanRepo, IVremeDostupnostiRepository vremeRepo)
        {
            ClanRepo = clanRepo;
            VremeRepo = vremeRepo;
        }

        private Status IzracunajTrenutniStatus(Clan clan)
        {
            if (clan.Tabela == null || !clan.Tabela.Any())
                return Status.Nedostupan;

            var sada = DateTime.Now;
            var danasnjiDatum = sada.Date;
            var trenutnoVreme = sada.TimeOfDay;

            var trenutniTermin = clan.Tabela.FirstOrDefault(t => 
                t.Datum.Date == danasnjiDatum && 
                t.VremeOd <= trenutnoVreme && 
                t.VremeDo >= trenutnoVreme);

            return trenutniTermin != null ? trenutniTermin.JeDostupan : Status.Nedostupan;
        }

public async Task<IEnumerable<Clan>> VratiSveClanoveAsync(
    bool? opadajuce,
    Status? status)
{
    var clanovi = await ClanRepo.VratiSveSaTabelomAsync();

    foreach (var clan in clanovi)
    {
        clan.Status = IzracunajTrenutniStatus(clan);
    }

    if (status.HasValue)
    {
        clanovi = clanovi
            .Where(c => c.Status == status.Value)
            .ToList();
    }

    if (opadajuce.HasValue)
    {
        clanovi = opadajuce.Value
            ? clanovi.OrderByDescending(c => c.BrojIzvrsenihZahteva).ToList()
            : clanovi.OrderBy(c => c.BrojIzvrsenihZahteva).ToList();
    }

    return clanovi;
}
        public async Task<Clan?> VratiClanaPoIdAsync(int id)
        {
            var clan = await ClanRepo.VratiPoIdSaTabelomAsync(id);
            if (clan != null)
            {
                clan.Status = IzracunajTrenutniStatus(clan);
            }
            return clan;
        }

        public async Task<Clan?> VratiClanaPoImenuIPrezimenuAsync(string ime, string prezime)
        {
            var clan = await ClanRepo.GetClanByImenuIPrezimenuAsync(ime, prezime);
            if (clan != null)
            {
                clan.Status = IzracunajTrenutniStatus(clan);
            }
            return clan;
        }

     public async Task PromeniStatusClanaAsync(int clanId, Status noviStatus)
{
    var clan = await ClanRepo.VratiPoIdSaTabelomAsync(clanId);

    if (clan == null)
        throw new Exception("Član nije pronađen.");

    var sada = DateTime.Now;
    var danasnjiDatum = sada.Date;
    var trenutnoVreme = sada.TimeOfDay;

    var trenutniTermin = clan.Tabela?.FirstOrDefault(t =>
        t.Datum.Date == danasnjiDatum &&
        t.VremeOd <= trenutnoVreme &&
        t.VremeDo >= trenutnoVreme);

    if (trenutniTermin != null)
    {
        if (trenutniTermin.JeDostupan == noviStatus)
            return;

        if (trenutniTermin.VremeOd == trenutnoVreme)
        {
            trenutniTermin.JeDostupan = noviStatus;

            VremeRepo.Update(trenutniTermin);

            await ClanRepo.SaveChangesAsync();
            return;
        }

        var staroVremeDo = trenutniTermin.VremeDo;

        trenutniTermin.VremeDo = trenutnoVreme;

        VremeRepo.Update(trenutniTermin);

        var noviTermin = new VremeDostupnosti
        {
            Datum = danasnjiDatum,
            VremeOd = trenutnoVreme,
            VremeDo = staroVremeDo,
            JeDostupan = noviStatus,
            Clan = clan
        };

        await VremeRepo.AddAsync(noviTermin);
    }
    else
    {
        var noviTermin = new VremeDostupnosti
        {
            Datum = danasnjiDatum,
            VremeOd = trenutnoVreme,
            VremeDo = new TimeSpan(23, 59, 59),
            JeDostupan = noviStatus,
            Clan = clan
        };

        await VremeRepo.AddAsync(noviTermin);
    }

    await ClanRepo.SaveChangesAsync();
}

    }}