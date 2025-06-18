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
    public class DepartmentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DepartmentsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = await _context.Departments.ToListAsync();
            return Ok(departments);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromForm] DepartmentDTO _reqDepartment)
        {
            var dep = await _context.Departments.FirstOrDefaultAsync(d => d.Name == _reqDepartment.Name);
            if (dep != null)
            {
                return BadRequest("This department already exist");
            }
            Department department = new Department();
            department.Name = _reqDepartment.Name;
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            // Redirect with 301 Status code to GetDepartments
            string newUrl = Url.Action("GetDepartments", "Departments");
            return RedirectPermanent(newUrl);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment( int id, [FromForm] DepartmentDTO _reqDepartment)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return BadRequest();
            }
            department.Name = _reqDepartment.Name;
            _context.Entry(department).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
            /*
            // Redirect with 301 Status code to GetDepartments
            string newUrl = Url.Action("GetDepartments", "Departments");
            return RedirectPermanent(newUrl);
            */
        }


        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return BadRequest();
            }

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();

           // return NoContent();
            
            // Redirect with 301 Status code to GetDepartments
            string newUrl = Url.Action("GetDepartments", "Departments");
            return RedirectPermanent(newUrl);
            
        }
    }
}
