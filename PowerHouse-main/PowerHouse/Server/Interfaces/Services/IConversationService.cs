using Microsoft.AspNetCore.Mvc;
using PowerHouse.Shared.DTO;

namespace PowerHouse.Server.Interfaces.Services
{
    public interface IConversationService
    {
        Task Delete(Guid id);
        Task<List<ConversationDTO>> GetConversations();
        Task<List<ConversationDTO>> GetPublicConversations();
        Task<ConversationDTO> GetConversationById(Guid id);
        Task Insert(CreateTopicDTO conversationDTO);
        Task UpdateAsync(ConversationDTO conversationDTO);
        Task<ActionResult<List<ConversationDTO>>> GetMostPopularConversations();
        Task RemoveUserFromConversation(UserConversationDTO input);
        Task AddUserToConversation(UserConversationDTO input);
    }
}