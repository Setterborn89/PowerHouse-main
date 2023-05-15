using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;

using PowerHouse.Server.Interfaces.Services;
using PowerHouse.Shared.AES_Encryptions;
using PowerHouse.Shared.DTO;

using System.Text;

namespace PowerHouse.Server.Hubs
{
    public class ConversationHub : Hub
    {
        private readonly IMessageService _messageService;
        public ConversationHub(IMessageService messageService)
        {
            _messageService = messageService;
        }

        //public async Task JoinRoomAsync(Guid conversationId)
        //{
        //    await Groups.AddToGroupAsync(Context.ConnectionId, conversationId.ToString());
        //    await Clients.Group(conversationId.ToString()).SendAsync("JoinRoom", $"A new user has joined!");
        //}

        public async Task JoinConversation(Guid conversationId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, conversationId.ToString());
        }

        public async Task LeaveConversation(Guid conversationId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, conversationId.ToString());
        }

        public async Task SendMessageAsync(Guid conversationId, PostMessageDTO messageDTO)
        {
            try
            {
                MessageDTO postedMessage = await _messageService.PostMessage(messageDTO);

                await Clients.Group(conversationId.ToString()).SendAsync("RecieveMessage", postedMessage);
            }
            catch (Exception ex)
            {
                await SendMessageErrorAsync(ex.Message);
            }
        }

        public async Task UpdateMessageAsync(Guid conversationId, MessageDTO messageDTO)
        {
            try
            {
                MessageDTO editmessage = await _messageService.UpdateMessage(messageDTO);
                await Clients.Group(conversationId.ToString()).SendAsync("RecieveUpdate", editmessage);
            }
            catch (Exception ex)
            {
                await SendMessageErrorAsync(ex.Message);
            }
        }

        public async Task DeleteMessageAsync(Guid conversationId, Guid messageId)
        {
            try
            {
                MessageDTO deletedMessage = await _messageService.DeleteMessage(messageId);
                await Clients.Group(conversationId.ToString()).SendAsync("RecieveDeleted", deletedMessage);
            }
            catch (Exception ex)
            {
                await SendMessageErrorAsync(ex.Message);
            }
        }

        private async Task SendMessageErrorAsync(string error)
        {
            await Clients.Caller.SendAsync("RecieveError", error);
        }
    }
}
