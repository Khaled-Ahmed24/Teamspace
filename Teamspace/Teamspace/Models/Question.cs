namespace Teamspace.Models
{
    public enum QuestionType
    {
        MCQ,
        CheckBox,
        Written
    }
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public byte[]? Image { get; set; }

        public byte[]? File { get; set; }

        public QuestionType Type { get; set; }

        public string CorrectAns { get; set; }

        public double Grade { get; set; }


        //relationShips

        public int ExamId { get; set; }

        public Exam Exam { get; set; }

        public ICollection<QuestionAns> QuestionAnss { get; set; }
        public ICollection<Choice> Choices { get; set; }
    }
}
