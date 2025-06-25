namespace Teamspace.Models
{
    public enum NotificationType
    {
        Message,
        MaterialUploaded,
        MeetingCreated,
        Announcement,
        Custom
    }
    public class Notification
    {
        public int Id { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsRead { get; set; } = false;
        public NotificationType Type { get; set; }
        public string? RelatedUrl { get; set; } // optional link related to notification
    }

   
}
