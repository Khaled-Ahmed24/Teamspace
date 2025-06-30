using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    public class CoursesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CoursesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            return await _context.Courses.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
            {
                return NotFound("There is no course with this Id");
            }

            return Ok(course);
        }




        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Course>> PostCourse([FromForm] CourseDTO _reqCourse)
        {
            var subject = _context.Subjects.FirstOrDefault(s=> s.Name == _reqCourse.SubjectName);
            if (subject == null)
            {
                return BadRequest("There is no subject with this Name");
            }

            Course course = new Course();
            course.SubjectId = subject.Id;
            course.Year = _reqCourse.Year;

            // محتاجة تتعدل لما نربط مع الفورنت
            course.Semester = _reqCourse.Semester;
            course.CreatedAt = DateTime.Now;

            var course2 = await _context.Courses.Where(c=> c.SubjectId == course.SubjectId && c.Semester == course.Semester &&
                                                       c.CreatedAt.Year == course.CreatedAt.Year).FirstOrDefaultAsync();
            if (course2 != null) return BadRequest("this course has been already created");

            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();

            foreach (var item in _reqCourse.Departments)
            {
                CourseDepartment courseDepartment = new CourseDepartment();
                courseDepartment.DepartmentId = item;
                courseDepartment.CourseId = course.Id;
                await _context.CourseDepartments.AddAsync(courseDepartment);
               
            }
            await _context.SaveChangesAsync();

            return Ok(course);
        }



        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutCourse(int id, [FromForm] CourseDTO _reqCourse)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) { return NotFound("There is no course with this Id"); }
            var subject = _context.Subjects.FirstOrDefault(s => s.Name == _reqCourse.SubjectName);
            if (subject == null)
            {
                return BadRequest("There is no subject with this Name");
            }

            course.SubjectId = subject.Id;
            course.Year = _reqCourse.Year;

            // محتاجة تتعدل لما نربط مع الفورنت
            course.Semester = _reqCourse.Semester;
            course.CreatedAt = DateTime.Now;

            _context.Entry(course).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            var cur_courseDepartments = await _context.CourseDepartments.Where(c=> c.CourseId == course.Id).ToListAsync();
            _context.CourseDepartments.RemoveRange(cur_courseDepartments);

            foreach (var item in _reqCourse.Departments)
            {
                CourseDepartment courseDepartment = new CourseDepartment();
                courseDepartment.DepartmentId = item;
                courseDepartment.CourseId = course.Id;
                await _context.CourseDepartments.AddAsync(courseDepartment);
            }
            await _context.SaveChangesAsync();
            return NoContent();
            
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) { return NotFound("There is no course with this Id"); }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpPut]
        [Authorize(Roles = "Admin,Professor")]
        public async Task<IActionResult> FinishCourse(int courseId,double successGrad)
        {
            var course = await _context.Courses.Where(c => c.Id == courseId).FirstOrDefaultAsync();
            if (course == null) { return NotFound("There is no course with this Id"); }
            var studentsIds = await _context.StudentStatuses.Where(s=> s.SubjectId == course.SubjectId && 
                                                                   s.Status == Status.Pending).ToListAsync();
            foreach (var student in studentsIds)
            {
                if (student.Grade >= successGrad)
                {
                    student.Status = Status.Succeed;
                }
                else
                {
                    student.Grade = 0;
                    student.Status = Status.Failed;// شيل شيل شيل شيل يا طويل العمر ي شييييييييييييل 
                }
            }
            return NoContent();
        }
    }
}
