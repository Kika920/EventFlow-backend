
 namespace WebTemplate.Services
{
    public interface INotifikacijaService
    {
        // Šalje notifikaciju, čuva je u bazi i emituje preko SignalR-a
        Task PosaljiNotifikacijuAsync(int primalacId, string naslov, TipNotifikacije tip);

        // Vraća sve notifikacije za određenog korisnika (za njegov Inbox)
        Task<IEnumerable<Notifikacija>> PreuzmiSveZaKorisnikaAsync(int korisnikId);

        // Označava notifikaciju kao pročitanu
        Task MarkirajKaoProcitanuAsync(int id);

        // Briše staru notifikaciju
        Task ObrisiSveProcistaneZaKorisnika(int id);
    }
}
