using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
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

        public async Task<bool> addPost(DtoPost dtoPost)
        {
            if (dtoPost == null) return false;
            Post Post = new Post();
            using (var stream = new MemoryStream())
            {
                if(dtoPost.Image != null && dtoPost.Image.Length > 0)
                {
                    await dtoPost.Image.CopyToAsync(stream);
                    Post.Image = stream.ToArray();
                }
            }
            Post.Title = dtoPost.Title;
            Post.Content = dtoPost.Content;
            Post.UploadedAt = DateTime.Now;
            Post.staffId = dtoPost.staffId;
            Post.CourseId = dtoPost.CourseId;
            //Console.WriteLine(dtoPost.staffId);
            await _db.Posts.AddAsync(Post);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<Post>> getAllPosts(int courseId)
        {

            return await _db.Posts.Where(s=>s.CourseId == courseId).ToListAsync();

        }
        //updatedate here
        public async Task<Post?> getPostById(int id)
        {
            var post = await _db.Posts.FirstOrDefaultAsync(p=>p.Id == id);
            return post;
        }
       // updatedate here
       public async Task<bool> DeletePostById(int postId)
       {
           var Post = await _db.Posts.FirstOrDefaultAsync(n => n.Id == postId);
           if (Post != null)
           {
                // what about delete post comments?
               _db.Posts.Remove(Post);
               await _db.SaveChangesAsync();
               return true;
           }
           return false;
       }
        //updatedate here
        public async Task<bool> UpdatePost(DtoPost dtoPost)
        {
            var post = await _db.Posts.FirstOrDefaultAsync(s => s.Id == dtoPost.Id);
            if (post == null) return false;
            using (var stream = new MemoryStream())
            {
                if (dtoPost.Image != null && dtoPost.Image.Length > 0)
                {
                    await dtoPost.Image.CopyToAsync(stream);
                    post.Image = stream.ToArray();
                }
            }
            post.Title = dtoPost.Title;
            post.Content = dtoPost.Content;
            post.staffId = dtoPost.staffId;
            post.CourseId = dtoPost.CourseId;
            
            await _db.SaveChangesAsync();
            return true;
        }

        // comment

        public async Task<bool> addComment(string commenterName, DtoComment dtoComment)
        {
            if (dtoComment == null) return false;

            PostComment comment = new PostComment
            {
                PostId = dtoComment.PostId,
                Content = dtoComment.Content,
                SentAt = DateTime.Now,
                CommenterName = commenterName
            };
           // Console.WriteLine(comment.UploadedAt + "&&&&&&&&&&&&\n");
            await _db.PostComments.AddAsync(comment);
            await _db.SaveChangesAsync();
            return true;
        }
        // updateDate here
        public async Task<List<PostComment>> getAllComments(int postId)
        {
            return await _db.PostComments.
                Where(s => s.PostId == postId).ToListAsync();
        }
    }

}
