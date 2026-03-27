
namespace WebTemplate.Repositories{
public class ChatRepository : GenericRepository<ChatMessage>, IChatRepository
{
    public ChatRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<ChatMessage>> GetChatHistoryAsync(int userA, int userB)
    {
        return await DbSet
            .Include(c => c.Sender).Include(c => c.Reciver)
            .Where(c => (c.Sender!.Id == userA && c.Reciver!.Id == userB) || 
                        (c.Sender!.Id == userB && c.Reciver!.Id == userA))
            .OrderBy(c => c.Timestamp)
            .ToListAsync();
    }

    public async Task<IEnumerable<ChatMessage>> GetEventChatAsync(int dogadjajId)
    {
        return await DbSet
            .Include(c => c.Sender)
            .Where(c => c.Dogadjaj!.Id == dogadjajId)
            .OrderBy(c => c.Timestamp)
            .ToListAsync();
    }
}}