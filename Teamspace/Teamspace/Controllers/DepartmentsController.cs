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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateDepartment([FromForm] DepartmentDTO _reqDepartment)
        {
            var dep = await _context.Departments.FirstOrDefaultAsync(d => d.Name == _reqDepartment.Name);
            if (dep != null)
            {
                return BadRequest("This department Name already exist");
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutDepartment( int id, [FromForm] DepartmentDTO _reqDepartment)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound("This department not exist");
            }
            department.Name = _reqDepartment.Name;
            _context.Entry(department).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
           
        }


        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound("This department not exist");
            }

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();

           return NoContent();
            
        }
    }
}
