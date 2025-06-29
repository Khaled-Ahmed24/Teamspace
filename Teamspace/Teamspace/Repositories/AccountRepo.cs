using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Security.Cryptography;
using Teamspace.Configurations;
using Teamspace.DTO;
using Teamspace.Migrations;
using Teamspace.Models;
using Teamspace.SpaghettiModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Teamspace.Repositories
{
    public class AccountRepo
    {
        private readonly AppDbContext _db;
        public AccountRepo(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Student>> GetAllStudents()
        {
            var students = await _db.Students.ToListAsync();
            return students;
        }

        public async Task<List<Staff>> GetAllStaffs(int role)
        {
            var staffs = await _db.Staffs.Where(s => (int)s.Role == role).ToListAsync();
            return staffs;
        }

        public async Task<Student> GetStudentById(int id)
        {
            var student = await _db.Students.FirstOrDefaultAsync(s => s.Id == id);
            return student;

        }



        public async Task<Staff> GetStaffById(int id)
        {
            var staff = await _db.Staffs.FirstOrDefaultAsync(s => s.Id == id);
            return staff;
        }



        public async Task<string> Add(int role, Account account)
        {
            string email = GenerateEmail(account.FirstName, account.LastName, account.NationalId);
            string password = GeneratePassword(8);
            var check = await GetByEmail(email);
            if (check != null)
                return "This account is already exist.";
            Console.WriteLine(password);
            //password = BCrypt.Net.BCrypt.HashPassword(password);

            if (role == 3)
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
                    DepartmentId = account.DepartmentId,
                };

                using (var memoryStream = new MemoryStream())
                {
                    if (account.Image != null && account.Image.Length > 0)
                    {
                        await account.Image.CopyToAsync(memoryStream);
                        student.Image = memoryStream.ToArray();
                    }
                }
                await _db.Students.AddAsync(student);
                await SaveChanges();
                var data = await GetByEmail(email);
                if (data == null)
                    return "There is a problem occured during adding this account please try again.";
                await InitializeStudentSubjects(data.Id);
                return "Ok";
            }
            else if (role < 3)
            {
                var staff = new Staff
                {
                    Email = email,
                    Name = account.Name,
                    Gender = account.Gender,
                    PhoneNumber = account.PhoneNumber,
                    NationalId = account.NationalId,
                    Password = password,
                    Role = (Role)role,
                };
                using (var memoryStream = new MemoryStream())
                {
                    if (account.Image != null && account.Image.Length > 0)
                    {
                        await account.Image.CopyToAsync(memoryStream);
                        staff.Image = memoryStream.ToArray();
                    }
                }
                await _db.Staffs.AddAsync(staff);
                await SaveChanges();

                var schedule = new DoctorSchedule
                {
                    StaffId = staff.Id,
                    ScheduleData = "{}"
                };
                _db.DoctorSchedules.Add(schedule);


                await SaveChanges();
                var data = await GetByEmail(email);
                if (data == null)
                    return "There is a problem occured during adding account please try again.";
                return "Ok";
            }
            return "Invalid role, Please ensure you selected a valid role and try again.";
        }


        public async Task<List<ExcelErrorDTO>> AddByExcel(Excel file)
        {
            var Errors = new List<ExcelErrorDTO>();
            if (file == null || file.ExcelFile.Length == 0)
            {
                var RowErrors = new List<string>();
                RowErrors.Add(
                    "File is empty, Please upload a valid Excel file."
                );
                Errors.Add(new ExcelErrorDTO { Row = 0, Errors = RowErrors });
                return Errors;
            }

            using (var stream = new MemoryStream())
            {
                await file.ExcelFile.CopyToAsync(stream);
                using (var excel = new ExcelPackage(stream))
                {
                    var worksheet = excel.Workbook.Worksheets[0];
                    int rows = worksheet.Dimension.Rows;
                    for (int i = 2; i <= rows; i++)
                    {
                        if (IsRowEmpty(worksheet, i, 9)) continue;
                        var RowErrors = new List<string>();
                        var account = new Account{
                            FirstName = worksheet.Cells[i, 1].Text.Trim(),
                            LastName = worksheet.Cells[i, 2].Text.Trim(),
                            Name = worksheet.Cells[i, 3].Text.Trim(),
                            PhoneNumber = worksheet.Cells[i, 5].Text.Trim(),
                            NationalId = worksheet.Cells[i, 6].Text.Trim(),
                        };
                        // year - gender - department id - Role

                        // year
                        var year = worksheet.Cells[i, 7].Text.Trim();
                        if (int.TryParse(year, out int output))
                            account.Year = output;
                        else RowErrors.Add("Invalid year format. Please enter a valid year.");

                        // gender
                        var female = worksheet.Cells[i, 4].Text.Trim().Equals("Female", StringComparison.OrdinalIgnoreCase);
                        var male = worksheet.Cells[i, 4].Text.Trim().Equals("Male", StringComparison.OrdinalIgnoreCase);
                        if (!female && !male) RowErrors.Add("Invalid gender format. Please enter a valid gender.");
                        else if (female) account.Gender = true;
                        else account.Gender = false;

                        // department id
                        var deptName = worksheet.Cells[i, 8].Text.Trim();
                        var dept = await _db.Departments.Where(d => d.Name == deptName)
                            .FirstOrDefaultAsync();
                        account.DepartmentId = dept.Id;

                        // validation using data annotation
                        var validationContext = new ValidationContext(account, null, null);
                        var validationResults = new List<ValidationResult>();
                        bool isValid = Validator.TryValidateObject(account, validationContext, validationResults, true);
                        if (!isValid)
                        {
                            var errs = validationResults.Select(r => r.ErrorMessage).ToList();
                            foreach (var err in errs) RowErrors.Add(err);
                        }
                        if(RowErrors.Count > 0)
                        {
                            Errors.Add(new ExcelErrorDTO
                            {
                                Row = i,
                                Errors = RowErrors
                            });
                            continue;
                        }
                            
                        // role
                        var input = worksheet.Cells[i, 9].Text.Trim();
                        if (Enum.TryParse<Role>(input, true, out Role role))
                        {
                            var ok = await Add(3, account);
                            if(ok != "Ok") RowErrors.Add(ok);
                        }
                        else RowErrors.Add("Invalid role format. Please enter a valid role (Student, Professor, or TA).");
                    }
                }            
            }
            return Errors;
        }


        public async Task<string> Update(int role, int id, Account account)
        {
            if (role == 3)
            {
                var student = await _db.Students.FirstOrDefaultAsync(s => s.Id == id);
                if (student == null)
                    return "Student not found";

                string email = GenerateEmail(account.FirstName, account.LastName, account.NationalId);

                student.Name = account.Name;
                student.Email = email;
                student.PhoneNumber = account.PhoneNumber;
                student.NationalId = account.NationalId;
                student.Year = account.Year;
                student.DepartmentId = account.DepartmentId;
                await SaveChanges();
                return "Ok";
            }
            else if (role < 3)
            {
                var staff = await _db.Staffs.SingleOrDefaultAsync(s => s.Id == id);
                if (staff == null)
                    return "Staff not found";


                string email = GenerateEmail(account.FirstName, account.LastName, account.NationalId);

                staff.Name = account.Name;
                staff.PhoneNumber = account.PhoneNumber;
                staff.NationalId = account.NationalId;
                staff.PhoneNumber = account.PhoneNumber;
                await SaveChanges();
                return "Ok";
            }
            else
                return "Invalid role, Please ensure you selected a valid role and try again.";
        }


        public async Task<string> Delete(int role, int id)
        {
            if (role == 3)
            {
                var student = await _db.Students.FirstOrDefaultAsync(s => s.Id == id);
                if (student == null) return "Student not found";
                await UnInitializeStudentSubjects(id);
                _db.Students.Remove(student);
                await SaveChanges();
                return "Ok";
            }
            else if (role < 3)
            {
                var staff = await _db.Staffs.SingleOrDefaultAsync(s => s.Id == id);
                if (staff == null) return "Staff not found";
                _db.Staffs.Remove(staff);
                await SaveChanges();
                return "Ok";
            }
            else
                return "Invalid role, Please ensure you selected a valid role and try again.";
        }

        public async Task<dynamic?> GetByEmail(string email)
        {
            var students = _db.Students
                .Where(s => s.Email == email)
                .Select(s => new { Id = s.Id, Name = s.Name, Email = s.Email, Password = s.Password, Role = Role.Student });

            var staff = _db.Staffs
                .Where(s => s.Email == email)
                .Select(s => new { Id = s.Id, Name = s.Name, Email = s.Email, Password = s.Password, Role = s.Role });

            return await students.Union(staff).FirstOrDefaultAsync();
        }


        public async Task SaveChanges()
        {
            await _db.SaveChangesAsync();
        }







        // Helper methods for generating email and password
        public string GenerateEmail(string Fname, string Lname, string NationalID)
        {
            string domain = "@fci.aun.edu.eg";
            string email = Fname + "." + Lname;
            string nationalId = NationalID;
            email = email + nationalId[1] + nationalId[2];
            for (int i = 10; i < 14; i++)
                email += nationalId[i];
            email += domain;
            return email;
        }
        public string GeneratePassword(int length)
        {
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Range(0, length).
                Select(c => characters[RandomNumberGenerator.GetInt32(characters.Length)])
                .ToArray());
        }

        public async Task InitializeStudentSubjects(int studentId)
        {
            var subjects = _db.Subjects.ToList();
            var statuses = new List<StudentStatus>();
            foreach (var sub in subjects)
            {
                statuses.Add(new StudentStatus
                {
                    StudentId = studentId,
                    SubjectId = sub.Id,
                    Status = Status.Failed
                });
            }
            await _db.StudentStatuses.AddRangeAsync(statuses);
            await _db.SaveChangesAsync();
            return;
        }
        public async Task UnInitializeStudentSubjects(int id)
        {
            var statuses = await _db.StudentStatuses.Where(s => s.StudentId == id).ToListAsync();
            if (statuses.Count > 0)
            {
                _db.StudentStatuses.RemoveRange(statuses);
                await SaveChanges();
            }
        }

        public bool IsRowEmpty(ExcelWorksheet worksheet, int row, int maxCols)
        {
            for (int i = 1; i <= maxCols; i++)
                if (!string.IsNullOrEmpty(worksheet.Cells[row, i].Text)) return false;

            return true;
        }
    }
}
