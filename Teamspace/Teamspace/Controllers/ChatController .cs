using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Teamspace.Configurations;
using Teamspace.Hubs;
using Teamspace.Models;
using Teamspace.DTO;
using Microsoft.EntityFrameworkCore;
using Humanizer;


[ApiController]
[Route("[controller]/[action]")]
public class ChatController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IHubContext<ChatHub> _chatHub;

    public ChatController(AppDbContext context, IHubContext<ChatHub> chatHub)
    {
        _context = context;
        _chatHub = chatHub;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendMessage([FromForm] ChatMessageDto dto)
    {
        
        var message = new ChatMessage
        {
            FromUserEmail = dto.FromUserEmail,
            ToUserEmail = dto.ToUserEmail,
            Message = dto.Message
        };

        using (var stream = new MemoryStream())
        {
            await dto.File.CopyToAsync(stream);
            message.File = stream.ToArray();
        }

        _context.ChatMessages.Add(message);
        await _context.SaveChangesAsync();

        await _chatHub.Clients.User(dto.ToUserEmail)
                      .SendAsync("ReceiveMessage", dto.FromUserEmail, dto.Message);

        return Ok();
    }

    [HttpGet("conversation/{user1Email }/{user2Email }")]
    public async Task<IActionResult> GetConversation(string user1Email, string user2Email)
    {
        var messages = await _context.ChatMessages
            .Where(m =>
                (m.FromUserEmail == user1Email && m.ToUserEmail == user2Email) ||
                (m.FromUserEmail == user2Email && m.ToUserEmail == user1Email))
            .OrderBy(m => m.SentAt)
            .ToListAsync();

        return Ok(messages);
    }


    // بجيب كل اليورز الي كلموني وبععته مترتبين مترتبين من الاحدث للاقدم

    [HttpGet("users/{email}")]
    public async Task<ActionResult<IEnumerable<ChatUserDto>>> GetUserChats(string email)
    {
        var messages = await _context.ChatMessages
            .Where(m => m.FromUserEmail == email || m.ToUserEmail == email)
            .OrderByDescending(m => m.SentAt)
            .ToListAsync();

        var chatUsers = messages
            .Select(m => new
            {
                OtherUser = m.FromUserEmail == email ? m.ToUserEmail : m.FromUserEmail,
                m.Message,
                m.SentAt
            })
            .GroupBy(m => m.OtherUser)
            .Select(g => new ChatUserDto
            {
                Email = g.Key,
                LastMessage = g.First().Message,
                LastMessageTime = g.First().SentAt
            })
            .OrderByDescending(x => x.LastMessageTime)
            .ToList();

        return Ok(chatUsers);
    }

    // ------------------------------------ GroupChat--------------------------------------

    [HttpPost("send-group")]
    public async Task<IActionResult> SendGroupMessage([FromForm] GroupMessageDto dto)
    {

        var course = await _context.Courses
            .Include(c => c.Subject)
            .FirstOrDefaultAsync(c => c.Id == dto.CourseId);

        string groupName = $"{course.Subject.Name}-{course.CreatedAt.Year}-{course.Semester}";

        var message = new GroupMessage
        {
            CourseId = dto.CourseId,
            SenderEmail = dto.SenderEmail,
            Message = dto.Message,
            SentAt = DateTime.UtcNow
        };

        _context.GroupMessages.Add(message);
        await _context.SaveChangesAsync();


        await _chatHub.Clients.Group(groupName)
                      .SendAsync("ReceiveGroupMessage", dto.SenderEmail, dto.Message);

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetGroupConversation(int courseId)
    {
        var messages = await _context.GroupMessages
            .Where(m => m.CourseId == courseId)
            .OrderBy(m => m.SentAt)
            .ToListAsync();
        return Ok(messages);
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ChatUserDto>>> GetUserGroups(string email)
    {
        var messages = new List<GroupMessage>();
        var staff = await _context.Staffs.Where(s=> s.Email == email).FirstOrDefaultAsync();
        var student = await _context.Students.Where(s => s.Email == email).FirstOrDefaultAsync();
        if (staff != null)
        {
            var courses = await _context.Registerations.Where(r=> r.StaffId == staff.Id).ToListAsync();
            foreach(var c in courses)
            {
                var message = await _context.GroupMessages.Where(m=> m.CourseId==c.CourseId).ToListAsync();
                messages.AddRange(message);
            }
        }
        else
        {
            var subjects = await _context.StudentStatuses.Where(s => s.StudentId == student.Id &&
                                                s.Status == Status.Pending).ToListAsync();
            foreach (var item in subjects)
            {
                var subject = await _context.Subjects.Where(s=> s.Id == item.Id).FirstOrDefaultAsync();
                var courses = await _context.Courses.Where(c => c.SubjectId == subject.Id).OrderByDescending(c => c.CreatedAt).FirstOrDefaultAsync();
                var message = await _context.GroupMessages.Where(m => m.CourseId == courses.Id).ToListAsync();
                messages.AddRange(message);
            }
            
        }
        return Ok(messages);
    }
}