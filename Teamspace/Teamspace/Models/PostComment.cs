using Microsoft.Extensions.Hosting;

namespace Teamspace.Models
{
    public class PostComment
    {
        public int PostStaffId { get; set; }
        public int CourseId { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
        public int CommenterId { get; set; }

        // relationships
        public Post Post { get; set; }
    }
}
