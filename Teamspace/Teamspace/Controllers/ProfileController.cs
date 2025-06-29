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
        public async Task<IActionResult> GetById()
        {

            // role, id should be gotten from the token
            var id_txt = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var roleClaim = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            if(id_txt == null) return Unauthorized("Token is null, Please login and try again.");
            int role = 2; // any role for staff
            int id = int.Parse(id_txt);
            if (roleClaim == "Student") role = 3;
            var user = await _profileRepo.GetById(role, id);
            if (user == null)
                return NotFound("User not found");
            return Ok(user);
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromForm] Profile profile)
        {
            // role, id should be gotten from the token
            var id_txt = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var roleClaim = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            if (id_txt == null) return Unauthorized("Token is null, Please login and try again.");
            int role = 2; // any role for staff
            int id = int.Parse(id_txt);
            if (roleClaim == "Student") role = 3;
            var ok = await _profileRepo.Update(role, id, profile);
            if (ok == "Ok") return Ok("Profile updated successfully :)");  
            return BadRequest(ok);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> ChangePassword(Password pass)
        {
            // role, id should be gotten from the token
            // role, id should be gotten from the token
            var id_txt = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var roleClaim = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            if (id_txt == null) return Unauthorized("Token is null, Please login and try again.");
            int role = 2; // any role for staff
            int id = int.Parse(id_txt);
            if (roleClaim == "Student") role = 3;
            var ok = await _profileRepo.ChangePassword(id, role, pass);
            if (ok == "Ok") return Ok("Password changed successfully :)");
            else return BadRequest(ok);
        }
    }
}
