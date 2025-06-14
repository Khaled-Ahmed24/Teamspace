namespace Teamspace.Models
{
    public class CourseDepartment
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int DepartmentId { get; set; }
        public Course Course { get; set; }
        public Department Department { get; set; }
    }
}
