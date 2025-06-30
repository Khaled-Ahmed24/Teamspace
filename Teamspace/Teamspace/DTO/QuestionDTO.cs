using Teamspace.Attributes;
using Teamspace.Models;

namespace Teamspace.DTO
{

    public class QuestionDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        public IFormFile? Image { get; set; }
        [AllowedExtensions(new string[] { ".pdf", ".docx", ".txt", ".pptx" })]
        public IFormFile? File { get; set; }
        public QuestionType Type { get; set; }
        public string? CorrectAns { get; set; }
        public int Grade { get; set; }
        public List<ChoiceDTO> Choices { get; set; }

    }
}
