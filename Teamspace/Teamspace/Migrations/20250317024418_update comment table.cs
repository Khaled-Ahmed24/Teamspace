using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teamspace.Migrations
{
    /// <inheritdoc />
    public partial class updatecommenttable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PostComments",
                table: "PostComments");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostComments",
                table: "PostComments",
                columns: new[] { "PostStaffId", "CourseId", "UploadedAt", "SentAt" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PostComments",
                table: "PostComments");

            migrationBuilder.DropColumn(
                name: "UploadedAt",
                table: "PostComments");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "PostComments",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostComments",
                table: "PostComments",
                columns: new[] { "PostStaffId", "CourseId", "Content", "SentAt" });
        }
    }
}
