namespace Teamspace.Models
{
    public class News
    {
        public int Id { get; set; }
        public string? Content { get; set; }

        public byte[]? Image { get; set; }

        //foreign key
<<<<<<< HEAD
        public string? StaffEmail { get; set; }
=======
        public int StaffId { get; set; }
>>>>>>> e57b0b5b650cf323280314714a5c632bae949613

        public Staff Staff { get; set; }
    }
}
