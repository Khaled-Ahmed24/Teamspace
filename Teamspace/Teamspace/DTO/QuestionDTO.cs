using Teamspace.Models;

namespace Teamspace.DTO
{
    public class QuestionDTO
    {
        public string Title { get; set; }
        public QuestionType Type { get; set; }
        public string CorrectAns { get; set; }

        public double Grade { get; set; }

        public int ExamId { get; set; }
    }
}
