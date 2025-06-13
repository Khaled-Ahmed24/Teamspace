using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teamspace.Migrations
{
    /// <inheritdoc />
    public partial class hi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Staffs_StaffId",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostComments",
                table: "PostComments");

            migrationBuilder.RenameColumn(
                name: "StaffId",
                table: "Posts",
                newName: "staffId");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_StaffId",
                table: "Posts",
                newName: "IX_Posts_staffId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostComments",
                table: "PostComments",
                columns: new[] { "Id", "PostId" });

            migrationBuilder.CreateIndex(
                name: "IX_PostComments_PostId",
                table: "PostComments",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Staffs_staffId",
                table: "Posts",
                column: "staffId",
                principalTable: "Staffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Staffs_staffId",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostComments",
                table: "PostComments");

            migrationBuilder.DropIndex(
                name: "IX_PostComments_PostId",
                table: "PostComments");

            migrationBuilder.RenameColumn(
                name: "staffId",
                table: "Posts",
                newName: "StaffId");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_staffId",
                table: "Posts",
                newName: "IX_Posts_StaffId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostComments",
                table: "PostComments",
                columns: new[] { "PostId", "Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Staffs_StaffId",
                table: "Posts",
                column: "StaffId",
                principalTable: "Staffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
