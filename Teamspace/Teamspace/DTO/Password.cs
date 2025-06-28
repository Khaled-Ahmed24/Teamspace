using System.ComponentModel.DataAnnotations;

namespace Teamspace.DTO
{
    public class Password
    {
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$", ErrorMessage = "Password must contain a combination of upper case characters, lower case characters and digits.")]
        public string Pass { get; set; }
    }
}
