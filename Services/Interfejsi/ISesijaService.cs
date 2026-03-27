namespace WebTemplate.Services
{
    public interface ISesijaService
{
    Task<bool> DodajSesijuAsync(int deoId, SesijaDTO dto);
    Task ObrisiSesiju(int id);
    Task<SesijaDTO?> PreuzmiDetaljeSesijeAsync(int id);
}
}