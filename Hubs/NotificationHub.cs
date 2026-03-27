public class NotificationHub : Hub
{
    // Korisnik se prijavljuje na svoj lični kanal pri konekciji
    public override async Task OnConnectedAsync()
    {
       
        var userId = Context.UserIdentifier; 
        if (userId != null)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"User_{userId}");
        }
        await base.OnConnectedAsync();
    }

    public async Task SendNotification(int primalacId, string title, string tip)
    {
        await Clients.Group($"User_{primalacId}").SendAsync("ReceiveNotification", new {
            Title = title,
            Tip = tip,
            CreatedAt = DateTime.Now
        });
    }
}