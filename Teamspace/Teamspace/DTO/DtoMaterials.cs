using Teamspace.Attributes;

namespace Teamspace.DTO
{
    public class DtoMaterials
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StaffId { get; set; }
        public int CourseId { get; set; }
        public DateTime UploadedAt { get; set; }
        // file
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png", ".pdf", ".pptx", ".txt" })]
        public List<IFormFile> Files { get; set; }
    }
}
