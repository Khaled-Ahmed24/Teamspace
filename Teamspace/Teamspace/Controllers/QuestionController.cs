using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Teamspace.Models;
using Teamspace.Repositories;

namespace Teamspace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        public QuestionRepo _questionRepo;
        public QuestionController(QuestionRepo questionRepo)
        {
            _questionRepo = questionRepo;
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll([FromQuery] int examId)
        {
            var questions = await _questionRepo.GetAll(examId);
            if (questions != null && questions.Count > 0)
                return Ok(questions);
            return NotFound("No questions found.");
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetById([FromQuery] int questionId, [FromQuery] int examId)
        {
            var question = await _questionRepo.GetById(examId, questionId);
            if (question != null)
                return Ok(question);
            return NotFound("Question not found.");
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Add([FromForm] Question question)
        {
            var result = await _questionRepo.Add(question);
            if (result)
                return Ok("Question added successfully.");
            return BadRequest("Failed to add question.");
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromForm] Question question)
        {
            var result = await _questionRepo.Update(question);
            if (result)
                return Ok("Question updated successfully.");
            return BadRequest("Failed to update question.");
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> Delete([FromQuery] int questionId)
        {
            var result = await _questionRepo.Delete(questionId);
            if (result)
                return Ok("Question deleted successfully.");
            return BadRequest("Failed to delete question.");
        }
    }
}
