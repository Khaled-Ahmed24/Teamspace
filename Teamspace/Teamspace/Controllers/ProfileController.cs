using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Teamspace.Configurations;
using Teamspace.DTO;
using Teamspace.Models;
using Teamspace.Repositories;

namespace Teamspace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        public ProfileRepo _profileRepo;
        public ProfileController(ProfileRepo profileRepo)
        {
            _profileRepo = profileRepo;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetById(int role, int id)
        {
            // role, id should be gotten from the token
            var user = await _profileRepo.GetById(role, id);
            if (user == null)
                return NotFound("User not found");
            return Ok(user);
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromQuery] int role, [FromQuery] int id, [FromForm] Profile profile)
        {
            bool ok = await _profileRepo.Update(role, id, profile);
            if (ok)
            {
                await _profileRepo.SaveChanges();
                return Ok("Profile updated successfully :)");
            }
                
            return BadRequest("Invalid Role please ensure you select a valid role :)");
        }
    }
}
