namespace Teamspace.Models
{
    public enum Semester
    {
        FirstTerm,
        SecondTerm,
        Summer
    }
    public class Course
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Year { get; set; }
        public Semester Semester { get; set; }


        // one - to - many (subject)
        public string SubjectLevel { get; set; }
        public string SubjectDepartment { get; set; }
        public Subject Subject { get; set; }

        // many - to - many (staff)
        public ICollection<Exam> Exams { get; set; }
        public ICollection<Registeration> Registerations { get; set; }
        public ICollection<Material> Materials { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
