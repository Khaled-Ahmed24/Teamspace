using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            if (ok == false) return BadRequest();
            return Ok();
        }
        [HttpGet]

        public async Task<IActionResult> getAllPosts()
         {
             return Ok(_postsRepo.getAllPosts());
         }

        [HttpGet("{CourseId}/{StaffId}")]

        public async Task<IActionResult> getNewsById(int CourseId, int StaffId)
        {
            var Post = _postsRepo.getPostById(CourseId,StaffId);
            if (Post == null) return NotFound();
            return Ok(Post);
        }

        [HttpDelete("{CourseId:int}/{StaffId:int}")]

        public async Task<IActionResult> DeletePostById(int CourseId, int StaffId)
        {
            bool ok = _postsRepo.DeletePostById(CourseId, StaffId);
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

        public async Task<IActionResult> getAllComments(int CourseId,int StaffId)
        {
            return Ok(_postsRepo.getAllComments(CourseId, StaffId));
        }
    }
}
