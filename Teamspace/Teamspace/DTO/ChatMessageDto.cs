namespace Teamspace.DTO
{
    public class ChatMessageDto
    {
        public string FromUserEmail { get; set; }
        public string ToUserEmail { get; set; }
        public IFormFile? File { get; set; }
        public string Message { get; set; }
    }
}
