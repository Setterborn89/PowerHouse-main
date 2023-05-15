using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerHouse.Shared.DTO
{
	public class CreateTopicDTO
	{
		public Guid Id { get; set; }
		[Required]
        [MinLength(5, ErrorMessage = "The text must be at least 5 characters long.")]
        public string Topic { get; set; }
		public virtual RegisterDTO? Author { get; set; }
		public Guid AuthorId { get; set; }
        [Required]
		public bool IsPublic { get; set; }
		public List<UserDTO>? Users { get; set; }
		public List<MessageDTO>? Messages { get; set; }
		public bool IsBlocked { get; set; }
	}
}