using Teamspace.Models;

namespace Teamspace.DTO
{

    public class QuestionDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IFormFile? Image { get; set; }
        public IFormFile? File { get; set; }
        public QuestionType Type { get; set; }
        public string? CorrectAns { get; set; }
        public int Grade { get; set; }
        public List<ChoiceDTO>? Choices { get; set; }
    }
}
