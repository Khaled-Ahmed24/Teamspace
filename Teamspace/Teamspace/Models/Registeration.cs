namespace Teamspace.Models
{
    public class Registeration
    {
        // many - to - many (Staff - Course)
        public int StaffId { get; set; }
        public int CourseId { get; set; }

        public Staff Staff { get; set; }
        public Course Course { get; set; }
    }
}
