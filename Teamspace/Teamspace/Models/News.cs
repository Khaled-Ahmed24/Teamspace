namespace Teamspace.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Content { get; set; }

        // image

        //foreign key
        public int StaffEmail { get; set; }

        public Staff Staff { get; set; }
    }
}
