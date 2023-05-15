using System.ComponentModel.DataAnnotations;

namespace PowerHouse.Shared.DTO
{
    public class ConversationDTO
    {
        public Guid Id { get; set; }
        [Required]
        public string Topic { get; set; }
        public virtual UserDTO? Author { get; set; }
		public Guid AuthorId { get; set; }

        [Required]
        public bool IsPublic { get; set; }
		public List<UserDTO>? Users { get; set; }
		public List<MessageDTO>? Messages { get; set; }
		public bool IsBlocked { get; set; }
    }
}
