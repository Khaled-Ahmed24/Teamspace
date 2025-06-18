namespace Teamspace.DTO
{
    public class AssignmentAnswer
    {
        public int StudentId { get; set; }
        public int QuestionId { get; set; }
        public IFormFile? File { get; set; }
    }
}
