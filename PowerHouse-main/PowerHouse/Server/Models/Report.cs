using Microsoft.Build.Framework;

namespace PowerHouse.Server.Models
{
    public class Report
    {
        public Guid Id { get; init; }
        public DateTime Reported { get; init; }
        public Guid? ReporterId { get; init; }
        public string Reason { get; set; }
        public string? Decision { get; set; }
        [Required]
        public Shared.Enums.Action Action { get; set; }
        [Required]
        public Shared.Enums.Type Type { get; set; }
        public bool IsChecked { get; set; }
        public bool IsClosed { get; set; }
        public Message? Message { get; init; }
        public Guid? MessageId { get; init; }
        public Conversation? Conversation { get; init; }
        public Guid? ConversationId { get; init; }
        
    }
}
