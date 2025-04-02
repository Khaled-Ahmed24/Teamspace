using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Teamspace.Configurations;
using Teamspace.Models;

namespace Teamspace.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StaffsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StaffsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            return await _context.Courses.ToListAsync();
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<Registeration>> Registerater(int id)
        {
            Registeration registeration = new Registeration();

            registeration.CourseId = id;
            // JWT
            registeration.StaffId = 1;
            await _context.AddAsync(registeration);
            await _context.SaveChangesAsync();
            
            return Ok(registeration);
        }
    }
}
