

namespace WebTemplate.Services
{
    public interface IZahtevService
    {
        Task<Zahtev> KreirajZahtevAsync(ZahtevDTO dto, int posiljalacId, int dogadjajId);
        Task<Zahtev> DodeliZahtevAsync(int zahtevId, List<int> clanoviIds, int koordinatorId);
        Task<IEnumerable<Zahtev>> GetAllSaDetaljimaAsync();
        Task<IEnumerable<Zahtev>> GetNedodeljeniAsync();
        Task<IEnumerable<Zahtev>> GetZahteviPoDogadjajuAsync(int dogadjajId);
        Task PromeniStatusAsync(int zahtevId, StatusZahteva noviStatus);
        Task IzvrsiZahtevAsync(int zahtevId);
    }
}