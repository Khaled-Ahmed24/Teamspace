using Microsoft.Extensions.Hosting;

namespace Teamspace.Models
{
    public class Subject
    {
        public string Department { get; set; }
        public string Level { get; set; }

        public string Name { get; set; }

        public int Hours { get; set; }

        // relationShips

        public ICollection<Exam> Exams { get; set; }
        public ICollection<Registeration> Registerations { get; set; }
        public ICollection<Material> Materials { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
