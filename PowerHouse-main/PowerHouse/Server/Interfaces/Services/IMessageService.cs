using PowerHouse.Shared.DTO;

namespace PowerHouse.Server.Interfaces.Services
{
    public interface IMessageService
    {
        Task<MessageDTO> DeleteMessage(Guid id);
        Task<MessageDTO> GetMessageById(Guid id);
        Task<List<MessageDTO>> GetAllMessages();
        Task<List<MessageDTO>> GetMessagesByConversationId(Guid id);
        Task<MessageDTO> PostMessage(PostMessageDTO message);
        Task<MessageDTO>UpdateMessage(MessageDTO message);
        Task <string> EncryptTextMessage(string message);
        Task<string> DecryptTextMessage(string message);
    }
}
