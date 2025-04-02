using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teamspace.Migrations
{
    /// <inheritdoc />
    public partial class EditSubjecttable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Subjects_SubjectId",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_Materials_Subjects_SubjectId",
                table: "Materials");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Subjects_SubjectId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Registerations_Subjects_SubjectId",
                table: "Registerations");

            migrationBuilder.DropIndex(
                name: "IX_Registerations_SubjectId",
                table: "Registerations");

            migrationBuilder.DropIndex(
                name: "IX_Posts_SubjectId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Materials_SubjectId",
                table: "Materials");

            migrationBuilder.DropIndex(
                name: "IX_Exams_SubjectId",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Registerations");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Exams");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "Registerations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "Posts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "Materials",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "Exams",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Registerations_SubjectId",
                table: "Registerations",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_SubjectId",
                table: "Posts",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_SubjectId",
                table: "Materials",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_SubjectId",
                table: "Exams",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Subjects_SubjectId",
                table: "Exams",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_Subjects_SubjectId",
                table: "Materials",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Subjects_SubjectId",
                table: "Posts",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Registerations_Subjects_SubjectId",
                table: "Registerations",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id");
        }
    }
}
