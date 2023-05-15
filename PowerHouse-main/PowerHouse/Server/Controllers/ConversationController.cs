using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PowerHouse.Server.Interfaces.Services;
using PowerHouse.Shared.DTO;

namespace PowerHouse.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationController : ControllerBase
    {
        private readonly IConversationService _service;

        public ConversationController(IConversationService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shared.DTO.ConversationDTO>>> Get()
        {
            return await _service.GetConversations();
        }


        [HttpGet]
        [Route("public")]
        public async Task<ActionResult<IEnumerable<Shared.DTO.ConversationDTO>>> GetPublicConversations()
        {
            return await _service.GetPublicConversations();
        }

        [HttpGet]
        [Route("public/mostpopular")]
        public async Task<ActionResult<List<Shared.DTO.ConversationDTO>>> GetMostPopularConversations()
        {
            return await _service.GetMostPopularConversations();
        }


        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Shared.DTO.ConversationDTO>> GetById(Guid id)
        {
            return await _service.GetConversationById(id);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put(Shared.DTO.ConversationDTO conversationDTO)
        {
            try
            {
                await _service.UpdateAsync(conversationDTO);
                return Ok();
            }
            catch
            {
                return Problem();
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Shared.DTO.ConversationDTO>> Post(Shared.DTO.CreateTopicDTO conversationDTO)
        {
            try
            {
                await _service.Insert(conversationDTO);
                return Ok();
            }
            catch
            {
                return Problem();
            }

        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _service.Delete(id);
                return Ok();
            }
            catch
            {
                return Problem();
            }
        }

        [Authorize]
        [HttpPost]
        [Route("removeuserfromconversation")]
        public async Task<ActionResult> RemoveUserFromConversation(UserConversationDTO input)
        {
            var result = _service.RemoveUserFromConversation(input);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [Route("addusertoconversation")]
        public async Task<ActionResult> AddUserToConversation(UserConversationDTO input)
        {
            try
            {
                await _service.AddUserToConversation(input);
                return Ok();
            }
            catch
            {
                return Problem();
            }
        }
    }
}
