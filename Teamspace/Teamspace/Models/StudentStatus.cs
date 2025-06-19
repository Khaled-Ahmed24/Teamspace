namespace Teamspace.Models
{
    public enum Status
    {
        Failed,
        Pending,
        Succeed
    }
    public class StudentStatus
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public Status Status { get; set; }
        public int Grade { get; set; }
        public Student Student { get; set; }
        public Subject Subject { get; set; }
    }
}
