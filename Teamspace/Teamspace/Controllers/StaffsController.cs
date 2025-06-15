using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.Security.Claims;
using Teamspace.Configurations;
using Teamspace.Migrations;
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
        //[Authorize]
        public async Task<ActionResult<Registeration>> Registerater(int id)
        {

            Registeration registeration = new Registeration();

            registeration.CourseId = id;
            // JWT
            var StaffId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            registeration.StaffId = int.Parse(StaffId);
            await _context.AddAsync(registeration);
            await _context.SaveChangesAsync();
            
            return Ok(registeration);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetAvailableCourses()
        {
            int StudentId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            Student student = await _context.Students.FindAsync(StudentId);

            var AllCourses = await _context.Courses.ToListAsync();
            var MyCourses = new List<Course>();
            foreach (var c in AllCourses)
            {
                if (c.Year > student.Year) continue;
                StudentStatus studentStatus = await _context.StudentStatuses.Where(s => s.SubjectId == c.SubjectId && s.StudentId == StudentId && s.Status == Status.Failed).FirstOrDefaultAsync();
                if (studentStatus != null) MyCourses.Add(c);
            }
            return MyCourses;
        }


    }
}
