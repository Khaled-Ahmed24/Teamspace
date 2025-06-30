using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Teamspace.Configurations;
using Teamspace.DTOs;
using Teamspace.Models;

namespace Teamspace.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
   // [Authorize(Roles = "Admin")]
    
    public class SubjectsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SubjectsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetSubjects()
        {
            var subjects = await _context.Subjects.ToListAsync();
            var data = new List<SubjectDTO>();
            foreach(var sub in subjects)
            {
                var dept = _context.Departments.Where(d => d.Id == sub.DepartmentId).FirstOrDefault();
                data.Add(new SubjectDTO
                {
                    //Id = sub.Id,
                    Name = sub.Name,
                    DepartmentName = dept.Name,
                    DependentId = sub.DependentId,
                    Hours = sub.Hours
                });
            }
            return Ok(data);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateSubject([FromForm] SubjectDTO _reqSubject)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(d => d.Name == _reqSubject.DepartmentName);
            if (department == null)
            {
                return NotFound("There is no department with this name");
            } 

            // NO 2 subject have the same names 
            int existSubjects = await _context.Subjects.CountAsync(s => s.Name == _reqSubject.Name);
            if (existSubjects > 0)
            {
                return BadRequest("there is a subject with this name");
            }

            var subject = new Subject();
            // الفرونت لازم ياخد ليست من الاقسام المتاحة عشان يظهرله اسماء الاقسام الي هيختار منها 
            subject.Hours = _reqSubject.Hours;
            subject.Name = _reqSubject.Name;
            subject.DepartmentId = department.Id;
            subject.DependentId = _reqSubject.DependentId;
            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();
            

            // Redirect with 301 Status code to GetDepartments
            string newUrl = Url.Action("GetSubjects", "Subjects");
            return RedirectPermanent(newUrl);
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutSubject(int id, [FromForm] SubjectDTO _reqSubject)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(d => d.Name == _reqSubject.DepartmentName);
            if (department == null)
            {
                return NotFound("There is no department with this name");
            }
            // NO 2 subject have the same names 
            var subject = await _context.Subjects.FindAsync(id);
            if (subject != null)
            {
                return BadRequest("there is no subject with this Id");
            }
            int existSubjects = await _context.Subjects.CountAsync(s => s.Name == _reqSubject.Name);
            if (_reqSubject.Name == subject.Name)
            {
                // NO changes the name of the current subject
                if (existSubjects > 1) return BadRequest();
            }
            else
            {
                // change the name of the current subject
                if (existSubjects > 0) return BadRequest("there is a subject with this name");
            }

            subject.Name = _reqSubject.Name;
            subject.DepartmentId = department.Id;
            subject.Hours = _reqSubject.Hours;


            _context.Entry(subject).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
            
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound("there is no subject with this Id");
            }

            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();

            return NoContent();
            
        }
    }
}
