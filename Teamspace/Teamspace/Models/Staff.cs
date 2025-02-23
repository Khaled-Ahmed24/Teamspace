using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace Teamspace.Models
{
    public class Staff
    {
        [Key]
        public string Email { get; set; }
        public string Name { get; set; }

        public bool Gender { get; set; }

        public string PhoneNumber { get; set; }

        public string NationalId { get; set; }

        public string Password { get; set; }

        // image



        // relationShips
        public ICollection<News> News { get; set; }
        public ICollection<Exam> Exams { get; set; }
        public ICollection<Registeration> Registerations { get; set; }
        public ICollection<Material> Materials { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
