namespace Teamspace.Models
{
    public class Material
    {
        // many - to - many (Staff - Course)
        public int StaffId { get; set; }
        public int CourseId { get; set; }
        public DateTime UploadedAt { get; set; }
        // file

        public Staff Staff { get; set; }
        public Course Course { get; set; }
    }
}
