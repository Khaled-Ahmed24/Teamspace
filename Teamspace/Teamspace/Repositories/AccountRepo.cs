using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Teamspace.Configurations;
using Teamspace.Models;

namespace Teamspace.Repositories
{
    public class AccountRepo
    {
        private readonly AppDbContext _db;
        public AccountRepo(AppDbContext db)
        {
            _db = db;
        }

        public List<Student> GetAllStudents()
        {
            var students = _db.Students.ToList();
            return students;
        }
        
        public List<Staff> GetAllStaffs()
        {
            var staffs = _db.Staffs.ToList();
            return staffs;
        }
    }
}
