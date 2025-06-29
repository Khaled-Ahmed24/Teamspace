using System.ComponentModel.DataAnnotations;
using Teamspace.Models;

namespace Teamspace.DTO
{
    public class ExamDTO
    {
        public int Id { get; set; }

        public string Description { get; set; }

        [Required]
        public ExamType type { get; set; }

        [Required]
        public int IsShuffled { get; set; }

        [Required]
        public int PassingScore { get; set; }

        [Required]
        public int GradeIsSeen { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        // in minutes
        [Required]
        public int Duration { get; set; }

        [Required]
        public int Grade { get; set; }

        [Required]
        public int CourseId { get; set; }

    }
}
