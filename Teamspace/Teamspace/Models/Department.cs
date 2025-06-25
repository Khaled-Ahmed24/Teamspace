namespace Teamspace.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // relationShips

        // foreign key
        public ICollection<Student> Students { get; set; }
        public ICollection<Subject> Subjects { get; set; }
        public ICollection<LevelSchedule> LevelSchedules { get; set; }
        public ICollection<CourseDepartment> CourseDepartments { get; set; }
    }
}
