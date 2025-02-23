namespace Teamspace.Models
{
    public class NewsComment
    {
        public int NewsId { get; set; }
        public string Content { get; set; }
        public string CommenterEmail { get; set; }
        public DateTime SentAt { get; set; }

        // relationships
        public News News { get; set; }
    }
}
