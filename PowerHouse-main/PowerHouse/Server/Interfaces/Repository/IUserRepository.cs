using PowerHouse.Server.Models;

namespace PowerHouse.Server.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserById(Guid id);
        Task<User> GetUserByMail(string mail);
        Task PostOrUpdateUser(User user);
        Task DeleteUser(Guid id);
        Task SaveChangesAsync();

    }
}
