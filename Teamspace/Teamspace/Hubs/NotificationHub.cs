using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Teamspace.Hubs
{
    public class NotificationHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            var userEmail = Context.GetHttpContext()?.Request.Query["userEmail"];

            if (!string.IsNullOrEmpty(userEmail))
            {
                Groups.AddToGroupAsync(Context.ConnectionId, userEmail);
            }

            return base.OnConnectedAsync();
        }
    }
}
