using PowerHouse.Shared.DTO;

namespace PowerHouse.Server.Interfaces.Services
{
    public interface IReportService
    {
        Task DeleteReportAsync(Guid id);
        Task<ReportDTO> GetReportByIdAsync(Guid id);
        Task<List<ReportDTO>> GetAllReportsAsync(Guid? userId);
        Task PostReportAsync(ReportDTO reportDTO);
        Task<ReportDTO> UpdateReportAsync(ReportDTO reportDTO);
    }
}
