using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using Teamspace.Configurations;
using Teamspace.DTO;
using Teamspace.Models;
using Teamspace.Repositories;
using Teamspace.SpaghettiModels;

namespace Teamspace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public AccountRepo _accountRepo;
        public AccountController(AccountRepo accountRepo)
        {
            _accountRepo = accountRepo;
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllByRole(int role)
        {
            if (role == 0)
            {
                var students = await _accountRepo.GetAllStudents();
                return Ok(students);
            }
            else if (role == 1)
            {
                var staffs = await _accountRepo.GetAllStaffs();
                return Ok(staffs);
            }
            return BadRequest("Invalid role please ensure you select a valid role :)");
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetById(int role, int id)
        {
            if(role == 0)
            {
                var student = await _accountRepo.GetById(role, id);
                if (student != null)
                    return Ok(student);
                return NotFound("Student not found :(");
            }
            else if(role == 1)
            {
                var staff = await _accountRepo.GetById(role, id);
                if (staff != null)
                    return Ok(staff);
                return NotFound("Staff not found :(");
            }
            return BadRequest("Invalid role please ensure you select a valid role :)");
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> AddAccount([FromQuery] int role, [FromForm] Account account)
        {
            await _accountRepo.Add(role, account);  
            await _accountRepo.SaveChanges();
            return Ok();
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> AddByExcel([FromForm] Excel file)
        {
            await _accountRepo.AddByExcel(file);
            await _accountRepo.SaveChanges();
            return Ok();
        }


        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromQuery] int role, [FromQuery] int id, [FromForm] Account account)
        {
            await _accountRepo.Update(role, id, account);
            await _accountRepo.SaveChanges();
            return Ok();
        }


        [HttpDelete("[action]")]
        public async Task<IActionResult> Delete(int role, int id)
        {
            await _accountRepo.Delete(role, id);
            await _accountRepo.SaveChanges();
            return Ok();
        }
    }
}
