using Microsoft.AspNetCore.Mvc;
using Teamspace.Configurations;
using Teamspace.DTO;
using Teamspace.Models;

namespace Teamspace.Repositories
{
    public class PostRepo
    {
        private AppDbContext _db;
        public PostRepo(AppDbContext db)
        {
            _db = db;
        }

        public bool addPost([FromForm] DtoPost dtoPost)
        {
            using var stream = new MemoryStream();
            dtoPost.Image.CopyTo(stream);
            if (dtoPost == null) return false;
            Post Post = new Post {Title=dtoPost.Title ,Content = dtoPost.Content,Image = stream.ToArray(),
                StaffId = 1,CourseId=2,UploadedAt= DateTime.Now};
            _db.Posts.Add(Post);
            _db.SaveChanges();
            return true;
        }

        public List<Post> getAllPosts()
        {
            return _db.Posts.ToList();
        }
        //updatedate here
        public Post getPostById(int CourseId,int StaffId)
        {
            var post = _db.Posts.FirstOrDefault(n => n.CourseId == CourseId &&
                n.StaffId == StaffId );
            return post;
        }
       // updatedate here
       public bool DeletePostById(int CourseId, int StaffId)
       {
           var Post = _db.Posts.FirstOrDefault(n => n.CourseId == CourseId &&
                n.StaffId == StaffId );
            if (Post != null)
           {
               _db.Posts.Remove(Post);
               _db.SaveChanges();
               return true;
           }
           return false;
       }
        //updatedate here
        public bool UpdatePost([FromForm] DtoPost dtoPost)
        {
            Post post = _db.Posts.First(s => s.CourseId == dtoPost.CourseId && s.StaffId== dtoPost.StaffId);
            if (post == null) return false;
            using var stream = new MemoryStream();
            dtoPost.Image.CopyTo(stream);
            post.Content = dtoPost.Content;
            post.Title = dtoPost.Title;
            post.UploadedAt = dtoPost.UploadedAt;
            post.StaffId = 1;
            post.CourseId = 2;
            post.Image = stream.ToArray();   
            _db.SaveChanges();
            return true;
        }

        // comment

        public bool addComment([FromForm] DtoComment dtoComment)
        {
            if (dtoComment == null) return false;
            PostComment comment = new PostComment
            {
                Content = dtoComment.Content,
                PostStaffId = 1,
                CourseId = 2,
                UploadedAt = dtoComment.UploadedAt,
                SentAt = DateTime.Now
            };
            _db.PostComments.Add(comment);
            _db.SaveChanges();
            return true;
        }
        // updateDate here
        public List<PostComment> getAllComments(int CourseId, int StaffId)
        {
            return _db.PostComments.
                Where(s => s.CourseId == CourseId && s.PostStaffId == StaffId).ToList();
        }
    }

}
