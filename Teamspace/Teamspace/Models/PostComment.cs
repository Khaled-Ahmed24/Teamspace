using Microsoft.Extensions.Hosting;

namespace Teamspace.Models
{
    public class PostComment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
        public int CommenterId { get; set; }

        // relationships
        public Post Post { get; set; }
    }
}
