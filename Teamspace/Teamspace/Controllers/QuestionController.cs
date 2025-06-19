using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Teamspace.DTO;
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
        public async Task<IActionResult> Add([FromQuery] int examId, [FromForm] List<QuestionDTO> questions)
        {
            List <QuestionDTO> WrongQuestions = new List<QuestionDTO>();
            foreach (var question in questions)
            {
                var result = await _questionRepo.Add(examId, question);
                /*if (!result)
                {
                    WrongQuestions.Add(question); // question not added to send its id :(
                }*/
            }
            if (WrongQuestions.Count == 0)
            {
                //await _questionRepo.Save();
                return Ok("Question Added successfully.");
            }
            return BadRequest(new { msg = "Failed to add question.", WrongQuestions, questions });
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromForm] List<Question> req_questions,[FromQuery]int examId)
        {
            var cur_questions = await _questionRepo.GetAll(examId);

            foreach(var question in cur_questions)
            {
                if (req_questions.Find(q=> q.Id == question.Id) == null)
                {
                    await _questionRepo.Delete(question.Id);
                }
            }

            List <int> question_Ids = new List<int>();
            foreach (var question in req_questions)
            {
                if ( await _questionRepo.GetById(examId, question.Id) == null)
                {
                    //await _questionRepo.Add(question);
                    continue;
                }

                var result = await _questionRepo.Update(question);
                if (!result) question_Ids.Add(question.Id);              
            }
            if (question_Ids.Count == 0)
            {
                await _questionRepo.Save();
                return Ok("Question updated successfully.");
            }
            // بنرجع اراقم الاسئلة الي فيها مشاكل 
            return BadRequest(new { req_questions, question_Ids , error = "Failed to update question." });

        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> Delete([FromQuery] int questionId)
        {
            var result = await _questionRepo.Delete(questionId);
            if (result)
            {
                await _questionRepo.Save();
                return Ok("Question deleted successfully.");
            }       
            return BadRequest("Failed to delete question.");
        }
    }
}
