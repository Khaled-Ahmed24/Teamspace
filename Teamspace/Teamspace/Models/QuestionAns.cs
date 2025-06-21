namespace Teamspace.Models
{
    public class QuestionAns
    {
        public int StudentId { get; set; }
        public int QuestionId { get; set; }
        public double Grade { get; set; }

        // 6/20/2025 for ai 
        public string reasoning { get; set; } = string.Empty;

        public Student Student { get; set; }
        public Question Question { get; set; }

        public string StudentAns { get; set; }
    }
}
