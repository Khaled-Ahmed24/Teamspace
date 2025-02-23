namespace Teamspace.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // relationShips

        // foreign key
        public ICollection<Student> Students { get; set; }
    }
}
