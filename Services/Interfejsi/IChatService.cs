namespace WebTemplate.Services
{
    public interface IChatService
    {
        // Čuva privatnu poruku i šalje je primaocu real-time
        Task<ChatMessage> PosaljiPrivatnuPorukuAsync(int posiljalacId, int primalacId, string tekst);

        // Čuva poruku vezanu za događaj i šalje je celoj grupi
        Task<ChatMessage> PosaljiPorukuDogadjajaAsync(int posiljalacId, int dogadjajId, string tekst);

        // Vraća istoriju chata između dva korisnika
        Task<IEnumerable<ChatMessage>> PreuzmiIstorijuChataAsync(int korisnikA, int korisnikB);

        // Briše poruku (obično dozvoljeno samo pošiljaocu)
        Task ObrisiPorukuAsync(int porukaId, int korisnikId);
    }
}
