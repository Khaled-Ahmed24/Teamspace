using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Teamspace.Hubs
{
    public class ChatHub : Hub
    {
        // سيتم استدعاؤها من الفرونت لإرسال الرسالة
        public async Task SendMessage(string fromUserId, string toUserId, string message)
        {
            // أرسل الرسالة للمستخدم الآخر
            await Clients.User(toUserId).SendAsync("ReceiveMessage", fromUserId, message);
        }
    }
}
