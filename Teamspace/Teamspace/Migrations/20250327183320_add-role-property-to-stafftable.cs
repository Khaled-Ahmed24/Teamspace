using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teamspace.Migrations
{
    /// <inheritdoc />
    public partial class addrolepropertytostafftable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Staffs",
                type: "varbinary(max)",
                nullable: true,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Staffs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Staffs");
        }
    }
}
