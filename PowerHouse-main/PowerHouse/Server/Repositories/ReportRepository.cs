using Microsoft.EntityFrameworkCore;
using PowerHouse.Server.Data;
using PowerHouse.Server.Interfaces.Repository;
using PowerHouse.Server.Models;

namespace PowerHouse.Server.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly PowerHouseDbContext _context;

        public ReportRepository(PowerHouseDbContext context)
        {
            _context = context;
        }
        public async Task DeleteReportAsync(Guid id)
        {
            Report report = await _context.Reports.FirstOrDefaultAsync(r => r.Id == id);
            if (report == null)
            {
                throw new Exception($"report with id {id} does not exist ");
            }
            _context.Reports.Remove(report);
        }

        public async Task<List<Report>> GetAllReportsAsync(Guid? userId)
        {
            if (userId != null)
            {
                return await _context.Reports
                .Include(m => m.Message)
                .Include(m => m.Conversation)
                .Where(x => x.ReporterId == userId)
                .ToListAsync();
            }
            return await _context.Reports
            .Include(m => m.Message)
            .Include(m => m.Conversation)
            .ToListAsync();
        }

        public async Task<Report> GetReportByIdAsync(Guid id)
        {
            return await _context.Reports
                .Include(m => m.Message)
                .Include(m => m.Conversation)
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task PostReportAsync(Report report)
        {

            if (report.Type == Shared.Enums.Type.Message & !await _context.Messages.AnyAsync(x => x.Id == report.MessageId) ||
                report.Type == Shared.Enums.Type.Conversation & !await _context.Conversation.AnyAsync(x => x.Id == report.ConversationId))
            {
                throw new Exception("This element you are trying to create a report to does not exist!");
            }

            await _context.Reports.AddAsync(report);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateReportAsync(Report report)
        {

            if (!await _context.Reports.AnyAsync(x => x.Id == report.Id))
            {
                throw new Exception("The item you try to update does not exist!");
            }

            _context.Reports.Update(report);
        }
    }
}
