using Microsoft.EntityFrameworkCore;
using PowerHouse.Server.Data;
using PowerHouse.Server.Interfaces.Repository;
using PowerHouse.Server.Models;

namespace PowerHouse.Server.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly PowerHouseDbContext _context;

        public UserRepository(PowerHouseDbContext context)
        {
            _context = context;

        }

        public async Task DeleteUser(Guid id)
        {
            User user = await _context.Users.FirstOrDefaultAsync(x => x.Id.Equals(id));
            _context.Users.Remove(user);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserById(Guid id)
        {
            return await _context.Users.Include(x => x.Conversations).FirstOrDefaultAsync(m => m.Id.Equals(id));
        }

        public async Task<User> GetUserByMail(string mail)
        {
            return await _context.Users.Include(x => x.Conversations).FirstOrDefaultAsync(m => m.Email.Equals(mail));
        }

        public async Task PostOrUpdateUser(User user)
        {
			if (!await _context.Users.AnyAsync(x => x.Id == user.Id))
			{
				_context.Users.Add(user);
			}
            else
            {
				_context.Users.Update(user);
			}
			
			
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }


    }
}
