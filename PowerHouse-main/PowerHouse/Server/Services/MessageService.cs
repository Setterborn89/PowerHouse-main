using AutoMapper;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PowerHouse.Server.AES_Encryption;
using PowerHouse.Server.Interfaces.Repository;
using PowerHouse.Server.Interfaces.Services;
using PowerHouse.Server.Models;
using PowerHouse.Shared.DTO;
using System.Text;

namespace PowerHouse.Server.Services
{
    public class MessageService : IMessageService
    {
		private static (byte[] key, byte[] iv) keyAndIv;
		private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;

        private static bool ValidateMessage(MessageDTO message)
        {
            if (message == null)
            {
                return false;
            }
            return true;
        }
        public MessageService(IMessageRepository messageRepository, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
        }

        public async Task<MessageDTO> DeleteMessage(Guid id)
        {
            await _messageRepository.DeleteMessage(id);
            await _messageRepository.SaveChangesAsync();
            MessageDTO deletedMessage = _mapper.Map<Message, MessageDTO>(await _messageRepository.GetMessageByIdAsync(id));
            if (deletedMessage.IsDeleted) deletedMessage.Text = "This message has been deleted";
            return deletedMessage;
        }

        public async Task<List<MessageDTO>> GetAllMessages()
        {
            List<MessageDTO> messages = _mapper.Map<List<Message>, List<MessageDTO>>(await _messageRepository.GetAllMessagesAsync());

            foreach (var message in messages)
            {
                var decryptedMessage = await DecryptTextMessage(message.Text);
                message.Text = decryptedMessage;
                if (message.IsBlocked) message.Text = "This message has been reported";
                if (message.IsDeleted) message.Text = "This message has been deleted";
            }
            return messages;
        }

        public async Task<List<MessageDTO>> GetMessagesByConversationId(Guid id)
        {
            IEnumerable<MessageDTO> messages = _mapper.Map<IEnumerable<Message>, IEnumerable<MessageDTO>>(await _messageRepository.GetMessagesByConversationId(id));
            foreach (var message in messages)
            {
                var decryptedMessage = await DecryptTextMessage(message.Text);
                message.Text = decryptedMessage;
                if (message.IsBlocked) message.Text = "This message has been reported";
                if (message.IsDeleted) message.Text = "This message has been deleted";
            }
            return messages.ToList();
        }

        public async Task<MessageDTO> GetMessageById(Guid id)
        {
            MessageDTO message = _mapper.Map<Message, MessageDTO>(await _messageRepository.GetMessageByIdAsync(id));
            var decryptedMessage = await DecryptTextMessage(message.Text);
            message.Text = decryptedMessage;
            if (message.IsBlocked) message.Text = "This message has been reported";
            if (message.IsDeleted) message.Text = "This message has been deleted";
            
            return message;

        }

        public async Task<MessageDTO> PostMessage(PostMessageDTO messageDTO)
        {
           
            if (messageDTO == null)
            {
                throw new Exception();
            }
            ;
            messageDTO.CreateDate = DateTime.UtcNow;
           
           messageDTO.Text = await EncryptTextMessage(messageDTO.Text);

			Message encryptedMessage = _mapper.Map<PostMessageDTO, Message>(messageDTO);
            MessageDTO postedMessage = _mapper.Map<Message, MessageDTO>(await _messageRepository.PostMessage(encryptedMessage));
            
            await _messageRepository.SaveChangesAsync();
            postedMessage.Text = await DecryptTextMessage(postedMessage.Text);
            return postedMessage;
        }

        public async Task<MessageDTO> UpdateMessage(MessageDTO messageDTO)
        {
            if (!ValidateMessage(messageDTO))
            {
                throw new Exception();
            }
            var updateMessage = await GetMessageById(messageDTO.Id);
            
			updateMessage.Text = await EncryptTextMessage(messageDTO.Text);
			Message message = _mapper.Map<MessageDTO, Message>(updateMessage);

            await _messageRepository.UpdateMessage(message);
            await _messageRepository.SaveChangesAsync();
            MessageDTO editMessage = await GetMessageById(message.Id);
			
			return editMessage;
        }

        public async Task<string> EncryptTextMessage(string message)
        {
            if (keyAndIv == default)
            {
                keyAndIv = AESEncryption.GenerateKeyAndIV();
            }

            var messageBytes = Encoding.UTF8.GetBytes(message);
            var encryptedMessage = AESEncryption.Encrypt(messageBytes, keyAndIv.key, keyAndIv.iv);

            message = Convert.ToBase64String(encryptedMessage);
            return message;
        }

        public async Task<string> DecryptTextMessage(string message)
        {
            if (keyAndIv == default)
            {
                keyAndIv = AESEncryption.GenerateKeyAndIV();
            }

            byte[] encryptedData = Convert.FromBase64String(message);
            byte[] decryptedData = AESEncryption.Decrypt(encryptedData, keyAndIv.key, keyAndIv.iv);
            message = Encoding.UTF8.GetString(decryptedData);
            return message;
        }
    }
}
