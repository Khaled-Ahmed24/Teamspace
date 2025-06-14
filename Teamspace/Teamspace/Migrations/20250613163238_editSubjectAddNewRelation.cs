using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teamspace.Migrations
{
    /// <inheritdoc />
    public partial class editSubjectAddNewRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DependentId",
                table: "Subjects",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_DependentId",
                table: "Subjects",
                column: "DependentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Subjects_DependentId",
                table: "Subjects",
                column: "DependentId",
                principalTable: "Subjects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Subjects_DependentId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_DependentId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "DependentId",
                table: "Subjects");
        }
    }
}
