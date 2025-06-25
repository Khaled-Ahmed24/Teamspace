namespace Teamspace.DTO
{
    public class ChatUserDto
    {
        public string Email { get; set; } = string.Empty;
        public string LastMessage { get; set; } = string.Empty;

        public DateTime LastMessageTime { get; set; }
    }
}