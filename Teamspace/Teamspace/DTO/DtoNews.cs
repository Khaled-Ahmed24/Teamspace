using Teamspace.Models;

namespace Teamspace.DTO
{
    public class DtoNews
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public IFormFile Image { get; set; }

        //foreign key

    }
}
