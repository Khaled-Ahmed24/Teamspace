namespace Teamspace.DTOs
{
    public class SubjectDTO
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }

        public string Name { get; set; }
        public int? DependentId { get; set; }
        public int Hours { get; set; }
    }
}