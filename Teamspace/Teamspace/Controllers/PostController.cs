
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Professor,TA")]
        public async Task<IActionResult> addPost([FromForm] DtoPost dtoPost)
        {
            bool ok = await _postsRepo.addPost(dtoPost);
            //Console.WriteLine(ok + "*************\n");
            if (ok == false) return BadRequest("There is a problem occured when adding post, Please try again");
            return Ok("Post Added Successfully");
        }
        [HttpGet("getAllPosts/{p:int}")]
        [Authorize]
        public async Task<IActionResult> getAllPosts(int courseId)
        {
            var Posts = await _postsRepo.getAllPosts(courseId);
            return Ok(Posts);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> getPostById(int id)
        {
            //var temp = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            //Console.WriteLine(temp + "********\n");
            var Post = await _postsRepo.getPostById(id);
            if (Post == null) return NotFound("This post does not exist.");
            return Ok(Post);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Professor,TA")]
        public async Task<IActionResult> DeletePostById(int id)
        {
            bool ok = await _postsRepo.DeletePostById(id);
            if (ok == true) return Ok("Post deleted successfully");
            return NotFound("This post does not exist");
        }


        [HttpPut]
        public async Task<IActionResult> UpdatePost([FromForm] DtoPost dtoPost)
        {
            bool ok = await _postsRepo.UpdatePost(dtoPost);
            if (ok == true) return Ok("Post updated successfully");
            return NotFound("This post does not exist");
        }

        // comment

        [HttpPost("[action]")]
        public async Task<IActionResult> addComment([FromForm] DtoComment dtoComment)
        {
            var name = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            if (name == null) return Unauthorized("You must be logged in to comment.");
            bool ok = await _postsRepo.addComment(name, dtoComment);
            if (ok == false) return BadRequest();
            return Ok();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> getAllComments(int postId)
        {
            return Ok(await _postsRepo.getAllComments(postId));
        }
    }
}
