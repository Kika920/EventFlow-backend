
namespace WebTemplate.Services{

    public interface IParticipantService
    {
        Task<Participant> KreirajParticipantaAsync(ParticipantDTO dto);
        Task<IEnumerable<Participant>> GetByKomitetAsync(string komitet);
        Task<bool> UpdateIshranaAsync(int id, Ishrana tip, string alergije);
        Task<IEnumerable<Participant>> FiltrirajParticipanteAsync(string? pretraga, string? komitet, Ishrana? ishrana);
    }
}