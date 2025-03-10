namespace Teamspace.Models
{
    public class News
    {
        public int Id { get; set; }
        public string? Content { get; set; }

        public byte[]? Image { get; set; }

        //foreign key
        public string? StaffEmail { get; set; }

        public Staff Staff { get; set; }
    }
}
