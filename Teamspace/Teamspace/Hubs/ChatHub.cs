using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Teamspace.Configurations;

namespace Teamspace.Hubs
{
    public class ChatHub : Hub
    {
        private readonly AppDbContext _context;

        public ChatHub(AppDbContext context)
        {
            _context = context;
        }
        // سيتم استدعاؤها من الفرونت لإرسال الرسالة
        public async Task SendMessage(string fromUserId, string toUserId, string message, byte[]? fileData, string? fileName)
        {
            await Clients.User(toUserId).SendAsync("ReceiveMessage", new
            {
                fromUserId,
                message,
                fileName,
                fileDataBase64 = fileData != null ? Convert.ToBase64String(fileData) : null
            });
        }

        // ------------------------------------ GroupChat--------------------------------------

        public async Task JoinCourseGroup(int courseId)
        {
            var course = await _context.Courses
                .Include(c => c.Subject)
                .FirstOrDefaultAsync(c => c.Id == courseId);

            string groupName = $"{course.Subject.Name}-{course.CreatedAt.Year}-{course.Semester}";

            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task SendMessageToCourse(int courseId, string senderEmail, string message)
        {
            var course = await _context.Courses
                .Include(c => c.Subject)
                .FirstOrDefaultAsync(c => c.Id == courseId);


            string groupName = $"{course.Subject.Name}-{course.CreatedAt.Year}-{course.Semester}";

            await Clients.Group(groupName)
                        .SendAsync("ReceiveGroupMessage", senderEmail, message, DateTime.UtcNow);
           
        }
    }
}
