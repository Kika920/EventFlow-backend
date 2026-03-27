namespace WebTemplate.Services

{
    public interface IKoordinatorService
    {
        Task<IEnumerable<Koordinator>> GetAllAsync();
        Task<Koordinator?> GetByIdAsync(int id);
        Task<IEnumerable<Koordinator>> FiltrirajKoordinatoreAsync(TipKoordinatora? tip);
        Task<Koordinator> KreirajKoordinatoraAsync(KoordinatorDTO dto);
        Task<bool> UpdateTipAsync(int id, TipKoordinatora noviTip);
        Task<bool> ObrisiKoordinatoraAsync(int id);
    }
}
