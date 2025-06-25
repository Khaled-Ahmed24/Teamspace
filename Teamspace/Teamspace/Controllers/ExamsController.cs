using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Teamspace.Configurations;
using Teamspace.DTO;
using Teamspace.Models;

namespace Teamspace.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        // الي يعدل الامتحان الي عمله بس يعني المعيد مينفعش يعدل 
        // كذلك الي يشوف برضو 
        // لسه المفروض نهدل حوار الطالب و المعيد 
        private readonly AppDbContext _context;

        public ExamsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Exams
        [HttpGet]
        // my all exams
        public async Task<ActionResult<IEnumerable<Exam>>> GetMyExams()
        {

            // هعرض الامتحانات الي في القناه الي اليوزر ده فيها 
            // محتاج اهندلها للطالب وللدكتر وكده لما اخد من خالد ال jwt 

            // JWT انهي دكتور الي فاتح
            int StaffId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            List <Exam> MyExams = await _context.Exams.Where(e=> e.StaffId == StaffId).ToListAsync();
            return await _context.Exams.ToListAsync();
        }

        [HttpGet("{id}")]
        // all exams for specific Course
        public async Task<ActionResult<IEnumerable<ExamDTO>>> GetCourseExams(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return BadRequest("There is no course with this ID");
            }
            List<Exam> MyExams = await _context.Exams.Where(e => e.CourseId == id).ToListAsync();
            List<ExamDTO> TargetExams = new List<ExamDTO>();
            foreach (var exam in MyExams)
            {
                TargetExams.Add(new ExamDTO
                {
                    Id = exam.Id,
                    Description = exam.Description,
                    type = exam.type,
                    IsShuffled = exam.IsShuffled,
                    PassingScore = exam.PassingScore,
                    GradeIsSeen = exam.GradeIsSeen,
                    StartDate = exam.StartDate,
                    Duration = exam.Duration,
                    Grade = exam.Grade,
                    CourseId = exam.CourseId
                });
            }
            return TargetExams;
        }

        [HttpGet("{id}")]

        // my all exams for specific Course
        public async Task<ActionResult<IEnumerable<Exam>>> GetMyCourseExams(int id)// id for Course
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return BadRequest("There is no course with this ID");
            }
            int StaffId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            List<Exam> MyExams = await _context.Exams.Where(e => e.CourseId == id && e.StaffId == StaffId).ToListAsync();
            return await _context.Exams.ToListAsync();

            
        }

        [HttpPost/*("{id}")*/]
        public async Task<ActionResult<Exam>> PostExam([FromForm] ExamDTO _reqExam)
        {
            Exam exam = new Exam();
            exam.Description = _reqExam.Description;
            exam.Duration = _reqExam.Duration;
            exam.type = _reqExam.type;
            exam.StartDate = _reqExam.StartDate;
            exam.Grade = _reqExam.Grade;
            exam.IsShuffled = _reqExam.IsShuffled;
            exam.PassingScore = _reqExam.PassingScore;
            exam.GradeIsSeen = _reqExam.GradeIsSeen;
            // JWT هو حاليا فاتح انهي مادة
            // ممكن اخليه يختار اسم المادة ويبعتها في الموديل الي بستقبله
            // او الفرونت يعملها من غير م الدكتور يختار
            exam.CourseId = _reqExam.CourseId;
            // JWT انهي دكتور الي فاتح

            exam.StaffId = 1;

            _context.Exams.Add(exam);
            await _context.SaveChangesAsync();

            return NoContent();

            // Redirect with 301 Status code to GetAllExams
            //string newUrl = Url.Action("GetAllExams", "Exams");
            //return RedirectPermanent(newUrl);
            return Ok(exam);
        }

        
       
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExam(int id, [FromHeader] ExamDTO _reqExam)
        {
            Exam exam = await _context.Exams.FindAsync(id);
            if (exam == null)
            {
                return BadRequest("There is no exam with this ID");
            }
            // JWT انهي دكتور الي فاتح
            int StaffId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (exam.StaffId != StaffId)
            {
                return Unauthorized("This Exam is not for you");
            }

            exam.Description = _reqExam.Description;
            exam.Duration = _reqExam.Duration;
            exam.type = _reqExam.type;
            exam.StartDate = _reqExam.StartDate;
            exam.Grade = _reqExam.Grade;
            // مش هينفع اغير الكورس بتاعه ولا الدكتور الي عمله
            

            _context.Entry(exam).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            /*
            شايف ملهاش لازمة بس الذكاء اقترحها عليها 
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            */
            // Redirect with 301 Status code to GetAllExams
            string newUrl = Url.Action("GetAllExams", "Exams");
            return RedirectPermanent(newUrl);
        }

        // DELETE: api/Exams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExam(int id)
        {

            var exam = await _context.Exams.FindAsync(id);
            if (exam == null)
            {
                return BadRequest("There is no exam with this ID");
            }
            // JWT انهي دكتور الي فاتح
            int StaffId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (exam.StaffId != StaffId)
            {
                return Unauthorized("This Exam is not for you");
            }

            _context.Exams.Remove(exam);
            await _context.SaveChangesAsync();

            // Redirect with 301 Status code to GetAllExams
            string newUrl = Url.Action("GetAllExams", "Exams");
            return RedirectPermanent(newUrl);
        }


        // يجيب كل الامتحانات الخاصة بطالب معين

        [HttpGet]

        public async Task<ActionResult<IEnumerable<Exam>>> GetStudentExams(int studentId)
        {
            /*
             var questions = await _context.QuestionAnss.Where(q=> q.StudentId == studnetId).ToListAsync();
             var exams = new List<Exam>();
             foreach (var item in questions)
             {
                 var question = await _context.Questions.Where(q=> q.Id == item.QuestionId).FirstOrDefaultAsync();
                 var exam = await _context.Exams.Where(e => e.Id == question.ExamId).FirstOrDefaultAsync();
                 exams.Add(exam);
             }
             */

            // omtimiztion
            var exams = await _context.QuestionAnss
                         .Where(qa => qa.StudentId == studentId)
                         .Include(qa => qa.Question)
                         .ThenInclude(q => q.Exam)
                         .Select(qa => qa.Question.Exam)
                         .Distinct()
                         .ToListAsync();

            return exams;
        }

        // يجيب احابات طالب معين في الامتحان ده والفرونت يقدر يعرف درجته

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionAns>>> GetStudentExam(int studentId,int examId)
        {
            var questionIds = await _context.Questions
                               .Where(q => q.ExamId == examId)
                               .Select(q => q.Id)
                               .ToListAsync();

            var questionAnss = await _context.QuestionAnss
                               .Where(qa => qa.StudentId == studentId && questionIds.Contains(qa.QuestionId))
                               .ToListAsync();

            return questionAnss;
        }

        [HttpGet]

        public async Task<List<StudentExamResult>> GetStudentGradesForExam(int examId)
        {
            var results = await _context.QuestionAnss
                .Include(qa => qa.Question)  
                .Include(qa => qa.Student)
                .Where(qa => qa.Question.ExamId == examId)
                .GroupBy(qa => new { qa.StudentId, qa.Student.Email })
                .Select(group => new StudentExamResult
                {
                    StudentId = group.Key.StudentId,
                    StudentEmail = group.Key.Email,
                    TotalGrade = group.Sum(qa => qa.Grade)
                })
                .ToListAsync();
            return results;
        }

        public class StudentExamResult
        {
            public int StudentId { get; set; }
            public string StudentEmail { get; set; } = string.Empty;
            public double TotalGrade { get; set; }
        }

    }
}
