using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using PowerHouse.Server.Data;
using PowerHouse.Server.Interfaces.Repository;
using PowerHouse.Shared.DTO;
using Conversation = PowerHouse.Server.Models.Conversation;

namespace PowerHouse.Server.Repositories
{
    public class ConversationRepository : IConversationRepository
    {
        private readonly PowerHouseDbContext _context;

        public ConversationRepository(PowerHouseDbContext context)
        {
            _context = context;
        }

        public async Task<List<Conversation>> GetConversationsAsync()
        {
            return await _context.Conversation.Include(x => x.Users).ToListAsync();
        }

        public async Task<Conversation> GetConversationByIdAsync(Guid id)
        {
            return await _context.Conversation.Include(c => c.Messages.OrderBy(m => m.CreateDate)).ThenInclude(u => u.Author).Include(x => x.Users).FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task InsertAsync(Conversation conversation)
        {
            if (!await _context.Users.AnyAsync(x => x.Id == conversation.AuthorId))
            {
                await _context.Conversation.AddAsync(conversation);
            }
            else
            {
                _context.Conversation.Update(conversation);
            }
        }

        public async Task UpdateAsync(Conversation conversation)
        {

            if (!await _context.Conversation.AnyAsync(x => x.Id == conversation.Id))
            {
                throw new Exception("The item you try to update does not exist!");
            }

            if (await _context.Conversation.AnyAsync(x => x.Topic == conversation.Topic && x.Id != conversation.Id))
            {
                throw new Exception("A conversation with that topic does already exists!");
            }

            _context.Conversation.Update(conversation);
        }
        public async Task DeleteAsync(Guid id)
        {
            Conversation conversation = await _context.Conversation.FirstOrDefaultAsync(x => x.Id.Equals(id));
            _context.Conversation.Remove(conversation);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<Conversation>> GetPublicConversations()
        {
            return await _context.Conversation.Where(x => x.IsPublic).Where(c => c.IsBlocked == false).ToListAsync();
        }

        public async Task<List<Conversation>> GetMostPopularConversations()
        {
            var result = await _context.Conversation.Include(c => c.Users).OrderByDescending(x => x.Users.Count).Where(c => c.IsPublic).Where(c => c.IsBlocked == false).Take(8).ToListAsync();
            return result;
        }

        public async Task RemoveUserFromConversation(UserConversationDTO input)
        {
            var result = _context.Database.ExecuteSqlRaw(
                "DELETE FROM ConversationUser WHERE UsersId={0} AND ConversationsId={1}", 
                input.UserId, 
                input.ConversationId);
        }

        public async Task AddUserToConversation(UserConversationDTO input)
        {
            var result = _context.Database.ExecuteSqlRaw(
            "INSERT INTO ConversationUser (UsersId, ConversationsId) VALUES ({0}, {1})",
            input.UserId,
            input.ConversationId);
            await _context.SaveChangesAsync();
        }
    }
}
