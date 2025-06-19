using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Data;
using System.Security.Cryptography;
using Teamspace.Configurations;
using Teamspace.DTO;
using Teamspace.Models;
using Teamspace.SpaghettiModels;

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



        public async Task<bool> Add(int role, Account account)
        {
            string email = GenerateEmail(account.FirstName, account.LastName, account.NationalId);
            string password = GeneratePassword(8);
            byte[] image;

            using (var memoryStream = new MemoryStream())
            {
                await account.Image.CopyToAsync(memoryStream);
                image = memoryStream.ToArray();
            }

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

                // there is a problem here in image
                if (image.Length > 0)
                    student.Image = image;
                await _db.Students.AddAsync(student);
                await SaveChanges();
                var data = await GetByEmail(email);
                InitializeStudentSubjects(data.Id);
                return true;
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
                    Image = image
                };
                await _db.Staffs.AddAsync(staff);
                await SaveChanges();
                var data = await GetByEmail(email);
                return true;
            }
            return false;
        }


        public async Task<bool> AddByExcel(Excel file)
        {
            if (file == null || file.ExcelFile.Length == 0)
            {
                return false;
            }
            using (var stream = new MemoryStream())
            {
                await file.ExcelFile.CopyToAsync(stream);
                stream.Position = 0;
                using (var excel = new ExcelPackage(stream))
                {
                    var worksheet = excel.Workbook.Worksheets[0];
                    int rows = worksheet.Dimension.Rows;
                    if (file.role == 3)
                    {
                        var students = new List<Student>();
                        for (int i = 2; i <= rows; i++)
                        {
                            string email = GenerateEmail(worksheet.Cells[i, 1].Text, worksheet.Cells[i, 2].Text, worksheet.Cells[i, 6].Text);
                            string password = GeneratePassword(8);

                            students.Add(new Student
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
                        return true;
                    }
                    else if (file.role < 3)
                    {
                        var staffs = new List<Staff>();
                        for (int i = 2; i <= rows; i++)
                        {
                            string email = GenerateEmail(worksheet.Cells[i, 1].Text, worksheet.Cells[i, 2].Text, worksheet.Cells[i, 6].Text);
                            string password = GeneratePassword(8);

                            staffs.Add(new Staff
                            {
                                Name = worksheet.Cells[i, 3].Text,
                                Email = email,
                                Gender = worksheet.Cells[i, 5].Text == "Female",
                                PhoneNumber = worksheet.Cells[i, 6].Text,
                                NationalId = worksheet.Cells[i, 7].Text,
                                Password = password,
                                Role = (Role)Convert.ToInt32(worksheet.Cells[i, 8].Text)
                            });
                        }
                        await _db.AddRangeAsync(staffs);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }


        public async Task<bool> Update(int role, int id, Account account)
        {
            if (role == 3)
            {
                var student = await _db.Students.FirstOrDefaultAsync(s => s.Id == id);
                if (student == null)
                    return false;

                string email = GenerateEmail(account.FirstName, account.LastName, account.NationalId);

                student.Name = account.Name;
                student.Email = email;
                student.PhoneNumber = account.PhoneNumber;
                student.NationalId = account.NationalId;
                student.Year = account.Year;
                student.DepartmentId = account.DepartmentId;
                return true;
            }
            else if (role < 3)
            {
                var staff = await _db.Staffs.SingleOrDefaultAsync(s => s.Id == id);
                if (staff == null)
                    return false;


                string email = GenerateEmail(account.FirstName, account.LastName, account.NationalId);

                staff.Name = account.Name;
                staff.PhoneNumber = account.PhoneNumber;
                staff.NationalId = account.NationalId;
                staff.PhoneNumber = account.PhoneNumber;
                return true;
            }
            else
                return false;
        }


        public async Task<bool> Delete(int role, int id)
        {
            if (role == 3)
            {
                var student = await _db.Students.FirstOrDefaultAsync(s => s.Id == id);
                if (student == null) return false;
                var stutes = await _db.StudentStatuses.Where(st => st.Id == id).ToListAsync();
                _db.StudentStatuses.RemoveRange(stutes);
                _db.Students.Remove(student);
                return true;
            }
            else if (role < 3)
            {
                var staff = await _db.Staffs.SingleOrDefaultAsync(s => s.Id == id);
                if (staff == null)
                    return false;
                _db.Staffs.Remove(staff);
                return true;
            }
            else
                return false;
        }

        public async Task<dynamic?> GetByEmail(string email)
        {
            return await _db.Students.
                Select(s => new { Id = s.Id, Email = s.Email, Password = s.Password, Role = Role.Student })
                .Union(
                      _db.Staffs
                      .Select(s => new { Id = s.Id, Email = s.Email, Password = s.Password, Role = s.Role })
                ).FirstOrDefaultAsync(u => u.Email == email);
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
    }
}
