
using PowerHouse.Server.Models;

namespace PowerHouse.Server.Interfaces.Repository
{
    public interface IMessageRepository
    {
        Task<List<Message>> GetAllMessagesAsync();
        Task<List<Message>> GetMessagesByConversationId(Guid id);
        Task<Message> GetMessageByIdAsync(Guid id);
        Task<Message> PostMessage(Message message);
        Task UpdateMessage(Message message);
        Task DeleteMessage(Guid id);
        Task SaveChangesAsync();
    }
}
