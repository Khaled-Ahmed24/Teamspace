using System;
using System.Collections.Generic;
using System.Linq;
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
                return NotFound();
            }

            return Ok(course);
        }




        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse([FromForm] CourseDTO _reqCourse)
        {
            var subject = _context.Subjects.FirstOrDefault(s=> s.Name == _reqCourse.SubjectName);
            if (subject == null)
            {
                return NotFound();
            }

            Course course = new Course();
            course.SubjectId = subject.Id;
            course.Year = _reqCourse.Year;

            // محتاجة تتعدل لما نربط مع الفورنت
            course.Semester = _reqCourse.Semester;
            course.CreatedAt = DateTime.Now;

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

            // Redirect with 301 Status code to GetDepartments
            string newUrl = Url.Action("GetCourses", "Courses");
            return RedirectPermanent(newUrl);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, [FromForm] CourseDTO _reqCourse)
        {
            var course = await _context.Courses.FindAsync(id);
            var subject = _context.Subjects.FirstOrDefault(s => s.Name == _reqCourse.SubjectName);
            if (id != course.Id || subject == null)
            {
                return BadRequest();
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
            // Redirect with 301 Status code to GetDepartments
            string newUrl = Url.Action("GetCourses", "Courses");
            return RedirectPermanent(newUrl);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            string newUrl = Url.Action("GetCourses", "Courses");
            return RedirectPermanent(newUrl);
        }
    }
}
