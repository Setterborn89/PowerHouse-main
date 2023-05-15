using PowerHouse.Server.Models;
using PowerHouse.Shared.DTO;

namespace PowerHouse.Server.Interfaces.Repository
{
    public interface IConversationRepository
    {
        Task DeleteAsync(Guid id);
        Task<Conversation> GetConversationByIdAsync(Guid id);
        Task<List<Conversation>> GetPublicConversations();
        Task<List<Conversation>> GetConversationsAsync();
        Task InsertAsync(Conversation conversation);
        Task SaveChangesAsync();
        Task UpdateAsync(Conversation conversation);
        Task<List<Conversation>> GetMostPopularConversations();
        Task RemoveUserFromConversation(UserConversationDTO input);
        Task AddUserToConversation(UserConversationDTO input);
    }
}