using System.ComponentModel.DataAnnotations;

namespace Teamspace.DTO
{
    public class QuestionAnsDTO
    {
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int QuestionId { get; set; }

        // 6/20/2025 for ai 

        public int Grade { get; set; }

        public string StudentAns { get; set; }
    }
}
