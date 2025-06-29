using System.ComponentModel.DataAnnotations;
using Teamspace.Attributes;

namespace Teamspace.DTO
{
    public class Profile
    {
        public string Name { get; set; }
        public bool Gender { get; set; }
        [RegularExpression(@"01[0-2]\d{8}|015\d{8}", ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; }

        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        public IFormFile? Image { get; set; }
    }
}
