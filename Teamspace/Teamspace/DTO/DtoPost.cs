namespace Teamspace.DTO
{
    public class DtoPost
    {
        //pk
        public int Id {  get; set; }
        public int CourseId { get; set; }
        public DateTime UploadedAt { get; set; }
        //_______________________________________________________

        public int staffId { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }

        public IFormFile Image { get; set; }

        //foreign key

    }
}
