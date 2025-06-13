using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teamspace.Migrations
{
    /// <inheritdoc />
    public partial class addidtomaterials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Materials",
                table: "Materials");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Materials",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Materials",
                table: "Materials",
                columns: new[] { "Id", "StaffId", "CourseId" });

            migrationBuilder.CreateIndex(
                name: "IX_Materials_StaffId",
                table: "Materials",
                column: "StaffId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Materials",
                table: "Materials");

            migrationBuilder.DropIndex(
                name: "IX_Materials_StaffId",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Materials");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Materials",
                table: "Materials",
                columns: new[] { "StaffId", "CourseId" });
        }
    }
}
