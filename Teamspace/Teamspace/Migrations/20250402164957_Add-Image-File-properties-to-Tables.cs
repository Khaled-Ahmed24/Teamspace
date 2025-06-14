using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teamspace.Migrations
{
    /// <inheritdoc />
    public partial class AddImageFilepropertiestoTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostComments_Posts_PostStaffId_PostCourseId",
                table: "PostComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Posts",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostComments",
                table: "PostComments");

            migrationBuilder.DropIndex(
                name: "IX_PostComments_PostStaffId_PostCourseId",
                table: "PostComments");

            migrationBuilder.AddColumn<byte[]>(
                name: "File",
                table: "Questions",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Questions",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Posts",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "PostComments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadedAt",
                table: "PostComments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PostUploadedAt",
                table: "PostComments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<byte[]>(
                name: "File",
                table: "AssignmentAnss",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Posts",
                table: "Posts",
                columns: new[] { "StaffId", "CourseId", "UploadedAt" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostComments",
                table: "PostComments",
                columns: new[] { "PostStaffId", "CourseId", "UploadedAt", "SentAt" });

            migrationBuilder.CreateIndex(
                name: "IX_PostComments_PostStaffId_PostCourseId_PostUploadedAt",
                table: "PostComments",
                columns: new[] { "PostStaffId", "PostCourseId", "PostUploadedAt" });

            migrationBuilder.AddForeignKey(
                name: "FK_PostComments_Posts_PostStaffId_PostCourseId_PostUploadedAt",
                table: "PostComments",
                columns: new[] { "PostStaffId", "PostCourseId", "PostUploadedAt" },
                principalTable: "Posts",
                principalColumns: new[] { "StaffId", "CourseId", "UploadedAt" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostComments_Posts_PostStaffId_PostCourseId_PostUploadedAt",
                table: "PostComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Posts",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostComments",
                table: "PostComments");

            migrationBuilder.DropIndex(
                name: "IX_PostComments_PostStaffId_PostCourseId_PostUploadedAt",
                table: "PostComments");

            migrationBuilder.DropColumn(
                name: "File",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "UploadedAt",
                table: "PostComments");

            migrationBuilder.DropColumn(
                name: "PostUploadedAt",
                table: "PostComments");

            migrationBuilder.DropColumn(
                name: "File",
                table: "AssignmentAnss");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "PostComments",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Posts",
                table: "Posts",
                columns: new[] { "StaffId", "CourseId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostComments",
                table: "PostComments",
                columns: new[] { "PostStaffId", "CourseId", "Content", "SentAt" });

            migrationBuilder.CreateIndex(
                name: "IX_PostComments_PostStaffId_PostCourseId",
                table: "PostComments",
                columns: new[] { "PostStaffId", "PostCourseId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PostComments_Posts_PostStaffId_PostCourseId",
                table: "PostComments",
                columns: new[] { "PostStaffId", "PostCourseId" },
                principalTable: "Posts",
                principalColumns: new[] { "StaffId", "CourseId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
