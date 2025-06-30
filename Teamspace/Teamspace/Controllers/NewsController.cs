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

    public class NewsController : ControllerBase
    {
        public  NewsRepo _newsRepo;
       
        public NewsController(NewsRepo newsRepo)
        {   
            _newsRepo = newsRepo;
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> addNews([FromForm]DtoNews dtoNews)
        {
            var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            var id_txt = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (role != "Admin")
            {
                return Forbid(); // 403 Forbidden
            }
            int id = int.Parse(id_txt);
            bool ok = await _newsRepo.addNews(dtoNews, id);
            if (ok == false) return BadRequest("There is a problem occured while adding news, Please try again");
            return Ok("New added successfully");
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> getAllNews()
        {
            return Ok(await _newsRepo.getAllNews());
        }

        [HttpGet("id")]
        //[Authorize]
        public async Task<IActionResult> getNewsById(int Id)
        {
            var News = await _newsRepo.getNewsById(Id);
            if (News == null) return NotFound();
            return Ok(News);
        }

        [HttpDelete("id")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteNewsById(int Id)
        {

            var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (role != "Admin")
            {
                return Forbid(); // 403 Forbidden
            }
            bool ok = await _newsRepo.DeleteNewsById(Id);
            if (ok == true) return Ok("New deleted successfully");
            return NotFound("This new not found");
        }

        [HttpPut]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateNews([FromForm] DtoNews dtoNews)
        {
            if (dtoNews == null) return BadRequest();
            bool ok = await _newsRepo.UpdateNews(dtoNews);
            if (ok == true) return Ok("New updated successfully");
            return NotFound("This new does not exist");
        }



        // comment

        [HttpPost("[action]")]
        public async Task<IActionResult> addComment([FromForm] DtoComment dtoComment)
        {
            var name = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            if (name == null) return Unauthorized("You must be logged in to comment.");
            bool ok = await _newsRepo.addComment(name, dtoComment);
            if (ok == false) return BadRequest();
            return Ok();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> getAllComments(int newsId)
        {
            return Ok(await _newsRepo.getAllComments(newsId));
        }
    }
}
