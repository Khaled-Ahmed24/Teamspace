using System.Reflection.Metadata.Ecma335;
using Teamspace.Models;

namespace Teamspace.DTO
{
    public class CourseDTO
    {
        public string SubjectName { get; set; }
        public int Year { get; set; }
        public Semester Semester { get; set; }

        public List<int> Departments { get; set; }
    }
}