using Microsoft.Extensions.Hosting;

namespace Teamspace.Models
{
    public class PostComment
    {
        public int PostStaffEmail { get; set; }
        public string PostSubjectDepartment { get; set; }
        public string PostSubjectLevel { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
        public string CommenterEmail { get; set; }

        // relationships
        public Post Post { get; set; }
    }
}
