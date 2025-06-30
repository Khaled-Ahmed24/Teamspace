using System.ComponentModel.DataAnnotations;

namespace Teamspace.DTOs
{
    public class SubjectDTO
    {
        //public int Id { get; set; }
        [Required]
        public string DepartmentName { get; set; }
        [Required]
        public string Name { get; set; }

        public int? DependentId { get; set; }
        [Required]
        public int Hours { get; set; }
    }
}