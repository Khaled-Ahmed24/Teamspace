﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using Teamspace.Configurations;
using Teamspace.Models;

namespace Teamspace.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class RegisterationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RegisterationController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            return await _context.Courses.ToListAsync();
        }


        [HttpGet]
        [Authorize(Roles = "Admin,Professor,TA")]
        public async Task<IActionResult> GetStudentCourses(int id)
        {
            var studnet = await _context.Students.Where(s => s.Id == id).FirstOrDefaultAsync();
            if (studnet == null)
            {
                return NotFound("thres is no sutent whit this Id");
            }
            var subjects_Ids = await _context.StudentStatuses
                .Where(s => s.StudentId == id && s.Status == Status.Pending)
                .Select(s => s.SubjectId)
                .ToListAsync();

            var courses = new List<Course>();
            foreach (var sub_id in subjects_Ids)
            {
                courses.Add(await _context.Courses
                    .Where(c => c.SubjectId == sub_id).OrderBy(k => k.Year).LastAsync());
            }

            return Ok(courses);
        }

        [HttpPost("{id}")]
        [Authorize(Roles = "Admin,Professor,TA")]
        public async Task<ActionResult<Registeration>> Registerater(int courseId,int staffId)
        {
            var course = await _context.Courses.Where(c=> c.Id == courseId).FirstOrDefaultAsync();
            if (course == null)
            {
                return NotFound("there is no course with this Id");
            }
            var staff = await _context.Staffs.Where(c => c.Id == staffId).FirstOrDefaultAsync();
            if (staff == null)
            {
                return NotFound("there is no staff with this Id");
            }

            Registeration registeration = new Registeration();

            registeration.CourseId = courseId;
            registeration.StaffId = staffId;
            await _context.AddAsync(registeration);
            await _context.SaveChangesAsync();
            // JWT
            //registeration.StaffId = ??
            return Ok(registeration);
        }


        [HttpGet("[action]")]
        [Authorize(Roles = "Admin,Student")]
        public async Task<IActionResult> GetAvailableCourses([FromQuery] int id)
        {
            //int StudentId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound("Student not found.");
            }
            var res = from courses in _context.Courses
                      join statuses in _context.StudentStatuses
                      on courses.SubjectId equals statuses.SubjectId
                      join subjects in _context.Subjects
                      on courses.SubjectId equals subjects.Id
                      where statuses.StudentId == id &&
                            statuses.Status == Status.Failed &&
                            courses.Year <= student.Year &&
                _context.CourseDepartments
                .Any(c => c.CourseId == courses.Id && (student.DepartmentId == null ||  c.DepartmentId == student.DepartmentId)) &&
                (subjects.DependentId == null ||
                _context.StudentStatuses
                .Any(s => s.StudentId == student.Id && s.SubjectId == subjects.DependentId && s.Status == Status.Succeed))
                      select new
                      {
                          CourseId = courses.Id
                      };

            return Ok(await res.ToListAsync());
        }

        [HttpPut("[action]")]
        [Authorize(Roles = "Admin,Student")]
        public async Task<IActionResult> Register([FromQuery] int studentId, [FromQuery] int courseId)
        {
            var course = await _context.Courses.Where(c => c.Id == courseId).FirstOrDefaultAsync();
            if (course == null)
            {
                return NotFound("there is no course with this Id");
            }

            var subjectId = await _context.Courses
                .Where(c => c.Id == courseId)
                .Select(c => c.SubjectId)
                .FirstOrDefaultAsync();
            var status = await _context.StudentStatuses
                .FirstOrDefaultAsync(s => s.StudentId == studentId && s.SubjectId == subjectId);
            if (status == null)
            {
                return NotFound("Student status not found.");
            }
            status.Status = Status.Pending;
            await _context.SaveChangesAsync();
            return Ok(new { msg = "Student registered successfully for the course.", status });
        }
    }
}
