namespace Teamspace.Models
{
    public class News
    {
        public int Id { get; set; }
        public string? Content { get; set; }

        public byte[]? Image { get; set; }

<<<<<<< HEAD
        //foreign key

        public string? StaffEmail { get; set; }

=======
>>>>>>> 4fd9b6d56408d45a0f65673493397665f77f2e01
        public int StaffId { get; set; }

        public Staff Staff { get; set; }
    }
}
