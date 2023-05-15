using System.ComponentModel.DataAnnotations;

namespace PowerHouse.Shared.DTO
{
    public class ReportDTO
    {
        public Guid Id { get; set; }
        public DateTime Reported { get; set; }
        [Required]
        public string Reason { get; set; }
        public string? Decision { get; set; }
        public Guid? MessageId { get; init; }
        public Guid? ConversationId { get; init; }
        public MessageDTO? Message { get; set; }
        public ConversationDTO? Conversation { get; set; }
        public Enums.Action Action { get; set; }
        [Required]
        public Enums.Type Type { get; set; }
        public bool IsChecked { get; set; }
        public bool IsClosed { get; set; }
        public UserDTO? Reporter { get; init; }
        public Guid? ReporterId { get; set; }
    }
}
