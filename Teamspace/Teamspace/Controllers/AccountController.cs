using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using Teamspace.Configurations;
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

       

        [HttpPost("CreateByExcel")]
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
                            students.Add( new Student
                            {
                                Name = worksheet.Cells[i, 1].Text,
                                Email = worksheet.Cells[i, 2].Text,
                                Gender = worksheet.Cells[i, 3].Text == "Female",
                                PhoneNumber = worksheet.Cells[i, 4].Text,
                                NationalId = worksheet.Cells[i, 5].Text,
                                Year = Convert.ToInt32(worksheet.Cells[i, 6].Text),
                                Password = worksheet.Cells[i, 7].Text,
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
                            staffs.Add ( new Staff
                            {
                                Name = worksheet.Cells[i, 1].Text,
                                Email = worksheet.Cells[i, 2].Text,
                                Gender = worksheet.Cells[i, 3].Text == "Female",
                                PhoneNumber = worksheet.Cells[i, 4].Text,
                                NationalId = worksheet.Cells[i, 5].Text,
                                Password = worksheet.Cells[i, 6].Text
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

       
    }
}
