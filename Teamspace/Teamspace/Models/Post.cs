namespace Teamspace.Models
{
    public class Post
    {
        // many - to - many (Staff - Course)
        public int StaffId { get; set; }
        public int CourseId { get; set; }
        public string Content { get; set; }
        public DateTime UploadedAt { get; set; }
        // photo

        // relationships
        public Staff Staff { get; set; }
        public Course Course { get; set; }
        public ICollection<PostComment> PostComments { get; set; }
    }
}
