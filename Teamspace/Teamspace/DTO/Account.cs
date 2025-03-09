namespace Teamspace.DTO
{
    public class Account
    {
        public string? Email { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Gender { get; set; }

        public string PhoneNumber { get; set; }

        public string NationalId { get; set; }

        public int Year { get; set; }

        public string? Password { get; set; }

        // image
        public int? DepartmentId { get; set; }
    }
}
