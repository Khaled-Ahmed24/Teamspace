namespace Teamspace.Models
{
    public enum ExamType
    {
        Final,
        Midterm,
        Practical,
        Quiz,
        assignment
    }
    public class Exam
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public ExamType type { get; set; }
        public int IsShuffled { get; set; }
        public int PassingScore { get; set; }
        public int GradeIsSeen { get; set; }
        public DateTime StartDate { get; set; }

        // in minutes
        public int Duration { get; set; }

        public int Grade { get; set; }


        // file 

        // relationShips

        public ICollection<Question> Questions { get; set; }

        public int StaffId { get; set; }
        public int CourseId { get; set; }

        public Staff Staff { get; set; }

        public Course Course { get; set; }
    }
}
