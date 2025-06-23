using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Teamspace.Configurations;

namespace Teamspace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ScheduleController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("DoctorSchedule/{staffId}")]
        public async Task<IActionResult> GetSchedule(int staffId)
        {
            var schedule = await _context.DoctorSchedules
                .FirstOrDefaultAsync(s => s.StaffId == staffId);

            if (schedule == null)
                return NotFound();

            return Ok(schedule.ScheduleData);
        }
        [HttpPut("DoctorSchedule/{staffId}")]
        public async Task<IActionResult> UpdateSchedule(int staffId, [FromBody] JsonElement newSchedule)
        {
            var schedule = await _context.DoctorSchedules
                .FirstOrDefaultAsync(s => s.StaffId == staffId);

            if (schedule == null)
                return NotFound();

            schedule.ScheduleData = newSchedule.ToString();
            await _context.SaveChangesAsync();

            return Ok();
        }
      
    }
}
