namespace Teamspace.Models
{
    public class Material
    {
        public int StaffId { get; set; }
        public string SubjectDepartment { get; set; }
        public string SubjectLevel { get; set; }
        public DateTime UploadedAt { get; set; }
        // file

        public Staff Staff { get; set; }
        public Subject Subject { get; set; }
    }
}
