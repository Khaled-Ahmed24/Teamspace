﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Teamspace.Configurations;
using Teamspace.DTO;
using Teamspace.Models;

namespace Teamspace.Repositories
{
    public class NewsRepo
    {
        private  AppDbContext _db;
        public NewsRepo(AppDbContext db)
        {
            _db = db;
        }

        public  async Task<bool> addNews(DtoNews dtoNews, int staffId)
        {
            if (dtoNews == null) return false;
            News news = new News();
            using var stream = new MemoryStream();
            {
                if(dtoNews.Image != null && dtoNews.Image.Length > 0)
                {
                    await dtoNews.Image.CopyToAsync(stream);
                    news.Image = stream.ToArray();
                }
            }
            news.Title = dtoNews.Title;
            news.Content = dtoNews.Content;
            news.StaffId = staffId;
            await _db.News.AddAsync(news);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<News>> getAllNews()
        {
           return await _db.News.ToListAsync();
        }

        public async Task<News?> getNewsById(int Id)
        {
            var News = await _db.News.FirstOrDefaultAsync(n => n.Id == Id);
            return News;
        }
        public async Task<bool> DeleteNewsById(int Id)
        {
            var News = await _db.News.FirstOrDefaultAsync(n => n.Id == Id);
            if (News != null)
            {
                _db.News.Remove(News);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> UpdateNews([FromForm] DtoNews dtoNews)
        {
            var news = await _db.News.FirstOrDefaultAsync(s => s.Id == dtoNews.Id);
            if (news == null) return false;
            using (var stream = new MemoryStream())
            {
                if (dtoNews.Image != null && dtoNews.Image.Length > 0)
                {
                    await dtoNews.Image.CopyToAsync(stream);
                    news.Image = stream.ToArray();
                }
            }
            news.Title = dtoNews.Title;
            news.Content = dtoNews.Content;
            await _db.SaveChangesAsync();
            return true;
        }

        // comment

        public async Task<bool> addComment(string commenterName, DtoComment dtoComment)
        {
            if (dtoComment == null) return false;

            NewsComment comment = new NewsComment
            {
                NewsId = dtoComment.PostId,
                Content = dtoComment.Content,
                SentAt = DateTime.Now,
                CommenterName = commenterName
            };
            // Console.WriteLine(comment.UploadedAt + "&&&&&&&&&&&&\n");
            await _db.NewsComments.AddAsync(comment);
            await _db.SaveChangesAsync();
            return true;
        }
        // updateDate here
        public async Task<List<NewsComment>> getAllComments(int newsId)
        {
            return await _db.NewsComments.
                Where(s => s.NewsId == newsId).ToListAsync();
        }
    }
}
