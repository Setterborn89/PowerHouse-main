using PowerHouse.Server.Models;
using PowerHouse.Shared.DTO;

namespace PowerHouse.Server.Interfaces.Services
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAlUsersAsync();
        Task<UserDTO> GetUserById(Guid id);
        Task<UserDTO> GetUserByMail(string mail);
        Task PostOrUpdateUser(RegisterDTO user);
        Task DeleteUser(Guid id);
        
    }
}
