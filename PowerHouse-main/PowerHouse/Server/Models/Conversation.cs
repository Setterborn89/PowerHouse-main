using System.ComponentModel.DataAnnotations;

namespace PowerHouse.Server.Models
{
    public class Conversation
    {
        public Guid Id { get; set; }
        [Required]
        [MinLength(5)]
        public string Topic { get; set; }
        [Required]
        public bool IsPublic { get; set; }
        public bool IsBlocked { get; set; }
        public Guid? AuthorId { get; set; }
        public List<Message?>? Messages { get; set; }
        public List<User>? Users { get; set; }
    }
}
