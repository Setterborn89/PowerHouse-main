using System.ComponentModel.DataAnnotations;

namespace PowerHouse.Server.Models
{
    public class Message
    {
        public Guid Id { get; init; }
        [MaxLength(300)]
        public string Text { get; set; }
        public DateTime CreateDate { get; init; }
        public DateTime? EditDate { get; init; }
        public bool IsEdited { get; init; }
        public bool IsBlocked { get; set; }
        public User? Author { get; set; }
        public Guid? AuthorId { get; set; }
        public Conversation? Conversation { get; set; }
        public Guid? ConversationId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
