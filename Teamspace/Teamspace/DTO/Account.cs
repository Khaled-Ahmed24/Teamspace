using Teamspace.Models;

namespace Teamspace.DTO
{
    public class Account
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public bool Gender { get; set; }

        public string PhoneNumber { get; set; }

        public string NationalId { get; set; }

        public int Year { get; set; }

        // image
        public IFormFile? Image { get; set; }
        public int DepartmentId { get; set; }
    }
}
