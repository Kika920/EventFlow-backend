
    namespace WebTemplate.Services
{
    public interface IDogadjajService
    {
        Task<IEnumerable<Dogadjaj>> GetAllAsync();
        
        Task<Dogadjaj?> GetByIdAsync(int id);
        
       Task<bool> DaLiJeAgendaPraznaAsync(int dogadjajId);
        Task<IEnumerable<Dogadjaj>> PretraziPoImenuAsync(string ime);
        
        Task<Dogadjaj> KreirajDogadjajAsync(DogadjajDTO dto);
        
        Task<bool> UpdateAsync(int id, DogadjajDTO dto);
        
        Task<bool> ObrisiDogadjajAsync(int dogadjajId);
    }
}