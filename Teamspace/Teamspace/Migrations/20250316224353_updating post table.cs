using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teamspace.Migrations
{
    /// <inheritdoc />
    public partial class updatingposttable : Migration
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

            migrationBuilder.DropIndex(
                name: "IX_PostComments_PostStaffId_PostCourseId",
                table: "PostComments");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "PostUploadedAt",
                table: "PostComments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Posts",
                table: "Posts",
                columns: new[] { "StaffId", "CourseId", "UploadedAt" });

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

            migrationBuilder.DropIndex(
                name: "IX_PostComments_PostStaffId_PostCourseId_PostUploadedAt",
                table: "PostComments");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "PostUploadedAt",
                table: "PostComments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Posts",
                table: "Posts",
                columns: new[] { "StaffId", "CourseId" });

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
