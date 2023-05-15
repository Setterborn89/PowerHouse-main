using PowerHouse.Server.Models;

namespace PowerHouse.Server.Interfaces.Repository
{
    public interface IReportRepository
    {
        Task DeleteReportAsync(Guid id);
        Task<Report> GetReportByIdAsync(Guid id);
        Task<List<Report>> GetAllReportsAsync(Guid? userId);
        Task PostReportAsync(Report report);
        Task SaveChangesAsync();
        Task UpdateReportAsync(Report report);
    }
}
