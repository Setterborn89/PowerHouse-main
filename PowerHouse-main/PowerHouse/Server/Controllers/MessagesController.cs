using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PowerHouse.Server.Interfaces.Services;
using PowerHouse.Shared.DTO;

namespace PowerHouse.Server.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageDTO>>> GetAllMessages()
        {
            return await _messageService.GetAllMessages();
        }

        [HttpGet("conversation/{id}")]
        public async Task<ActionResult<IEnumerable<MessageDTO>>> GetMessagesByConversionId(Guid id)
        {
            return await _messageService.GetMessagesByConversationId(id);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MessageDTO>> GetMessageById(Guid id)
        {
            return await _messageService.GetMessageById(id);
        }

        // Author not required, AuthorId is
        [HttpPut]
        public async Task<IActionResult> UpdateMessage(MessageDTO message)
        {
            try
            {
                await _messageService.UpdateMessage(message);
                return Ok();
            }
            catch
            {
                return Problem();
            }
        }

        [HttpPost]
        public async Task<ActionResult<MessageDTO>> PostMessage([FromBody] PostMessageDTO message)
        {
            try
            {
                await _messageService.PostMessage(message);
                return Ok();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(Guid id)
        {
            try
            {
                await _messageService.DeleteMessage(id);
                return Ok();
            }
            catch
            {
                return Problem();
            }
        }
    }
}
