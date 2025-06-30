using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using Teamspace.Models;

namespace Teamspace.DTO
{
    public class CourseDTO
    {
        [Required]
        public string SubjectName { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public Semester Semester { get; set; }
        [Required]
        public List<int> Departments { get; set; }
    }
}