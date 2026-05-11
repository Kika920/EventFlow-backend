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
}}