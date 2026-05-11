    namespace WebTemplate.Services
{public interface IGostService
{
    Task<IEnumerable<GostDTO>> GetAllAsync();

    Task<GostDTO?> GetByIdAsync(int id);

    Task<IEnumerable<string>> GetKomitetiAsync();

    Task<IEnumerable<GostDTO>> FilterAsync(
        string? ime,
        string? prezime,
        string? komitet,
        Role? role,
        bool? imaAlergije,
        Ishrana? ishrana);
//ako su ti laksi pojedinacni filteri

Task<IEnumerable<GostDTO>> PretragaAsync(string tekst);
Task<IEnumerable<GostDTO>> GetByKomitetAsync(string komitet);

Task<IEnumerable<GostDTO>> GetByRoleAsync(Role role); //predavac ili participant

Task<IEnumerable<GostDTO>> GetSaAlergijamaAsync();

Task<IEnumerable<GostDTO>> GetByIshranaAsync(Ishrana ishrana);
Task<IEnumerable<GostDTO>> GetByDolazakAsync(DateTime datum);

Task<IEnumerable<GostDTO>> GetByOdlazakAsync(DateTime datum);

}
}