
using Microsoft.EntityFrameworkCore;
using PowerHouse.Server.Data;
using PowerHouse.Server.Interfaces.Repository;
using PowerHouse.Server.Models;

namespace PowerHouse.Server.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly PowerHouseDbContext _context;

        public MessageRepository(PowerHouseDbContext context)
        {
            _context = context;

        }

        public async Task DeleteMessage(Guid id)
        {
            Message message = await _context.Messages.FirstOrDefaultAsync(x => x.Id.Equals(id));
            _context.Messages.Remove(message);
        }

        public async Task<List<Message>> GetAllMessagesAsync()
        {
            return await _context.Messages.ToListAsync();
        }

        public async Task<List<Message>> GetMessagesByConversationId(Guid id)
        {
            return await _context.Messages.Where(x => x.ConversationId == id).ToListAsync();
        }

        public async Task<Message> GetMessageByIdAsync(Guid id)
        {
            return await _context.Messages.AsNoTracking().FirstOrDefaultAsync(m => m.Id.Equals(id));
        }

        public async Task<Message> PostMessage(Message message)
        {
			if (!await _context.Users.AnyAsync(x => x.Id == message.AuthorId))
			{
				var postedMessage = await _context.Messages.AddAsync(message);
                return postedMessage.Entity;
			}
			else
			{
                var postedMessage = _context.Messages.Update(message);
                return postedMessage.Entity;
            }
        }

        public async Task UpdateMessage(Message message)
        {
            if (!await _context.Messages.AnyAsync(x => x.Id == message.Id))
            {
                throw new Exception("The item you try to update does not exist!");
            }
           _context.Messages.Update(message);

        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
