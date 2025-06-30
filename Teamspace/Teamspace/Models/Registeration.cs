using System.ComponentModel.DataAnnotations;

namespace Teamspace.Models
{
    public class Registeration
    {
        // many - to - many (Staff - Course)
        [Required]
        public int StaffId { get; set; }
        [Required]

        public int CourseId { get; set; }

        public Staff Staff { get; set; }
        public Course Course { get; set; }
    }
}
