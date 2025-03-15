﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Teamspace.Configurations;

#nullable disable

namespace Teamspace.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Teamspace.Models.AssignmentAns", b =>
                {
                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("QuestionId", "StudentId");

                    b.HasIndex("StudentId");

                    b.ToTable("AssignmentAnss");
                });

            modelBuilder.Entity("Teamspace.Models.Choice", b =>
                {
                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<string>("choice")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("AddedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("QuestionId", "choice");

                    b.ToTable("Choices");
                });

            modelBuilder.Entity("Teamspace.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Semester")
                        .HasColumnType("int");

                    b.Property<string>("SubjectDepartment")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SubjectLevel")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SubjectDepartment", "SubjectLevel");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Teamspace.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Teamspace.Models.Exam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<int>("Grade")
                        .HasColumnType("int");

                    b.Property<int>("StaffId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<byte>("type")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("StaffId");

                    b.ToTable("Exams");
                });

            modelBuilder.Entity("Teamspace.Models.Material", b =>
                {
                    b.Property<int>("StaffId")
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UploadedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("StaffId", "CourseId");

                    b.HasIndex("CourseId");

                    b.ToTable("Materials");
                });

            modelBuilder.Entity("Teamspace.Models.News", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("StaffId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StaffId");

                    b.ToTable("News");
                });

            modelBuilder.Entity("Teamspace.Models.NewsComment", b =>
                {
                    b.Property<int>("NewsId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("SentAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CommenterId")
                        .HasColumnType("int");

                    b.HasKey("NewsId", "Content", "SentAt");

                    b.ToTable("NewsComments");
                });

            modelBuilder.Entity("Teamspace.Models.Post", b =>
                {
                    b.Property<int>("StaffId")
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UploadedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("StaffId", "CourseId");

                    b.HasIndex("CourseId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Teamspace.Models.PostComment", b =>
                {
                    b.Property<int>("PostStaffId")
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("SentAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CommenterId")
                        .HasColumnType("int");

                    b.Property<int>("PostCourseId")
                        .HasColumnType("int");

                    b.HasKey("PostStaffId", "CourseId", "Content", "SentAt");

                    b.HasIndex("PostStaffId", "PostCourseId");

                    b.ToTable("PostComments");
                });

            modelBuilder.Entity("Teamspace.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CorrectAns")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ExamId")
                        .HasColumnType("int");

                    b.Property<int>("Grade")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("Type")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.HasIndex("ExamId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Teamspace.Models.QuestionAns", b =>
                {
                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<string>("StudentAns")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("QuestionId", "StudentId");

                    b.HasIndex("StudentId");

                    b.ToTable("QuestionAnss");
                });

            modelBuilder.Entity("Teamspace.Models.Registeration", b =>
                {
                    b.Property<int>("StaffId")
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.HasKey("StaffId", "CourseId");

                    b.HasIndex("CourseId");

                    b.ToTable("Registerations");
                });

            modelBuilder.Entity("Teamspace.Models.Staff", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Gender")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NationalId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Staffs");
                });

            modelBuilder.Entity("Teamspace.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Gender")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NationalId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Teamspace.Models.Subject", b =>
                {
                    b.Property<string>("Department")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Level")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<int>("Hours")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Department", "Level");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("Teamspace.Models.AssignmentAns", b =>
                {
                    b.HasOne("Teamspace.Models.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Teamspace.Models.Student", "Student")
                        .WithMany("AssignmentAnss")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Teamspace.Models.Choice", b =>
                {
                    b.HasOne("Teamspace.Models.Question", "Question")
                        .WithMany("Choices")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("Teamspace.Models.Course", b =>
                {
                    b.HasOne("Teamspace.Models.Subject", "Subject")
                        .WithMany("Courses")
                        .HasForeignKey("SubjectDepartment", "SubjectLevel")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("Teamspace.Models.Exam", b =>
                {
                    b.HasOne("Teamspace.Models.Course", "Course")
                        .WithMany("Exams")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Teamspace.Models.Staff", "Staff")
                        .WithMany("Exams")
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("Teamspace.Models.Material", b =>
                {
                    b.HasOne("Teamspace.Models.Course", "Course")
                        .WithMany("Materials")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Teamspace.Models.Staff", "Staff")
                        .WithMany("Materials")
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("Teamspace.Models.News", b =>
                {
                    b.HasOne("Teamspace.Models.Staff", "Staff")
                        .WithMany("News")
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("Teamspace.Models.NewsComment", b =>
                {
                    b.HasOne("Teamspace.Models.News", "News")
                        .WithMany()
                        .HasForeignKey("NewsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("News");
                });

            modelBuilder.Entity("Teamspace.Models.Post", b =>
                {
                    b.HasOne("Teamspace.Models.Course", "Course")
                        .WithMany("Posts")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Teamspace.Models.Staff", "Staff")
                        .WithMany("Posts")
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("Teamspace.Models.PostComment", b =>
                {
                    b.HasOne("Teamspace.Models.Post", "Post")
                        .WithMany("PostComments")
                        .HasForeignKey("PostStaffId", "PostCourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");
                });

            modelBuilder.Entity("Teamspace.Models.Question", b =>
                {
                    b.HasOne("Teamspace.Models.Exam", "Exam")
                        .WithMany("Questions")
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exam");
                });

            modelBuilder.Entity("Teamspace.Models.QuestionAns", b =>
                {
                    b.HasOne("Teamspace.Models.Question", "Question")
                        .WithMany("QuestionAnss")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Teamspace.Models.Student", "Student")
                        .WithMany("QuestionAnss")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Teamspace.Models.Registeration", b =>
                {
                    b.HasOne("Teamspace.Models.Course", "Course")
                        .WithMany("Registerations")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Teamspace.Models.Staff", "Staff")
                        .WithMany("Registerations")
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("Teamspace.Models.Student", b =>
                {
                    b.HasOne("Teamspace.Models.Department", "Department")
                        .WithMany("Students")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("Teamspace.Models.Course", b =>
                {
                    b.Navigation("Exams");

                    b.Navigation("Materials");

                    b.Navigation("Posts");

                    b.Navigation("Registerations");
                });

            modelBuilder.Entity("Teamspace.Models.Department", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("Teamspace.Models.Exam", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("Teamspace.Models.Post", b =>
                {
                    b.Navigation("PostComments");
                });

            modelBuilder.Entity("Teamspace.Models.Question", b =>
                {
                    b.Navigation("Choices");

                    b.Navigation("QuestionAnss");
                });

            modelBuilder.Entity("Teamspace.Models.Staff", b =>
                {
                    b.Navigation("Exams");

                    b.Navigation("Materials");

                    b.Navigation("News");

                    b.Navigation("Posts");

                    b.Navigation("Registerations");
                });

            modelBuilder.Entity("Teamspace.Models.Student", b =>
                {
                    b.Navigation("AssignmentAnss");

                    b.Navigation("QuestionAnss");
                });

            modelBuilder.Entity("Teamspace.Models.Subject", b =>
                {
                    b.Navigation("Courses");
                });
#pragma warning restore 612, 618
        }
    }
}
