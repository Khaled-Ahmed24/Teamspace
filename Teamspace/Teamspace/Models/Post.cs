namespace Teamspace.Models
{
    public class Post
    {
        // many - to - many (Staff - Course)

        public int Id { get; set; }
        public string Title { get; set; }

        public int staffId { get; set; }
        public int CourseId { get; set; }
        public string Content { get; set; }
        public DateTime  UploadedAt { get; set; }
        // photo
        public byte[]? Image { get; set; }
        // relationships
        public Staff Staff { get; set; }
        public Course Course { get; set; }
        public ICollection<PostComment> PostComments { get; set; }
    }
}
