namespace Teamspace.Models
{
    public class Post
    {
        public int StaffEmail { get; set; }
        public string SubjectDepartment { get; set; }
        public string SubjectLevel { get; set; }
        public string Content { get; set; }
        public DateTime UploadedAt { get; set; }
        // photo

        // relationships
        public Staff Staff { get; set; }
        public Subject Subject { get; set; }
        public ICollection<PostComment> PostComments { get; set; }
    }
}
