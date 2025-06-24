using Teamspace.Models;

namespace Teamspace.DTO
{
    public class ExamDTO
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

        public int CourseId { get; set; }

    }
}
