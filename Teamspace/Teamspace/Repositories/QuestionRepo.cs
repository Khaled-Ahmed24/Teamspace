using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Teamspace.Configurations;
using Teamspace.DTO;
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
            var exam = await _db.Exams.Where(e => e.Id == ExamId).FirstOrDefaultAsync();
            if(exam == null) return new List<Question>();
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

        public async Task<Question?> GetById(int QuestionId)
        {
            return await _db.Questions
                .Where(q => q.Id == QuestionId)
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

        public async Task<bool> Add(int examId, QuestionDTO question)
        {
            var q = new Question
            {
                Title = question.Title,
                Type = question.Type,
                CorrectAns = question.CorrectAns,
                Grade = question.Grade,
                ExamId = examId,
            };
            using (var stream = new MemoryStream())
            {
                if (question.File != null && question.File.Length > 0)
                {
                    await question.File.CopyToAsync(stream);
                    q.File = stream.ToArray();
                }
            }
            using (var stream = new MemoryStream())
            {
                if(question.Image != null && question.Image.Length > 0)
                {
                    await question.Image.CopyToAsync(stream);
                    q.Image = stream.ToArray();
                }
            }
            await _db.Questions.AddAsync(q);
            await Save();
            if(question.Type == QuestionType.Written)
                return true; // No choices to add for written questions
            foreach (var choice in question.Choices)
            {
                await _db.Choices.AddAsync(new Choice
                {
                    choice = choice.choice,
                    QuestionId = q.Id,
                    AddedOn = DateTime.Now
                });
            }
            await Save();
            return true;
        }

        public async Task<bool> Update(QuestionDTO question)
        {
            var q = await _db.Questions.FirstOrDefaultAsync(q => q.Id == question.Id);
            if (q == null) return false;
            q.Title = question.Title;
            q.Type = question.Type;
            q.CorrectAns = question.CorrectAns;
            q.Grade = question.Grade;
            using(var stream = new MemoryStream())
            {
                if(question.File != null && question.File.Length > 0)
                {
                    await question.File.CopyToAsync(stream);
                    q.File = stream.ToArray();
                }  
            }
            using (var stream = new MemoryStream())
            {
                if(question.Image != null && question.Image.Length > 0)
                {
                    await question.Image.CopyToAsync(stream);
                    q.Image = stream.ToArray();
                }
            }
            var cur_choices = await _db.Choices.Where(c => c.QuestionId == q.Id).ToListAsync();
            foreach(var choice in question.Choices)
            {
                if(cur_choices.Find(c => c.Id == choice.Id) == null)
                {
                    await _db.Choices.AddAsync(new Choice
                    {
                        choice = choice.choice,
                        QuestionId = q.Id,
                        AddedOn = DateTime.Now
                    });
                }
                else
                {
                    var cur_choice = cur_choices.Find(c => c.Id == choice.Id);
                    if (cur_choice == null) continue;
                    cur_choice.choice = choice.choice;
                    _db.Choices.Update(cur_choice);
                }
            }
            foreach(var choice in cur_choices)
            {
                if (question.Choices.Find(c => c.Id == choice.Id) == null)
                    _db.Choices.Remove(choice);
            }
            return true;
        }


        public async Task<bool>DeleteQuestion(int QuestionId)
        {
            var question = await GetById(QuestionId);
            if (question == null) return false;
            foreach (var choice in question.Choices)
                await DeleteChoice(QuestionId, choice.Id);
            _db.Questions.Remove(question);
            return true;
        }

        public async Task<bool> DeleteChoice(int QuestionId, int ChoiceId)
        {
            var choice = await _db.Choices.FirstOrDefaultAsync(c => c.Id == ChoiceId && c.QuestionId == QuestionId);
            if (choice != null)
            {
                _db.Choices.Remove(choice);
                return true;
            }
            return false;
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
            return;
        }
    }
}
