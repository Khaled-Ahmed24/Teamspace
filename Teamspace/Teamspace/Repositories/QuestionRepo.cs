using Microsoft.EntityFrameworkCore;
using Teamspace.Configurations;
using Teamspace.Models;

namespace Teamspace.Repositories
{
    public class QuestionRepo
    {
        private readonly AppDbContext _db;
        public QuestionRepo(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Question>> GetAll(int ExamId)
        {
            return await _db.Questions
                .Where(q => q.ExamId == ExamId)
                .Select(q => new Question
                {
                    Id = q.Id,
                    Title = q.Title,
                    Image = q.Image,
                    File = q.File,
                    Type = q.Type,
                    CorrectAns = q.CorrectAns,
                    Grade = q.Grade,
                    Choices = q.Choices.Select(c => new Choice
                    {
                        Id = c.Id,
                        choice = c.choice,
                    }).ToList()
                }).ToListAsync();
        }

        public async Task<Question> GetById(int ExamId, int QuestionId)
        {
            return await _db.Questions
                .Where(q => q.ExamId == ExamId && q.Id == QuestionId)
                .Select(q => new Question
                {
                    Id = q.Id,
                    Title = q.Title,
                    Image = q.Image,
                    File = q.File,
                    Type = q.Type,
                    CorrectAns = q.CorrectAns,
                    Grade = q.Grade,
                    Choices = q.Choices.Select(c => new Choice
                    {
                        Id = c.Id,
                        choice = c.choice,
                    }).ToList()
                }).FirstOrDefaultAsync();
        }

        public async Task<bool> Add(Question question)
        {
            await _db.Questions.AddAsync(question);
            return true;
        }

        public async Task<bool> Update(Question question)
        {
            _db.Questions.Update(question);
            return true;
        }


        public async Task<bool>Delete(int QuestionId)
        {
            var question = await _db.Questions.FirstOrDefaultAsync(q => q.Id == QuestionId);
            foreach (var choice in question.Choices)
            {
                _db.Choices.Remove(choice);
            }
            _db.Questions.Remove(question);
            return false;
        }
    }
}
