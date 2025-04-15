using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace Teamspace.Models
{
    public enum Role
    {
        Admin,
        Professor,
        TA,
        Student
    }
    public class Staff
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }

        public bool Gender { get; set; }

        public string PhoneNumber { get; set; }

        public string NationalId { get; set; }

        public string Password { get; set; }

        public byte[]? Image { get; set; }
        public Role Role { get; set; }



        // relationShips
        public ICollection<News> News { get; set; }
        public ICollection<Exam> Exams { get; set; }
        public ICollection<Registeration> Registerations { get; set; }
        public ICollection<Material> Materials { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
