namespace Teamspace.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string FromUserEmail { get; set; }
        public string ToUserEmail { get; set; }
        public string Message { get; set; }
        public byte[]? File { get; set; }
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }

}
