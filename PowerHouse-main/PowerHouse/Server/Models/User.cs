using System.ComponentModel.DataAnnotations;

namespace PowerHouse.Server.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<Message?>? Messages { get; set; }
        public List<Conversation?>? Conversations { get; set; }
    }
}
