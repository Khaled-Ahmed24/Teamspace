namespace Teamspace.Models
{
    public class Exam
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public byte type { get; set; }

        public DateTime StartDate { get; set; }

        // in minutes
        public int Duration { get; set; }

        public int Grade { get; set; }


        // file 

        // relationShips

        public ICollection<Question> Questions { get; set; }

        public int StaffId { get; set; }

        public Staff Staff { get; set; }

        public Subject Subject { get; set; }
    }
}
