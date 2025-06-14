using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teamspace.Migrations
{
    /// <inheritdoc />
    public partial class addidpktochoicetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Choices",
                table: "Choices");

            migrationBuilder.AlterColumn<string>(
                name: "choice",
                table: "Choices",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Choices",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Choices",
                table: "Choices",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Choices_QuestionId",
                table: "Choices",
                column: "QuestionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Choices",
                table: "Choices");

            migrationBuilder.DropIndex(
                name: "IX_Choices_QuestionId",
                table: "Choices");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Choices");

            migrationBuilder.AlterColumn<string>(
                name: "choice",
                table: "Choices",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Choices",
                table: "Choices",
                columns: new[] { "QuestionId", "choice" });
        }
    }
}
