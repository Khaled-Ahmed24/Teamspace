using Teamspace.Models;

namespace Teamspace.DTO
{
    public class DtoNews
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public IFormFile image { get; set; }

        //foreign key
        public string StaffEmail { get; set; }

    }
}
