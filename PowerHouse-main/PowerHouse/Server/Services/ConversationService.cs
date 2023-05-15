using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PowerHouse.Server.Interfaces.Repository;
using PowerHouse.Server.Interfaces.Services;
using PowerHouse.Server.Models;
using PowerHouse.Shared.DTO;
using Conversation = PowerHouse.Server.Models.Conversation;

namespace PowerHouse.Server.Services
{
    public class ConversationService : IConversationService
    {
        private readonly IConversationRepository _ConversationRepository;
        private readonly IUserRepository _UserRepository;

        private readonly IMapper _mapper;
        private readonly IMessageService _messageService;

        public ConversationService(IConversationRepository conversationRepository, IMapper mapper, IMessageService messageService, IUserRepository userRepository)
        {
            _ConversationRepository = conversationRepository;
            _UserRepository = userRepository;

            _mapper = mapper;
            _messageService = messageService;
        }

        private static bool ValidateConversation(ConversationDTO conversationDTO)
        {
            if (conversationDTO == null)
            {
                return false;
            }
            return true;
        }

        public async Task<List<ConversationDTO>> GetConversations()
        {
            List<ConversationDTO> conversations = _mapper.Map<List<Conversation>, List<ConversationDTO>>(await _ConversationRepository.GetConversationsAsync());
            foreach (var item in conversations)
            {
                if (item.IsBlocked) item.Topic = "This topic has been reported";
            }
            return conversations;
        }

        public async Task<ConversationDTO> GetConversationById(Guid id)
        {
            ConversationDTO conversation = _mapper.Map<Conversation, ConversationDTO>(await _ConversationRepository.GetConversationByIdAsync(id));
            if (conversation.IsBlocked) conversation.Topic = "This topic has been reported";
            foreach (var message in conversation.Messages)
            {
                var messageDecrypted = await _messageService.DecryptTextMessage(message.Text);
                message.Text = messageDecrypted;
                if (message.IsBlocked) message.Text = "This message has been reported";
                if (message.IsDeleted) message.Text = "This message has been deleted";

            }
            return conversation;
        }

        public async Task Insert(CreateTopicDTO conversationDTO)
        {
            if (conversationDTO == null)
            {
                throw new Exception();
            }

            Conversation conversation = _mapper.Map<CreateTopicDTO, Conversation>(conversationDTO);
            User author = new()
            {
                Id = conversationDTO.Author.Id,
                Username = conversationDTO.Author.Username,
                Email = conversationDTO.Author.Email
            };
            conversation.Users.Add(author);
            await _ConversationRepository.InsertAsync(conversation);
            await _ConversationRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(ConversationDTO conversationDTO)
        {
            if (!ValidateConversation(conversationDTO))
            {
                throw new Exception();
            }

            Conversation conversation = _mapper.Map<ConversationDTO, Conversation>(conversationDTO);

            await _ConversationRepository.UpdateAsync(conversation);
            await _ConversationRepository.SaveChangesAsync();
        }
        public async Task Delete(Guid id)
        {
            await _ConversationRepository.DeleteAsync(id);
            await _ConversationRepository.SaveChangesAsync();
        }

        public async Task<List<ConversationDTO>> GetPublicConversations()
        {
            List<ConversationDTO> conversations = _mapper.Map<List<Conversation>, List<ConversationDTO>>(await _ConversationRepository.GetPublicConversations());
            foreach (var item in conversations)
            {
                if (item.IsBlocked) item.Topic = "This topic has been reported";
            }
            return conversations;
        }

        public async Task<ActionResult<List<ConversationDTO>>> GetMostPopularConversations()
        {
            List<ConversationDTO> conversations = _mapper.Map<List<Conversation>, List<ConversationDTO>>(await _ConversationRepository.GetMostPopularConversations());
            foreach (var item in conversations)
            {
                if (item.IsBlocked) item.Topic = "This topic has been reported";
            }
            return conversations;
        }

        public async Task RemoveUserFromConversation(UserConversationDTO input)
        {
            var result = _ConversationRepository.RemoveUserFromConversation(input);
        }

        public async Task AddUserToConversation(UserConversationDTO input)
        {
            //Check if user with entered email exists
            var getUserResult = await _UserRepository.GetUserByMail(input.UserEmail);
            if (getUserResult != null)
            {
                input.UserId = getUserResult.Id;
                await _ConversationRepository.AddUserToConversation(input);
            }
            else { throw new KeyNotFoundException(); }
        }
    }
}