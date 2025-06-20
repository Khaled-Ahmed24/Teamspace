﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Teamspace.Configurations;
using Teamspace.Models;
using Teamspace.DTO;

namespace Teamspace.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AnswerController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionAns>>> GetExamAnss(int examId,int studentId)
        {
            var questionAns = await GetStudentExamAnswers(examId, studentId);

            return questionAns;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionAns>> GetQuestionAns(int questionId, int studentId)
        {
            var questionAns = await _context.QuestionAnss.Where(q => q.QuestionId == questionId
                                                                && q.StudentId == studentId).FirstOrDefaultAsync();

            if (questionAns == null)
            {
                return NotFound();
            }

            return questionAns;
        }

        // init answer for all question for this student in this exam
        [HttpPost]
        public async Task<ActionResult<IEnumerable<QuestionAns>>> InitExamAnss(int examId, int studentId)
        {
            var questionsId = await (from question in _context.Questions 
                                     where(question.ExamId == examId)
                                     select question.Id 
                                     ).ToListAsync();
            foreach (var id in questionsId)
            {
                QuestionAns questionAns = new QuestionAns();
                questionAns.QuestionId = id;
                questionAns.StudentId = studentId;
                questionAns.StudentAns = "";
                questionAns.Grade = 0;
                await _context.AddAsync(questionAns);
                await _context.SaveChangesAsync();
            }
            return NoContent();
            
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestionAns(List <QuestionAns> questiosnAns)
        {
            foreach (var questionAns in questiosnAns)
            {
                var question = await _context.Questions.Where(q => q.Id == questionAns.QuestionId).FirstOrDefaultAsync();
                if (question.Type == QuestionType.Written)
                {
                    // ai 
                }
                else
                {
                    if (questionAns.StudentAns == question.CorrectAns)
                    {
                        questionAns.Grade = question.Grade;
                    }
                }
                _context.Entry(questionAns).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
            return NoContent();

        }

        [HttpPost]

        public async Task<IActionResult> EvaluateExam(int examId, int studentId)
        {
            List<QuestionAns> answer = await GetStudentExamAnswers(examId, studentId);
            int Total_Grade = 0;
            foreach (var item in answer)
            {
                Total_Grade += item.Grade;
            }
            Exam exam = await (_context.Exams.Where(e => e.Id == examId).FirstOrDefaultAsync());
            Course course = await (_context.Courses.Where(c => c.Id == exam.CourseId).FirstOrDefaultAsync());
            Subject subject = await (_context.Subjects.Where(s => s.Id == course.SubjectId).FirstOrDefaultAsync());
            int subjectId = subject.Id;
            StudentStatus studentStatus = await(_context.StudentStatuses.Where(s=> s.StudentId == studentId 
                                                && s.SubjectId == subjectId ).FirstOrDefaultAsync());

            studentStatus.Grade = Total_Grade;
            _context.Entry(studentStatus).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent(); 
        }


        //[HttpDelete("{id}")]
        // for clear answer 
        //public async Task<IActionResult> ClearQuestionAns(int id, int studentId)
        //{
        //    var questionAns = await _context.QuestionAnss.Where(q => q.QuestionId == id
        //                                                        && q.StudentId == studentId).FirstOrDefaultAsync();
        //    if (questionAns == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.QuestionAnss.Remove(questionAns);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}


        private async Task<List<QuestionAns>> GetStudentExamAnswers(int examId, int studentId)
        {
            return await (from question_ans in _context.QuestionAnss
                          where (from question in _context.Questions
                                 where question.ExamId == examId
                                 select question.Id)
                                .Contains(question_ans.QuestionId)
                                && question_ans.StudentId == studentId
                          select question_ans).ToListAsync();
        }


        ///////////////////////Assignment///////////////////////////////////////




        //////////////////Add answer/////////////////////
        [HttpPost("[action]")]
        public async Task<IActionResult> AddAssignmentAnswer([FromForm] List <AssignmentAnswer> answers)
        {
            List <int> questions_ids = new List<int>(); 
            foreach (var answer in answers)
            {
                if (answer.File == null || answer.File.Length == 0)
                {
                    questions_ids.Add(answer.QuestionId);
                    continue;
                }
                var ans = new AssignmentAns();
                using (var stream = new MemoryStream())
                {
                    await answer.File.CopyToAsync(stream);
                    ans.StudentId = answer.StudentId;
                    ans.QuestionId = answer.QuestionId;
                    ans.File = stream.ToArray();
                }
                await _context.AddAsync(ans);
            }
            if (questions_ids.Count == 0)
            {
                await _context.SaveChangesAsync();
                return Ok("Answer is added successfully");
            }
            return BadRequest(new { questions_ids, error = "File is required" });
        }

        ///Get assignment answer/////////
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAssignmentAnswer([FromQuery] int studentId, [FromQuery] int questionId)
        {
            var ans = await _context.AssignmentAnss.FirstOrDefaultAsync(a => a.StudentId == studentId && a.QuestionId == questionId);
            return Ok(ans);

        }

        //// Get all answers/////////

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAssignmentAnswers(int studentId, int examId)
        {
            var answers = from questions in _context.Questions
                          join ans in _context.AssignmentAnss
                          on questions.Id equals ans.QuestionId
                          where questions.ExamId == examId &&
                          ans.StudentId == studentId
                          select new
                          {
                              Id = questions.Id,
                              File = ans.File
                          };
            return Ok(await answers.ToListAsync());
        }

        ///// update assignment answer/////////
        /*
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateAssignmentAnswer(AssignmentAnswer answer)
        {
            if (answer.File == null || answer.File.Length == 0)
                return BadRequest("File is required");
            var ans = _context.AssignmentAnss.FirstOrDefault(a => a.StudentId == answer.StudentId && a.QuestionId == answer.QuestionId);
            using (var stream = new MemoryStream())
            {
                await answer.File.CopyToAsync(stream);
                ans.File = stream.ToArray();
            }
            await _context.SaveChangesAsync();
            return Ok("Answer updated Successfully");
        }
        */
        ///////////// delete /////////////

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteAssignmentAnswer(int studentId, int questionId)
        {
            var answer = _context.AssignmentAnss.FirstOrDefault(a => a.StudentId == studentId && a.QuestionId == questionId);
            if (answer == null)
                return BadRequest("Answer was not found for this question");
            _context.AssignmentAnss.Remove(answer);
            await _context.SaveChangesAsync();
            return Ok("Answer deleted successully");
        }
    }
}
