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
            return await _db.Students.Select(s => new 
            {
                ID = s.Id,
                Name = s.Name,
                Gender = s.Gender,
                Phone = s.PhoneNumber,
                Image = s.Image,
                Role = 0
            }).Union(_db.Staffs.Select(s => new
            {
                ID = s.Id,
                Name = s.Name,
                Gender = s.Gender,
                Phone = s.PhoneNumber,
                Image = s.Image,
                Role = 1
            })).FirstOrDefaultAsync(u => u.ID == id && u.Role == role);
        }

        public async Task<bool>Update(int role, int id, Profile profile)
        {

            if (role == 0)
            {
                var student = await _db.Students.SingleOrDefaultAsync(s => s.Id == id);
                if (student == null)
                    return false;

                student.Name = profile.Name;
                student.PhoneNumber = profile.PhoneNumber;
                student.Gender = profile.Gender;
                using(var stream = new MemoryStream())
                {
                    await profile.Image.CopyToAsync(stream);
                    student.Image = stream.ToArray();
                }
                //student.NationalId = account.NationalId;
                //student.Email = account.Email;
                //student.Year = account.Year;
                //student.DepartmentId = account.DepartmentId;
                return true;
            }
            else if (role == 1)
            {
                var staff = await _db.Staffs.SingleOrDefaultAsync(s => s.Id == id);
                if (staff == null)
                    return false;

                staff.Name = profile.Name;
                staff.PhoneNumber = profile.PhoneNumber;
                staff.Gender = profile.Gender;
                using(var stream = new MemoryStream())
                {
                    await profile.Image.CopyToAsync(stream);
                    staff.Image = stream.ToArray();
                }
                return true;
            }
            return false;
        }

        public async Task<bool> ChangePassword()
        {
            // type your body here
            return true;
        }
        public async Task SaveChanges()
        {
            await _db.SaveChangesAsync();
        }
    }
}
