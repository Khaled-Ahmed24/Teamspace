using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using Teamspace.Configurations;
using Teamspace.DTO;
using Teamspace.Models;
using Teamspace.SpaghettiModels;

namespace Teamspace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _db;
        public AccountController(AppDbContext db)
        {
            _db = db;
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllByRole(int role)
        {
            if (role == 0)
            {
                var students = await _db.Students.ToListAsync();
                return Ok(students);
            }
            else if (role == 1)
            {
                var staffs = await _db.Staffs.ToListAsync();
                return Ok(staffs);
            }
            return BadRequest("Invalid role please ensure you select a valid role :)");
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetById(int role, int id)
        {
            if(role == 0)
            {
                var student = await _db.Students.SingleOrDefaultAsync(s => s.Id == id);
                if (student != null)
                    return Ok(student);
                return NotFound("Student not found :(");
            }
            else if(role == 1)
            {
                var staff = await _db.Staffs.SingleOrDefaultAsync(s => s.Id == id);
                if (staff != null)
                    return Ok(staff);
                return NotFound("Staff not found :(");
            }
            return BadRequest("Invalid role please ensure you select a valid role :)");
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddAccount([FromQuery] int role, [FromForm] Account account)
        {
            // Generate Method to handle email here
            string domain = "@fci.aun.edu.eg";
            string email = account.FirstName + "." + account.LastName;
            string nationalId = account.NationalId;
            email = email + nationalId[1] + nationalId[2];
            for (int i = 10; i < 14; i++)
                email +=  nationalId[i];
            email += domain;

            // Generate Method to handle password here
            string password = account.FirstName + nationalId[1] + nationalId[2];
            for (int i = 10; i < 14; i++)
                password += nationalId[i];

            if (role == 0)
            {
                var student = new Student
                {
                    Email = email,
                    Name = account.Name,
                    Gender = account.Gender,
                    PhoneNumber = account.PhoneNumber,
                    NationalId = account.NationalId,
                    Year = account.Year,
                    Password = password,
                    DepartmentId = account.DepartmentId
                };
                await _db.Students.AddAsync(student);
                await _db.SaveChangesAsync();
                return Ok(account);
            }
            else if(role == 1)
            {
                var staff = new Staff
                {
                    Email = email,
                    Name = account.Name,
                    Gender = account.Gender,
                    PhoneNumber = account.PhoneNumber,
                    NationalId = account.NationalId,
                    Password = password
                };
                await _db.Staffs.AddAsync(staff);
                await _db.SaveChangesAsync();
                return Ok(account);
            }
            return BadRequest("Invalid role please ensure you select a valid role :)");

        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddByExcel([FromForm] Excel file)
        {
            if (file == null || file.ExcelFile.Length == 0)
            {
                return BadRequest("File is empty :(");
            }
            using (var stream = new MemoryStream())
            {
                await file.ExcelFile.CopyToAsync(stream);
                stream.Position = 0;
                using (var excel = new ExcelPackage(stream))
                {
                    var worksheet = excel.Workbook.Worksheets[0];
                    int rows = worksheet.Dimension.Rows;
                    if(file.role == 0)
                    {
                        var students = new List<Student>();
                        for (int i = 2; i <= rows; i++)
                        {
                            // Generate Method to handle email here
                            string domain = "@fci.aun.edu.eg";
                            string email = worksheet.Cells[i, 1].Text + "." + worksheet.Cells[i, 2].Text;
                            string nationalId = worksheet.Cells[i, 6].Text;
                            email = email + nationalId[1] + nationalId[2];
                            for (int j = 10; j < 14; j++)
                                email += nationalId[i];
                            email += domain;

                            // Generate Method to handle password here
                            string password = worksheet.Cells[i, 1].Text + nationalId[1] + nationalId[2];
                            for (int j = 10; j < 14; j++)
                                password += nationalId[i];

                            students.Add( new Student
                            {
                                Name = worksheet.Cells[i, 3].Text,
                                Email = email,
                                Gender = worksheet.Cells[i, 4].Text == "Female",
                                PhoneNumber = worksheet.Cells[i, 5].Text,
                                NationalId = worksheet.Cells[i, 6].Text,
                                Year = Convert.ToInt32(worksheet.Cells[i, 7].Text),
                                Password = password,
                                DepartmentId = Convert.ToInt32(worksheet.Cells[i, 8].Text)
                            });
                        }
                        await _db.Students.AddRangeAsync(students);
                        await _db.SaveChangesAsync();
                        return Ok(students);
                    }
                    else if(file.role == 1)
                    {
                        var staffs = new List<Staff>();
                        for (int i = 2; i <= rows; i++)
                        {
                            // Generate Method to handle email here
                            string domain = "@fci.aun.edu.eg";
                            string email = worksheet.Cells[i, 1].Text + "." + worksheet.Cells[i, 2].Text;
                            string nationalId = worksheet.Cells[i, 6].Text;
                            email = email + nationalId[1] + nationalId[2];
                            for (int j = 10; j < 14; j++)
                                email += nationalId[i];
                            email += domain;

                            // Generate Method to handle password here
                            string password = worksheet.Cells[i, 1].Text + nationalId[1] + nationalId[2];
                            for (int j = 10; j < 14; j++)
                                password += nationalId[i];
                            staffs.Add ( new Staff
                            {
                                Name = worksheet.Cells[i, 3].Text,
                                Email = email,
                                Gender = worksheet.Cells[i, 5].Text == "Female",
                                PhoneNumber = worksheet.Cells[i, 6].Text,
                                NationalId = worksheet.Cells[i, 7].Text,
                                Password = password
                            });
                        }
                        await _db.AddRangeAsync(staffs);
                        await _db.SaveChangesAsync();
                        return Ok(staffs);
                    }
                    else
                    {
                        return BadRequest("Invalid role please ensure you select a valid role :)");
                    }
                }
            }
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromQuery] int role, [FromQuery] int id, [FromForm] Account account)
        {

            if(role == 0)
            {
                var student = await _db.Students.SingleOrDefaultAsync(s => s.Id == id);
                if (student == null)
                    return NotFound("Student not found :(");

                // Generate Method to handle email here
                string domain = "@fci.aun.edu.eg";
                string email = account.FirstName + "." + account.LastName;
                string nationalId = account.NationalId;
                email = email + nationalId[1] + nationalId[2];
                for (int i = 10; i < 14; i++)
                    email += nationalId[i];
                email += domain;

                student.Name = account.Name;
                student.Email = email;
                student.PhoneNumber = account.PhoneNumber;
                student.NationalId = account.NationalId;
                student.Year = account.Year;
                student.DepartmentId = account.DepartmentId;
                await _db.SaveChangesAsync();
                return Ok(student); 
            }
            else if(role == 1)
            {
                var staff = await _db.Staffs.SingleOrDefaultAsync(s => s.Id == id);
                if (staff == null)
                    return NotFound("Staff not found :(");


                // Generate Method to handle email here
                string domain = "@fci.aun.edu.eg";
                string email = account.FirstName + "." + account.LastName;
                string nationalId = account.NationalId;
                email = email + nationalId[1] + nationalId[2];
                for (int i = 10; i < 14; i++)
                    email += nationalId[i];
                email += domain;

                staff.Name = account.Name;
                staff.PhoneNumber = account.PhoneNumber;
                staff.NationalId = account.NationalId;
                staff.PhoneNumber = account.PhoneNumber;
                await _db.SaveChangesAsync();
                return Ok(staff);
            }
            return BadRequest("Invalid role please ensure you select a valid role :)");
        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> Delete(int role, int id)
        {
            if(role == 0)
            {
                var student = await _db.Students.SingleOrDefaultAsync(s => s.Id == id);
                if( student == null)
                    return NotFound("Student not found :(");
                _db.Students.Remove(student);
                await _db.SaveChangesAsync();
                return Ok(student);
            }
            else if(role == 1)
            {
                var staff = await _db.Staffs.SingleOrDefaultAsync(s => s.Id == id);
                if (staff == null)
                    return NotFound("Staff not found :(");
                _db.Staffs.Remove(staff);
                await _db.SaveChangesAsync();
                return Ok(staff);
            }
            return BadRequest("Invalid role please ensure you select a valid role :)");
        }
    }
}
