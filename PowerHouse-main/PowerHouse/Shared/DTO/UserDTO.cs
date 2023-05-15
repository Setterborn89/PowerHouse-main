using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerHouse.Shared.DTO
{
    public class UserDTO
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Username { get; set; }
        public List<ConversationDTO>? Conversations { get; set; }
    }
}
