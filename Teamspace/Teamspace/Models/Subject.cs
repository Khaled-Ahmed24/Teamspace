using Microsoft.Extensions.Hosting;

using System.ComponentModel.DataAnnotations.Schema;

using System.Reflection.Metadata.Ecma335;


namespace Teamspace.Models
{
    public class Subject
    {
        public  int  Id { get; set; }

        public string Name { get; set; }

        public int Hours { get; set; }
        public int DepartmentId { get; set; }

        // relationShips

        public int? DependentId { get; set; }
        public Subject Dependent { get; set; }
        public Department Department { get; set; }

        public ICollection<Course> Courses { get; set; }
        public ICollection<StudentStatus> StudentStatuses { get; set; }

    }
}
