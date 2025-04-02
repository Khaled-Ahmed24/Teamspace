using Teamspace.Models;

namespace Teamspace.DTO
{
    public class ExamDTO
    {
        public string Description { get; set; }
        public ExamType type { get; set; }

        public DateTime StartDate { get; set; }

        // in minutes
        public int Duration { get; set; }

        public int Grade { get; set; }
        
    }
}
