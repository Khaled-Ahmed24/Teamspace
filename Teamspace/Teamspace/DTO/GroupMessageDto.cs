using System.ComponentModel.DataAnnotations;

namespace Teamspace.DTO
{
    public class GroupMessageDto
    {
        [Required]
        public int CourseId { get; set; }
        [Required]
        public string SenderEmail { get; set; }
        [Required]
        public string Message { get; set; }
        public IFormFile? File { get; set; } // <-- أضف هذا


    }
}
