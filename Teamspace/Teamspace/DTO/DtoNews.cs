using System.ComponentModel.DataAnnotations;
using Teamspace.Attributes;
using Teamspace.Models;

namespace Teamspace.DTO
{
    public class DtoNews
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Content { get; set; }

        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png", ".pdf"})]
        public IFormFile? Image { get; set; }

    }
}
