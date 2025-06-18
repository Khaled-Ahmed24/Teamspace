namespace Teamspace.Models
{
    public class Choice
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public DateTime AddedOn { get; set; }
        public string choice { get; set; }
    }
}
