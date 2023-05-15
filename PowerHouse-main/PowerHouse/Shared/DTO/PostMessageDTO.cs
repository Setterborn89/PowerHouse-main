using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerHouse.Shared.DTO
{
    public class PostMessageDTO
    {
        public Guid Id { get; set; }
        [MaxLength(300)]
        public string Text { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EditDate { get; set; }
		public Guid ConversationId { get; set; }
        public RegisterDTO Author { get; set; }
        public bool IsEdited { get; set; }
        public bool IsBlocked { get; set; }
    }
}
