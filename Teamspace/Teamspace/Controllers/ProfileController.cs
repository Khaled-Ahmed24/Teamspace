using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Teamspace.Configurations;
using Teamspace.DTO;
using Teamspace.Models;

namespace Teamspace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        AppDbContext _db;
        public ProfileController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetById(int role, int id)
        {
            if(role == 0)
            {
                var student = await _db.Students.SingleOrDefaultAsync(s => s.Id == id);
                if(student == null)
                    return NotFound("This Profile doesn't exist :(");

                return Ok(student);
            }
            else if(role == 1)
            {
                var staff = await _db.Staffs.SingleOrDefaultAsync(s => s.Id == id);
                if (staff == null)
                    return NotFound("This Profile doesn't exist :(");

                return Ok(staff);
            }
            return BadRequest("Invalid Role please ensure you select a valid role :)");
        }
        
    }
}
