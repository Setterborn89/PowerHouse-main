using AutoMapper;
using PowerHouse.Server.Interfaces.Repository;
using PowerHouse.Server.Interfaces.Services;
using PowerHouse.Server.Models;
using PowerHouse.Shared.DTO;

namespace PowerHouse.Server.Services
{
    public class ReportService : IReportService
    {
		private readonly IMessageService _messageService;
		private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;
        private readonly IConversationService _conversationService;

        public ReportService(IMessageService messageService, IReportRepository reportRepository, IMapper mapper, IConversationService conversationService)
        {
			_messageService = messageService;
			_reportRepository = reportRepository;
            _mapper = mapper;
            _conversationService = conversationService;
        }

        private static bool ValidateReport(ReportDTO report)
        {
            if (report == null)
            {
                return false;
            }
            return true;
        }

        public async Task DeleteReportAsync(Guid id)
        {
            await _reportRepository.DeleteReportAsync(id);
            await _reportRepository.SaveChangesAsync();
        }

        public async Task<List<ReportDTO>> GetAllReportsAsync(Guid? userId)
        {
            List<ReportDTO> reports = _mapper.Map<List<Report>, List<ReportDTO>>(await _reportRepository.GetAllReportsAsync(userId));
			foreach (var report in reports )
			{
                if (report.Message != null)
                {
                    var decryptedMessage = await _messageService.DecryptTextMessage(report.Message.Text);
                    report.Message.Text = decryptedMessage;
                }
			}
			return reports;
        }

        public async Task<ReportDTO> GetReportByIdAsync(Guid id)
        {
            ReportDTO report = _mapper.Map<Report, ReportDTO>(await _reportRepository.GetReportByIdAsync(id));
            if (report.Message != null)
            {
                var decryptedMessage = await _messageService.DecryptTextMessage(report.Message.Text);
                report.Message.Text = decryptedMessage;
            }
            return report;
        }

        public async Task PostReportAsync(ReportDTO reportDTO)
        {

            if (!ValidateReport(reportDTO))
            {
                throw new Exception();
            }

            Report newReport = _mapper.Map<ReportDTO, Report>(reportDTO);
            await _reportRepository.PostReportAsync(newReport);
            await _reportRepository.SaveChangesAsync();

        }

        public async Task<ReportDTO> UpdateReportAsync(ReportDTO reportDTO)
        {

            if (!ValidateReport(reportDTO))
            {
                throw new Exception();
            }

            reportDTO.IsChecked = true;
            reportDTO.IsClosed = true;

            if(reportDTO.Action == Shared.Enums.Action.Block)
            {
                if(reportDTO.Type == Shared.Enums.Type.Conversation)
                {
                    reportDTO.Conversation.IsBlocked = true;
                }
                else if(reportDTO.Type == Shared.Enums.Type.Message)
                {
                    reportDTO.Message.IsBlocked = true;
                    reportDTO.Message.Text = await _messageService.EncryptTextMessage(reportDTO.Message.Text);
                }
            }

            Report newReport = _mapper.Map<ReportDTO, Report>(reportDTO);
            await _reportRepository.UpdateReportAsync(newReport);
            await _reportRepository.SaveChangesAsync();

            return reportDTO;
        }
    }
}
