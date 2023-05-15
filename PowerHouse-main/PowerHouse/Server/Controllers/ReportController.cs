using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PowerHouse.Server.Interfaces.Services;
using PowerHouse.Shared.DTO;

namespace PowerHouse.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportDTO>>> GetAllReports([FromQuery] Guid? userId = null)
        {
            return await _reportService.GetAllReportsAsync(userId);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReportDTO>> GetReportById(Guid id)
        {
            return await _reportService.GetReportByIdAsync(id);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateReport(ReportDTO reportDTO)
        {
            try
            {
                ReportDTO updatedReport = await _reportService.UpdateReportAsync(reportDTO);
                return Ok(updatedReport);
            }
            catch
            {
                return Problem();
            }
        }

        [HttpPost]
        public async Task<ActionResult<ReportDTO>> PostReport([FromBody] ReportDTO report)
        {
            try
            {
                await _reportService.PostReportAsync(report);
                return Ok();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReport(Guid id)
        {
            try
            {
                await _reportService.DeleteReportAsync(id);
                return Ok();
            }
            catch
            {
                return Problem();
            }
        }
    }
}
