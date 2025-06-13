using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;
using Teamspace.Configurations;
using Teamspace.DTO;
using Teamspace.Models;
using Teamspace.Repositories;

namespace Teamspace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class NewsController : ControllerBase
    {
        public  NewsRepo _newsRepo;
       
        public NewsController(NewsRepo newsRepo)
        {   
            _newsRepo = newsRepo;
        }

        [HttpPost]

        public async Task<IActionResult> addNews([FromForm]DtoNews dtoNews)
        {
            var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (role != "Admin")
            {
                return Forbid(); // 403 Forbidden
            }
            bool ok =_newsRepo.addNews(dtoNews);
            if (ok == false) return BadRequest();
            return Ok();
        }
        [HttpGet]

        public async Task<IActionResult> getAllNews()
        {
            return Ok(_newsRepo.getAllNews());
        }
        [HttpGet("id")]

        public async Task<IActionResult> getNewsById(int Id)
        {
            var News = _newsRepo.getNewsById(Id);
            if (News == null) return NotFound();
            return Ok(News);
        }

        [HttpDelete("id")]

        public async Task<IActionResult> DeleteNewsById(int Id)
        {

            var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (role != "Admin")
            {
                return Forbid(); // 403 Forbidden
            }
            bool ok = _newsRepo.DeleteNewsById(Id);
            if (ok == true) return Ok();
            return NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateNews([FromForm] DtoNews dtoNews)
        {

            var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (role != "Admin")
            {
                return Forbid(); // 403 Forbidden
            }
            if (dtoNews == null) return BadRequest();
            bool ok = _newsRepo.UpdateNews(dtoNews);
            if (ok == true) return Ok();
            return NotFound();
        }
    }
}
