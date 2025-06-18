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

        [HttpGet]
        [HttpGet("{id}")]
        // all exams for specific Course
        public async Task<ActionResult<IEnumerable<Exam>>> GetCourseExams(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return BadRequest("There is no course with this ID");
            }
            List<Exam> MyExams = await _context.Exams.Where(e => e.CourseId == id).ToListAsync();
            return await _context.Exams.ToListAsync();
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

        [HttpPost("{id}")]
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
            int StaffId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            exam.StaffId = StaffId;

            _context.Exams.Add(exam);
            await _context.SaveChangesAsync();

            // Redirect with 301 Status code to GetAllExams
            string newUrl = Url.Action("GetAllExams", "Exams");
            return RedirectPermanent(newUrl);
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


        private bool ExamExists(int id)
        {
            return _context.Exams.Any(e => e.Id == id);
        }
    }
}
