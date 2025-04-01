namespace Teamspace.DTO
{
    public class DtoPost
    {

        //pk
        public int StaffId { get; set; }
        public int CourseId { get; set; }
        public DateTime UploadedAt { get; set; }
      //_______________________________________________________
      


        public string Title { get; set; }
        public string Content { get; set; }

        public IFormFile Image { get; set; }

        //foreign key

    }
}
