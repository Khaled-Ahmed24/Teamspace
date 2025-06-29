using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Teamspace.Configurations;
using Teamspace.DTO;
using Teamspace.Models;

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


        [HttpGet]
        public async Task<ActionResult<IEnumerable<LevelSchedule>>> GetAll()
        {
            return await _context.LevelSchedules.ToListAsync();
        }

        [HttpGet("{departmentId}/{level}")]
        public async Task<ActionResult<LevelSchedule>> Get(int departmentId, int level)
        {
            var schedule = await _context.LevelSchedules.FindAsync(departmentId, level);
            if (schedule == null)
            {
                return NotFound();
            }
            return schedule;
        }

        [HttpPost]
        public async Task<ActionResult<LevelSchedule>> Add(DtoLevelSchedule dtoLevelSchedule)
        {
            LevelSchedule levelSchedule = new LevelSchedule
            {
                Level = dtoLevelSchedule.Level,
                ScheduleData = dtoLevelSchedule.ScheduleData,
                DepartmentId = dtoLevelSchedule.DepartmentId
            };
            _context.LevelSchedules.Add(levelSchedule);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { departmentId = levelSchedule.DepartmentId, level = levelSchedule.Level }, levelSchedule);
        }

        [HttpPut("{departmentId}/{level}")]
        public async Task<IActionResult> Update(int departmentId, int level, DtoLevelSchedule updated)
        {
            if (departmentId != updated.DepartmentId || level != updated.Level)
            {
                return BadRequest("Composite key mismatch.");
            }
            var schedule = await _context.LevelSchedules.FindAsync(departmentId, level);
              if (schedule == null)
                {
                    return NotFound();
                }

            schedule.ScheduleData = updated.ScheduleData;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScheduleExists(departmentId, level))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }
        [HttpDelete("{departmentId}/{level}")]
        public async Task<IActionResult> Delete(int departmentId, int level)
        {
            var schedule = await _context.LevelSchedules.FindAsync(departmentId, level);
            if (schedule == null)
            {
                return NotFound();
            }

            _context.LevelSchedules.Remove(schedule);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool ScheduleExists(int departmentId, int level)
        {
            return _context.LevelSchedules.Any(e => e.DepartmentId == departmentId && e.Level == level);
        }
    }
}
