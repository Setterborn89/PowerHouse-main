using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PowerHouse.Server.Interfaces.Services;
using PowerHouse.Server.Services;
using PowerHouse.Shared.DTO;

namespace PowerHouse.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

		[Authorize(Roles = "Admin")]
		[HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
        {
            return await _userService.GetAlUsersAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(Guid id)
        {
            var user = await _userService.GetUserById(id);
            return user ?? new();
        }

        [HttpGet("email/{mail}")]
        public async Task<ActionResult<UserDTO>> GetUserByMail(string mail)
        {
            var user = await _userService.GetUserByMail(mail);
            return user ?? new();
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostOrUpdateUser([FromBody] RegisterDTO user)
        {
            try
            {
                await _userService.PostOrUpdateUser(user);
                
                return Ok();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                await _userService.DeleteUser(id);
                return Ok();
            }
            catch
            {
                return Problem();
            }
        }
    }
}
