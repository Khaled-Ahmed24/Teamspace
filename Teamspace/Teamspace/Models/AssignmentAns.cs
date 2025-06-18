namespace Teamspace.Models
{
    public class AssignmentAns
    {
        public int StudentId { get; set; }
        public int QuestionId { get; set; }

        public Student Student { get; set; }
        public Question Question { get; set; }

        public byte[]? File { get; set; }
    }
}
