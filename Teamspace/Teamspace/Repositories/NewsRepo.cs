using Microsoft.AspNetCore.Mvc;
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

        public  bool addNews([FromForm] DtoNews dtoNews)
        {
            using var stream = new MemoryStream();
            dtoNews.image.CopyTo(stream);
            if (dtoNews == null) return false;
            News news = new News { Content = dtoNews.Content, StaffEmail = "1", Image = stream.ToArray() };
            _db.News.Add(news);
            _db.SaveChanges();
            return true;
        }

        public  List<News> getAllNews()
        {
           return _db.News.ToList();
        }

        public  News getNewsById(int Id)
        {
            var News = _db.News.FirstOrDefault(n => n.Id == Id);
            return News;
        }
        public bool DeleteNewsById(int Id)
        {
            var News = _db.News.FirstOrDefault(n => n.Id == Id);
            if (News != null)
            {
                _db.News.Remove(News);
                _db.SaveChanges();
                return true;
            }
            return false;
        }
        public  bool UpdateNews([FromForm] DtoNews dtoNews)
        {
            News news = _db.News.First(s => s.Id == dtoNews.Id);
            if (news == null) return false;
            using var stream = new MemoryStream();
            dtoNews.image.CopyTo(stream);
            news.Content = dtoNews.Content;
            news.StaffEmail = dtoNews.StaffEmail;
            news.Image = stream.ToArray();
            _db.SaveChanges();
            return true;
        }
    }
}
