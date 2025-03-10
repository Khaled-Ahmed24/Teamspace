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
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromQuery] int role, [FromQuery] int id, [FromForm] Profile profile)
        {
            if (role == 0)
            {
                var student = await _db.Students.SingleOrDefaultAsync(s => s.Id == id);
                if (student == null)
                    return NotFound("This Profile doesn't exist :(");

                student.Name = profile.Name;
                student.Password = profile.Password;
                student.PhoneNumber = profile.PhoneNumber;
                //student.NationalId = account.NationalId;
                //student.Email = account.Email;
                student.Gender = profile.Gender;
                //student.Year = account.Year;
                //student.DepartmentId = account.DepartmentId;

                await _db.SaveChangesAsync();
                return Ok(new { Message = "Profile Updated Successfully :)", student });
            }
            else if (role == 1)
            {
                var staff = await _db.Staffs.SingleOrDefaultAsync(s => s.Id == id);
                if (staff == null)
                    return NotFound("This Profile doesn't exist :(");

                staff.Name = profile.Name;
                staff.Password = profile.Password;
                staff.PhoneNumber = profile.PhoneNumber;
                staff.Gender = profile.Gender;
                await _db.SaveChangesAsync();
                return Ok(new { Message = "Profile Updated Successfully :)", staff });
            }
            return BadRequest("Invalid Role please ensure you select a valid role :)");
        }
    }
}
