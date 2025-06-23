using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Teamspace.Configurations;
using Teamspace.Hubs;
using Teamspace.Models;
using Teamspace.DTO;
using Microsoft.EntityFrameworkCore;


[ApiController]
[Route("api/chat")]
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

        // أرسلها للطرف الآخر في الوقت الحقيقي
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
    public async Task<ActionResult<IEnumerable<ChatUserDto>>> GetChatUsers(string email)
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


}