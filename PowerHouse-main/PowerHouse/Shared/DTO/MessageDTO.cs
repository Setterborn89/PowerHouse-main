using System.ComponentModel.DataAnnotations;

namespace PowerHouse.Shared.DTO
{
    public class MessageDTO
    {
        public Guid Id { get; set; }
        [StringLength(300, ErrorMessage = "Message is too long.")]
        [MinLength(1, ErrorMessage = "Message is too short.")]
        public string Text { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EditDate { get; set; }
        public Guid ConversationId { get; set; }
        public Guid AuthorId { get; set; }
        public UserDTO? Author { get; set; }
        public bool IsEdited { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsDeleted { get; set; }
    }
}
