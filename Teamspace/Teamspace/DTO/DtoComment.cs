namespace Teamspace.DTO
{
    public class DtoComment
    {
        public int PostStaffId { get; set; }
        public int CourseId { get; set; }
        public DateTime UploadedAt { get; set; }
        public string Content { get; set; }
        public int CommenterId { get; set; }

    }
}
