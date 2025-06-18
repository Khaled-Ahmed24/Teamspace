using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Teamspace.Configurations;
using Teamspace.DTO;
using Teamspace.Models;

namespace Teamspace.Repositories
{
    [Authorize]
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
            Post Post = new Post {Title=dtoPost.Title ,Content = dtoPost.Content,Image = stream.ToArray()
              ,CourseId=dtoPost.CourseId,UploadedAt= DateTime.Now, staffId=dtoPost.staffId};
            Console.WriteLine(dtoPost.staffId);
            _db.Posts.Add(Post);
            _db.SaveChanges();
            return true;
        }

        public List<Post> getAllPosts(int courseId)
        {

            return _db.Posts.Where(s=>s.CourseId == courseId).ToList();

        }
        //updatedate here
        public Post getPostById(int id)
        {
           
            var post = _db.Posts.FirstOrDefault(p=>p.Id == id);
            return post;
        }
       // updatedate here
       public bool DeletePostById(int postId)
       {
           var Post = _db.Posts.FirstOrDefault(n => n.Id == postId);
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
            Post post = _db.Posts.First(s => s.Id == dtoPost.Id);
            if (post == null) return false;
            using var stream = new MemoryStream();
            dtoPost.Image.CopyTo(stream);
            post.Content = dtoPost.Content;
            post.Title = dtoPost.Title;
            post.CourseId = 1;
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
                PostId = dtoComment.PostId,
                Content = dtoComment.Content,
                SentAt = DateTime.Now,
                CommenterId = dtoComment.CommenterId,
            };
           // Console.WriteLine(comment.UploadedAt + "&&&&&&&&&&&&\n");
            _db.PostComments.Add(comment);
            _db.SaveChanges();
            return true;
        }
        // updateDate here
        public List<PostComment> getAllComments(int postId)
        {
            return _db.PostComments.
                Where(s => s.PostId == postId).ToList();
        }
    }

}
