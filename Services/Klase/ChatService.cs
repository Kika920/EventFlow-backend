
namespace WebTemplate.Services{

public class ChatService : IChatService
{
    private readonly IChatRepository _chatRepo;
    private readonly IKorisnikRepository _korisnikRepo;
    private readonly IDogadjajRepository _dogadjajRepo;
    private readonly IHubContext<ChatHub> _hubContext;

    public ChatService(IChatRepository chatRepo, IKorisnikRepository korisnikRepo, IDogadjajRepository dogadjajRepo, IHubContext<ChatHub> hubContext)
    {
        _chatRepo = chatRepo;
        _korisnikRepo = korisnikRepo;
        _dogadjajRepo = dogadjajRepo;
        _hubContext = hubContext;
    }

    public async Task<ChatMessage> PosaljiPrivatnuPorukuAsync(int posiljalacId, int primalacId, string tekst)
    {
        var posiljalac = await _korisnikRepo.GetByIdAsync(posiljalacId);
        var primalac = await _korisnikRepo.GetByIdAsync(primalacId);

        var poruka = new ChatMessage {
            Sender = posiljalac,
            Reciver = primalac,
            Message = tekst,
            Timestamp = DateTime.UtcNow
        };

        await _chatRepo.AddAsync(poruka);
        await _chatRepo.SaveChangesAsync();

        // Real-time slanje primaocu
        await _hubContext.Clients.Group($"User_{primalacId}").SendAsync("ReceivePrivateMessage", posiljalacId, tekst);
        return poruka;
    }

    public async Task<ChatMessage> PosaljiPorukuDogadjajaAsync(int posiljalacId, int dogadjajId, string tekst)
    {
        var posiljalac = await _korisnikRepo.GetByIdAsync(posiljalacId);
        var dogadjaj = await _dogadjajRepo.GetByIdAsync(dogadjajId);

        var poruka = new ChatMessage {
            Sender = posiljalac,
            Dogadjaj = dogadjaj,
            Message = tekst,
            Timestamp = DateTime.UtcNow,
            Reciver = null
        };

        await _chatRepo.AddAsync(poruka);
        await _chatRepo.SaveChangesAsync();

        // Real-time slanje celoj grupi događaja
        await _hubContext.Clients.Group($"Dogadjaj_{dogadjajId}").SendAsync("ReceiveEventMessage", posiljalacId, tekst);
        return poruka;
    }

    public async Task<IEnumerable<ChatMessage>> PreuzmiIstorijuChataAsync(int korisnikA, int korisnikB) =>
        await _chatRepo.GetChatHistoryAsync(korisnikA, korisnikB);

    public async Task ObrisiPorukuAsync(int porukaId, int korisnikId)
    {
        var msg = await _chatRepo.GetByIdAsync(porukaId);
        if (msg != null && msg.Sender?.Id == korisnikId)
        {
            _chatRepo.Delete(msg);
            await _chatRepo.SaveChangesAsync();
        }
    }
}}