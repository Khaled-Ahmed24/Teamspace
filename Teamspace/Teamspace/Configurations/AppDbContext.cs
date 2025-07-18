﻿using Microsoft.EntityFrameworkCore;
using Teamspace.Models;

namespace Teamspace.Configurations
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           /* modelBuilder.Entity<Subject>().
                HasKey(s => new { s.Department, s.Level });*/

            modelBuilder.Entity<QuestionAns>().
              HasKey(s => new { s.QuestionId, s.StudentId });

            modelBuilder.Entity<AssignmentAns>().
                      HasKey(s => new { s.QuestionId, s.StudentId });
            modelBuilder.Entity<Registeration>().
                HasKey(s => new { s.StaffId, s.CourseId});
            modelBuilder.Entity<Material>().
                HasKey(s => new {s.Id });
            modelBuilder.Entity<Post>().
                HasKey(s => new { s.Id });
            /*modelBuilder.Entity<Choice>().
                HasKey(s => new { s.QuestionId, s.choice });*/
            modelBuilder.Entity<PostComment>().
                HasKey(s => new {  s.Id});
            modelBuilder.Entity<NewsComment>().
                HasKey(s => new { s.NewsId, s.Content, s.SentAt });
            modelBuilder.Entity<LevelSchedule>()
            .HasKey(ls => new { ls.DepartmentId, ls.Level }); // Composite Key

            modelBuilder.Entity<LevelSchedule>()
                .HasOne(ls => ls.Department)
                .WithMany(d => d.LevelSchedules)
                .HasForeignKey(ls => ls.DepartmentId);

        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<AssignmentAns> AssignmentAnss { get; set; }
        public DbSet<Choice> Choices { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<NewsComment> NewsComments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionAns> QuestionAnss { get; set; }
        public DbSet<Registeration> Registerations { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<StudentStatus> StudentStatuses { get; set; }

        public DbSet<DoctorSchedule> DoctorSchedules { get; set; }

        public DbSet<CourseDepartment> CourseDepartments { get; set; }

        public DbSet<LevelSchedule> LevelSchedules { get; set; }

        public DbSet<ChatMessage> ChatMessages { get; set; }

        public DbSet<GroupMessage> GroupMessages { get; set; }

        public DbSet<Notification> Notifications { get; set; }

    }
}
