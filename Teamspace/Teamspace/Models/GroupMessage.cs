namespace Teamspace.Models
{
    public class GroupMessage
    {
        public int Id { get; set; }
        public int CourseId { get; set; }  // الجروب
        public string SenderEmail { get; set; }  // سواء staff أو student
        public string Message { get; set; }
        public byte[]? File { get; set; }
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }
}
