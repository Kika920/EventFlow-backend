
    namespace WebTemplate.Services
{
    public interface IDogadjajService
    {
        Task<IEnumerable<Dogadjaj>> GetAllAsync();
        
        Task<Dogadjaj?> GetByIdAsync(int id);
        
        // Nova metoda za pretragu
        Task<IEnumerable<Dogadjaj>> PretraziPoImenuAsync(string ime);
        
        Task<Dogadjaj> KreirajDogadjajAsync(DogadjajDTO dto);
        
        Task<bool> UpdateAsync(int id, DogadjajDTO dto);
        
        Task<bool> ObrisiDogadjajAsync(int dogadjajId);
    }
}