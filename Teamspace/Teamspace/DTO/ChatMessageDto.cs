using System.ComponentModel.DataAnnotations;

namespace Teamspace.DTO
{
    public class ChatMessageDto
    {
        [Required]
        public string FromUserEmail { get; set; }
        [Required]
        public string ToUserEmail { get; set; }
        public IFormFile? File { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
