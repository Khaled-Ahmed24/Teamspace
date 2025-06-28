using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Linq;
using Teamspace.Configurations;
using Teamspace.DTO;
using Teamspace.Models;

namespace Teamspace.Repositories
{
    public class ProfileRepo
    {
        private readonly AppDbContext _db;
        public ProfileRepo(AppDbContext db)
        {
            _db = db;
        }

        public async Task<dynamic>GetById(int role, int id)
        {

            if(role == 3)
            {
                var profile = await _db.Students.Where(s => s.Id == id).Select(s => new
                {
                    ID = s.Id,
                    Name = s.Name,
                    Gender = s.Gender,
                    Phone = s.PhoneNumber,
                    Image = s.Image,
                }).FirstOrDefaultAsync();
                return profile;

            }
            else if(role < 3)
            {
                var profile = await _db.Staffs.Where(s => s.Id == id).Select(s => new
                {
                    ID = s.Id,
                    Name = s.Name,
                    Gender = s.Gender,
                    Phone = s.PhoneNumber,
                    Image = s.Image,
                }).FirstOrDefaultAsync();
                return profile;
            }
            return null;
        }

        public async Task<string>Update(int role, int id, Profile profile)
        {

            if (role == 3)
            {
                var student = await _db.Students.FirstOrDefaultAsync(s => s.Id == id);
                if (student == null) return "This student not exist";

                student.Name = profile.Name;
                student.PhoneNumber = profile.PhoneNumber;
                student.Gender = profile.Gender;
                using(var stream = new MemoryStream())
                {
                    if(profile.Image != null && profile.Image.Length > 0)
                    {
                        await profile.Image.CopyToAsync(stream);
                        student.Image = stream.ToArray();
                    }
                }
                //student.NationalId = account.NationalId;
                //student.Email = account.Email;
                //student.Year = account.Year;
                //student.DepartmentId = account.DepartmentId;
                await SaveChanges();
                return "Ok";
            }
            else if (role < 3)
            {
                var staff = await _db.Staffs.FirstOrDefaultAsync(s => s.Id == id);
                if (staff == null) return "This staff not exist";

                staff.Name = profile.Name;
                staff.PhoneNumber = profile.PhoneNumber;
                staff.Gender = profile.Gender;
                using(var stream = new MemoryStream())
                {
                    if(profile.Image != null && profile.Image.Length > 0)
                    {
                        await profile.Image.CopyToAsync(stream);
                        staff.Image = stream.ToArray();
                    } 
                }
                await SaveChanges();
                return "Ok";
            }
            return "Invalid role please ensure you select a valid role :)";
        }

        public async Task<string> ChangePassword(int id, int role, Password pass)
        {
            // hashing password
            if(role == 3)
            {
                var student = await _db.Students.FirstOrDefaultAsync(s => s.Id == id);
                if (student == null) return "This student not exist";
                student.Password = pass.Pass;
                await SaveChanges();
                return "Ok";
            }
            else if (role < 3)
            {
                var staff = await _db.Staffs.FirstOrDefaultAsync(s => s.Id == id);
                if (staff == null) return "This staff not exist";
                staff.Password = pass.Pass;
                await SaveChanges();
                return "Ok";
            }
            return "Invalid role please ensure you select a valid role :)";
        }
        public async Task SaveChanges()
        {
            await _db.SaveChangesAsync();
        }
    }
}
