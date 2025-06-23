using System.ComponentModel.DataAnnotations;
using Teamspace.Models;

namespace Teamspace.DTO
{
    public class Account
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public bool Gender { get; set; }

        [RegularExpression(@"01[0-2]\d{8}|015\d{8}", ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; }

        [Length(14, 14, ErrorMessage = "National ID must be exactly 14 characters long")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "National ID must contain only digits")]
        public string NationalId { get; set; }

        public int Year { get; set; }

        // image
        public IFormFile? Image { get; set; }
        public int? DepartmentId { get; set; }
    }
}
