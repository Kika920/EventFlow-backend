public class ChatHub : Hub
{
    private readonly IChatService _chatService;

    public ChatHub(IChatService chatService)
    {
        _chatService = chatService;
    }

    // Kada se korisnik poveže, stavljamo ga u njegovu privatnu grupu
    public override async Task OnConnectedAsync()
    {
        var userId = Context.GetHttpContext()?.Request.Query["userId"];
        if (!string.IsNullOrEmpty(userId))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"User_{userId}");
        }
        await base.OnConnectedAsync();
    }

    // Privatna poruka 1 na 1
    public async Task SendPrivateMessage(int senderId, int receiverId, string message)
    {
        // Servis radi sav posao: provera korisnika, upis u bazu i emmitovanje preko HubContext-a
        await _chatService.PosaljiPrivatnuPorukuAsync(senderId, receiverId, message);
    }

    // Grupni chat za konkretan događaj
    public async Task JoinEventGroup(int dogadjajId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"Dogadjaj_{dogadjajId}");
    }

    public async Task SendEventMessage(int senderId, int dogadjajId, string message)
    {
        // Pozivamo servis za grupni chat
        await _chatService.PosaljiPorukuDogadjajaAsync(senderId, dogadjajId, message);
    }
}