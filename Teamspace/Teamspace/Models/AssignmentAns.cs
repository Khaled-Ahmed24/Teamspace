namespace Teamspace.Models
{
    public class AssignmentAns
    {
        public int StudentEmail { get; set; }
        public int QuestionId { get; set; }

        public Student Student { get; set; }
        public Question Question { get; set; }

        //file
    }
}
