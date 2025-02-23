namespace Teamspace.Models
{
    public class Registeration
    {
        public int StaffEmail { get; set; }
        public string SubjectDepartment { get; set; }
        public string SubjectLevel { get; set; }

        public Staff Staff { get; set; }
        public Subject Subject { get; set; }
    }
}
