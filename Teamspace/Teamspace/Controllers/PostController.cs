
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Teamspace.DTO;
using Teamspace.Repositories;

namespace Teamspace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        public PostRepo _postsRepo;

        public PostController(PostRepo postRepo)
        {
            _postsRepo = postRepo;
        }

        [HttpPost]
        public async Task<IActionResult> addPost([FromForm] DtoPost dtoPost)
        {
            bool ok = _postsRepo.addPost(dtoPost);
            Console.WriteLine(ok + "*************\n");
            if (ok == false) return BadRequest();
            return Ok();
        }
        [HttpGet("getAllPosts/{p}")]

        public async Task<IActionResult> getAllPosts(string p)
        {

            if (string.IsNullOrEmpty(p) || !int.TryParse(p, out int courseId))
                return BadRequest("Invalid or missing course ID");
            var Posts = _postsRepo.getAllPosts(courseId);
            return Ok(Posts);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> getNewsById(int id)
        {
            var temp = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Console.WriteLine(temp + "********\n");
            var Post = _postsRepo.getPostById(id);
            if (Post == null) return NotFound();
            return Ok(Post);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeletePostById(int id)
        {
            bool ok = _postsRepo.DeletePostById(id);
            if (ok == true) return Ok();
            return NotFound();
        }


        [HttpPut]

        public async Task<IActionResult> UpdatePost([FromForm] DtoPost dtoPost)
        {
            bool ok = _postsRepo.UpdatePost(dtoPost);
            if (ok == true) return Ok();
            return NotFound();
        }

        // comment

        [HttpPost("[action]")]
        public async Task<IActionResult> addComment([FromForm] DtoComment dtoComment)
        {
            bool ok = _postsRepo.addComment(dtoComment);
            if (ok == false) return BadRequest();
            return Ok();
        }

        [HttpGet("[action]")]

        public async Task<IActionResult> getAllComments(int postId)
        {
            return Ok(_postsRepo.getAllComments(postId));
        }
    }
}
