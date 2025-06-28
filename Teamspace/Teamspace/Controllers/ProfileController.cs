using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Teamspace.Configurations;
using Teamspace.DTO;
using Teamspace.Models;
using Teamspace.Repositories;

namespace Teamspace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
            //var id = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //var email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            //var roleClaim = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;

            var user = await _profileRepo.GetById(role, id);
            if (user == null)
                return NotFound("User not found");
            return Ok(user);
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromQuery] int role, [FromQuery] int id, [FromForm] Profile profile)
        {
            var ok = await _profileRepo.Update(role, id, profile);
            if (ok == "Ok") return Ok("Profile updated successfully :)");  
            return BadRequest(ok);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> ChangePassword([FromQuery]int id, [FromQuery] int role, Password pass)
        {
            // role, id should be gotten from the token
            var ok = await _profileRepo.ChangePassword(id, role, pass);
            if (ok == "Ok") return Ok("Password changed successfully :)");
            else return BadRequest(ok);
        }
    }
}
