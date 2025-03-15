using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata.Ecma335;

namespace Teamspace.Models
{
    public class Subject
    {
        public string Department { get; set; }
        public string Level { get; set; }

        public string Name { get; set; }

        public int Hours { get; set; }

        // relationShips
        public int CourseId { get; set; }

        public ICollection<Course> Courses { get; set; }
    }
}
