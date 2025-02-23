namespace Teamspace.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }

        // image

        // file

        public byte Type { get; set; }

        public string CorrectAns { get; set; }

        public int Grade { get; set; }


        //relationShips

        public int ExamId { get; set; }

        public Exam Exam { get; set; }

        public ICollection<QuestionAns> QuestionAnss { get; set; }
        public ICollection<Choice> Choices { get; set; }
    }
}
