namespace PowerHouse.Shared.DTO;

public class UserConversationDTO
{
    public Guid? UserId { get; set; }
    public Guid ConversationId { get; set; }
    public string? UserEmail { get; set; }
}
