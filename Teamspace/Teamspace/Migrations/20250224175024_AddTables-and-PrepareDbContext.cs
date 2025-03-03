﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teamspace.Migrations
{
    /// <inheritdoc />
    public partial class AddTablesandPrepareDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<bool>(type: "bit", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NationalId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Department = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hours = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => new { x.Department, x.Level });
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<bool>(type: "bit", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NationalId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Email);
                    table.ForeignKey(
                        name: "FK_Students_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StaffEmail = table.Column<int>(type: "int", nullable: false),
                    StaffEmail1 = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                    table.ForeignKey(
                        name: "FK_News_Staffs_StaffEmail1",
                        column: x => x.StaffEmail1,
                        principalTable: "Staffs",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type = table.Column<byte>(type: "tinyint", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Grade = table.Column<int>(type: "int", nullable: false),
                    StaffEmail = table.Column<int>(type: "int", nullable: false),
                    StaffEmail1 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubjectDepartment = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubjectLevel = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exams_Staffs_StaffEmail1",
                        column: x => x.StaffEmail1,
                        principalTable: "Staffs",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Exams_Subjects_SubjectDepartment_SubjectLevel",
                        columns: x => new { x.SubjectDepartment, x.SubjectLevel },
                        principalTable: "Subjects",
                        principalColumns: new[] { "Department", "Level" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    StaffEmail = table.Column<int>(type: "int", nullable: false),
                    SubjectDepartment = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubjectLevel = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StaffEmail1 = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => new { x.StaffEmail, x.SubjectDepartment, x.SubjectLevel });
                    table.ForeignKey(
                        name: "FK_Materials_Staffs_StaffEmail1",
                        column: x => x.StaffEmail1,
                        principalTable: "Staffs",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Materials_Subjects_SubjectDepartment_SubjectLevel",
                        columns: x => new { x.SubjectDepartment, x.SubjectLevel },
                        principalTable: "Subjects",
                        principalColumns: new[] { "Department", "Level" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    StaffEmail = table.Column<int>(type: "int", nullable: false),
                    SubjectDepartment = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubjectLevel = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StaffEmail1 = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => new { x.StaffEmail, x.SubjectDepartment, x.SubjectLevel });
                    table.ForeignKey(
                        name: "FK_Posts_Staffs_StaffEmail1",
                        column: x => x.StaffEmail1,
                        principalTable: "Staffs",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Posts_Subjects_SubjectDepartment_SubjectLevel",
                        columns: x => new { x.SubjectDepartment, x.SubjectLevel },
                        principalTable: "Subjects",
                        principalColumns: new[] { "Department", "Level" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Registerations",
                columns: table => new
                {
                    StaffEmail = table.Column<int>(type: "int", nullable: false),
                    SubjectDepartment = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubjectLevel = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StaffEmail1 = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registerations", x => new { x.StaffEmail, x.SubjectDepartment, x.SubjectLevel });
                    table.ForeignKey(
                        name: "FK_Registerations_Staffs_StaffEmail1",
                        column: x => x.StaffEmail1,
                        principalTable: "Staffs",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Registerations_Subjects_SubjectDepartment_SubjectLevel",
                        columns: x => new { x.SubjectDepartment, x.SubjectLevel },
                        principalTable: "Subjects",
                        principalColumns: new[] { "Department", "Level" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewsComments",
                columns: table => new
                {
                    NewsId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CommenterEmail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsComments", x => new { x.NewsId, x.Content, x.SentAt });
                    table.ForeignKey(
                        name: "FK_NewsComments_News_NewsId",
                        column: x => x.NewsId,
                        principalTable: "News",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<byte>(type: "tinyint", nullable: false),
                    CorrectAns = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Grade = table.Column<int>(type: "int", nullable: false),
                    ExamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Exams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostComments",
                columns: table => new
                {
                    PostStaffEmail = table.Column<int>(type: "int", nullable: false),
                    PostSubjectDepartment = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PostSubjectLevel = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CommenterEmail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostComments", x => new { x.PostStaffEmail, x.PostSubjectDepartment, x.PostSubjectLevel, x.Content, x.SentAt });
                    table.ForeignKey(
                        name: "FK_PostComments_Posts_PostStaffEmail_PostSubjectDepartment_PostSubjectLevel",
                        columns: x => new { x.PostStaffEmail, x.PostSubjectDepartment, x.PostSubjectLevel },
                        principalTable: "Posts",
                        principalColumns: new[] { "StaffEmail", "SubjectDepartment", "SubjectLevel" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssignmentAnss",
                columns: table => new
                {
                    StudentEmail = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    StudentEmail1 = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentAnss", x => new { x.QuestionId, x.StudentEmail });
                    table.ForeignKey(
                        name: "FK_AssignmentAnss_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignmentAnss_Students_StudentEmail1",
                        column: x => x.StudentEmail1,
                        principalTable: "Students",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Choices",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    choice = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AddedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Choices", x => new { x.QuestionId, x.choice });
                    table.ForeignKey(
                        name: "FK_Choices_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionAnss",
                columns: table => new
                {
                    StudentEmail = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    StudentEmail1 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StudentAns = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionAnss", x => new { x.QuestionId, x.StudentEmail });
                    table.ForeignKey(
                        name: "FK_QuestionAnss_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionAnss_Students_StudentEmail1",
                        column: x => x.StudentEmail1,
                        principalTable: "Students",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentAnss_StudentEmail1",
                table: "AssignmentAnss",
                column: "StudentEmail1");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_StaffEmail1",
                table: "Exams",
                column: "StaffEmail1");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_SubjectDepartment_SubjectLevel",
                table: "Exams",
                columns: new[] { "SubjectDepartment", "SubjectLevel" });

            migrationBuilder.CreateIndex(
                name: "IX_Materials_StaffEmail1",
                table: "Materials",
                column: "StaffEmail1");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_SubjectDepartment_SubjectLevel",
                table: "Materials",
                columns: new[] { "SubjectDepartment", "SubjectLevel" });

            migrationBuilder.CreateIndex(
                name: "IX_News_StaffEmail1",
                table: "News",
                column: "StaffEmail1");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_StaffEmail1",
                table: "Posts",
                column: "StaffEmail1");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_SubjectDepartment_SubjectLevel",
                table: "Posts",
                columns: new[] { "SubjectDepartment", "SubjectLevel" });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnss_StudentEmail1",
                table: "QuestionAnss",
                column: "StudentEmail1");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_ExamId",
                table: "Questions",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_Registerations_StaffEmail1",
                table: "Registerations",
                column: "StaffEmail1");

            migrationBuilder.CreateIndex(
                name: "IX_Registerations_SubjectDepartment_SubjectLevel",
                table: "Registerations",
                columns: new[] { "SubjectDepartment", "SubjectLevel" });

            migrationBuilder.CreateIndex(
                name: "IX_Students_DepartmentId",
                table: "Students",
                column: "DepartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignmentAnss");

            migrationBuilder.DropTable(
                name: "Choices");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "NewsComments");

            migrationBuilder.DropTable(
                name: "PostComments");

            migrationBuilder.DropTable(
                name: "QuestionAnss");

            migrationBuilder.DropTable(
                name: "Registerations");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Exams");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Staffs");

            migrationBuilder.DropTable(
                name: "Subjects");
        }
    }
}
