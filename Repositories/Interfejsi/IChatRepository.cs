namespace WebTemplate.Repositories
{
    public interface IChatRepository : IGenericRepository<ChatMessage>
{

    Task<IEnumerable<ChatMessage>> GetChatHistoryAsync(int senderId, int receiverId);
   // Task<IEnumerable<ChatMessage>> GetEventChatAsync(int dogadjajId);
}}