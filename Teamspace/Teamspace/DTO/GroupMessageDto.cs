namespace Teamspace.DTO
{
    public class GroupMessageDto
    {
        public int CourseId { get; set; }
        public string SenderEmail { get; set; }
        public string Message { get; set; }
        public IFormFile? File { get; set; } // <-- أضف هذا


    }
}
