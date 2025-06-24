using Microsoft.AspNetCore.SignalR;
using Teamspace.Configurations;
using Teamspace.Hubs;
using Teamspace.Models;

namespace Teamspace.Services
{
    public interface INotificationService
    {
        Task SendNotificationAsync(string userEmail, string message, NotificationType type, string? relatedUrl = null);
    }

    public class NotificationService : INotificationService
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<NotificationHub> _hub;

        public NotificationService(AppDbContext context, IHubContext<NotificationHub> hub)
        {
            _context = context;
            _hub = hub;
        }

        public async Task SendNotificationAsync(string userEmail, string message, NotificationType type, string? relatedUrl = null)
        {
            var notification = new Notification
            {
                UserEmail = userEmail,
                Message = message,
                Type = type,
                RelatedUrl = relatedUrl
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            // إرسال عبر SignalR لو المستخدم متصل
            await _hub.Clients.Group(userEmail)
                .SendAsync("ReceiveNotification", new
                {
                    notification.Id,
                    notification.Message,
                    notification.Type,
                    notification.RelatedUrl,
                    notification.CreatedAt
                });
        }
    }
}
