
namespace WebTemplate.Services{

    public interface IParticipantService
    {
        Task<Participant> KreirajParticipantaAsync(ParticipantDTO dto);
        Task<IEnumerable<Participant>> GetByKomitetAsync(string komitet);
        Task<bool> UpdateIshranaAsync(int id, Ishrana tip, string alergije);
       Task<IEnumerable<Participant>> FiltrirajParticipanteAsync(string? pretraga, string? komitet, Ishrana? ishrana,bool? imaAlergije);
        Task<Participant?> GetByIdAsync(int id);

Task<IEnumerable<Participant>> GetAllAsync();

Task<IEnumerable<Participant>> GetByIshranaAsync(Ishrana ishrana);

Task<IEnumerable<Participant>> GetByImeIPrezimeAsync(
    string ime,
    string prezime);
Task<IEnumerable<Participant>> GetByVremeDolaskaAsync(DateTime vreme);

Task<IEnumerable<Participant>> GetByVremeOdlaskaAsync(DateTime vreme);
Task<IEnumerable<Participant>> GetAllWithAlergijeAsync();
    }
}