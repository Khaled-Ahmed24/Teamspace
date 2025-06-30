using Teamspace.Attributes;

namespace Teamspace.DTO
{
    public class AssignmentAnswer
    {
        public int StudentId { get; set; }
        public int QuestionId { get; set; }
        [AllowedExtensions(new string[] { ".pdf", ".docx", ".txt", ".pptx" })]
        public IFormFile? File { get; set; }
    }
}
