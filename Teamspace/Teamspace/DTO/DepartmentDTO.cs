using System.ComponentModel.DataAnnotations;
using Teamspace.Controllers;

namespace Teamspace.DTOs
{
    public class DepartmentDTO 
    {
        [Required]
        public string Name { get; set; }
    }
}
