using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teamspace.Migrations
{
    /// <inheritdoc />
    public partial class AddIdToStudentAndStaffEditEmailForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentAnss_Students_StudentEmail1",
                table: "AssignmentAnss");

            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Staffs_StaffEmail1",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_Materials_Staffs_StaffEmail1",
                table: "Materials");

            migrationBuilder.DropForeignKey(
                name: "FK_News_Staffs_StaffEmail1",
                table: "News");

            migrationBuilder.DropForeignKey(
                name: "FK_PostComments_Posts_PostStaffEmail_PostSubjectDepartment_PostSubjectLevel",
                table: "PostComments");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Staffs_StaffEmail1",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionAnss_Students_StudentEmail1",
                table: "QuestionAnss");

            migrationBuilder.DropForeignKey(
                name: "FK_Registerations_Staffs_StaffEmail1",
                table: "Registerations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Staffs",
                table: "Staffs");

            migrationBuilder.DropIndex(
                name: "IX_Registerations_StaffEmail1",
                table: "Registerations");

            migrationBuilder.DropIndex(
                name: "IX_QuestionAnss_StudentEmail1",
                table: "QuestionAnss");

            migrationBuilder.DropIndex(
                name: "IX_Posts_StaffEmail1",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostComments",
                table: "PostComments");

            migrationBuilder.DropIndex(
                name: "IX_News_StaffEmail1",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_Materials_StaffEmail1",
                table: "Materials");

            migrationBuilder.DropIndex(
                name: "IX_Exams_StaffEmail1",
                table: "Exams");

            migrationBuilder.DropIndex(
                name: "IX_AssignmentAnss_StudentEmail1",
                table: "AssignmentAnss");

            migrationBuilder.DropColumn(
                name: "StaffEmail1",
                table: "Registerations");

            migrationBuilder.DropColumn(
                name: "StudentEmail1",
                table: "QuestionAnss");

            migrationBuilder.DropColumn(
                name: "StaffEmail1",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "CommenterEmail",
                table: "PostComments");

            migrationBuilder.DropColumn(
                name: "CommenterEmail",
                table: "NewsComments");

            migrationBuilder.DropColumn(
                name: "StaffEmail1",
                table: "News");

            migrationBuilder.DropColumn(
                name: "StaffEmail1",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "StaffEmail1",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "StudentEmail1",
                table: "AssignmentAnss");

            migrationBuilder.RenameColumn(
                name: "StaffEmail",
                table: "Registerations",
                newName: "StaffId");

            migrationBuilder.RenameColumn(
                name: "StudentEmail",
                table: "QuestionAnss",
                newName: "StudentId");

            migrationBuilder.RenameColumn(
                name: "StaffEmail",
                table: "Posts",
                newName: "StaffId");

            migrationBuilder.RenameColumn(
                name: "PostStaffEmail",
                table: "PostComments",
                newName: "CommenterId");

            migrationBuilder.RenameColumn(
                name: "StaffEmail",
                table: "News",
                newName: "StaffId");

            migrationBuilder.RenameColumn(
                name: "StaffEmail",
                table: "Materials",
                newName: "StaffId");

            migrationBuilder.RenameColumn(
                name: "StaffEmail",
                table: "Exams",
                newName: "StaffId");

            migrationBuilder.RenameColumn(
                name: "StudentEmail",
                table: "AssignmentAnss",
                newName: "StudentId");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Staffs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Staffs",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "PostStaffId",
                table: "PostComments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CommenterId",
                table: "NewsComments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Staffs",
                table: "Staffs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostComments",
                table: "PostComments",
                columns: new[] { "PostStaffId", "PostSubjectDepartment", "PostSubjectLevel", "Content", "SentAt" });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnss_StudentId",
                table: "QuestionAnss",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_News_StaffId",
                table: "News",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_StaffId",
                table: "Exams",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentAnss_StudentId",
                table: "AssignmentAnss",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentAnss_Students_StudentId",
                table: "AssignmentAnss",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Staffs_StaffId",
                table: "Exams",
                column: "StaffId",
                principalTable: "Staffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_Staffs_StaffId",
                table: "Materials",
                column: "StaffId",
                principalTable: "Staffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_News_Staffs_StaffId",
                table: "News",
                column: "StaffId",
                principalTable: "Staffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostComments_Posts_PostStaffId_PostSubjectDepartment_PostSubjectLevel",
                table: "PostComments",
                columns: new[] { "PostStaffId", "PostSubjectDepartment", "PostSubjectLevel" },
                principalTable: "Posts",
                principalColumns: new[] { "StaffId", "SubjectDepartment", "SubjectLevel" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Staffs_StaffId",
                table: "Posts",
                column: "StaffId",
                principalTable: "Staffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionAnss_Students_StudentId",
                table: "QuestionAnss",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Registerations_Staffs_StaffId",
                table: "Registerations",
                column: "StaffId",
                principalTable: "Staffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentAnss_Students_StudentId",
                table: "AssignmentAnss");

            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Staffs_StaffId",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_Materials_Staffs_StaffId",
                table: "Materials");

            migrationBuilder.DropForeignKey(
                name: "FK_News_Staffs_StaffId",
                table: "News");

            migrationBuilder.DropForeignKey(
                name: "FK_PostComments_Posts_PostStaffId_PostSubjectDepartment_PostSubjectLevel",
                table: "PostComments");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Staffs_StaffId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionAnss_Students_StudentId",
                table: "QuestionAnss");

            migrationBuilder.DropForeignKey(
                name: "FK_Registerations_Staffs_StaffId",
                table: "Registerations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Staffs",
                table: "Staffs");

            migrationBuilder.DropIndex(
                name: "IX_QuestionAnss_StudentId",
                table: "QuestionAnss");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostComments",
                table: "PostComments");

            migrationBuilder.DropIndex(
                name: "IX_News_StaffId",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_Exams_StaffId",
                table: "Exams");

            migrationBuilder.DropIndex(
                name: "IX_AssignmentAnss_StudentId",
                table: "AssignmentAnss");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "PostStaffId",
                table: "PostComments");

            migrationBuilder.DropColumn(
                name: "CommenterId",
                table: "NewsComments");

            migrationBuilder.RenameColumn(
                name: "StaffId",
                table: "Registerations",
                newName: "StaffEmail");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "QuestionAnss",
                newName: "StudentEmail");

            migrationBuilder.RenameColumn(
                name: "StaffId",
                table: "Posts",
                newName: "StaffEmail");

            migrationBuilder.RenameColumn(
                name: "CommenterId",
                table: "PostComments",
                newName: "PostStaffEmail");

            migrationBuilder.RenameColumn(
                name: "StaffId",
                table: "News",
                newName: "StaffEmail");

            migrationBuilder.RenameColumn(
                name: "StaffId",
                table: "Materials",
                newName: "StaffEmail");

            migrationBuilder.RenameColumn(
                name: "StaffId",
                table: "Exams",
                newName: "StaffEmail");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "AssignmentAnss",
                newName: "StudentEmail");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Students",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Staffs",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "StaffEmail1",
                table: "Registerations",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StudentEmail1",
                table: "QuestionAnss",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StaffEmail1",
                table: "Posts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CommenterEmail",
                table: "PostComments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CommenterEmail",
                table: "NewsComments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StaffEmail1",
                table: "News",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StaffEmail1",
                table: "Materials",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StaffEmail1",
                table: "Exams",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StudentEmail1",
                table: "AssignmentAnss",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Staffs",
                table: "Staffs",
                column: "Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostComments",
                table: "PostComments",
                columns: new[] { "PostStaffEmail", "PostSubjectDepartment", "PostSubjectLevel", "Content", "SentAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Registerations_StaffEmail1",
                table: "Registerations",
                column: "StaffEmail1");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnss_StudentEmail1",
                table: "QuestionAnss",
                column: "StudentEmail1");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_StaffEmail1",
                table: "Posts",
                column: "StaffEmail1");

            migrationBuilder.CreateIndex(
                name: "IX_News_StaffEmail1",
                table: "News",
                column: "StaffEmail1");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_StaffEmail1",
                table: "Materials",
                column: "StaffEmail1");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_StaffEmail1",
                table: "Exams",
                column: "StaffEmail1");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentAnss_StudentEmail1",
                table: "AssignmentAnss",
                column: "StudentEmail1");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentAnss_Students_StudentEmail1",
                table: "AssignmentAnss",
                column: "StudentEmail1",
                principalTable: "Students",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Staffs_StaffEmail1",
                table: "Exams",
                column: "StaffEmail1",
                principalTable: "Staffs",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_Staffs_StaffEmail1",
                table: "Materials",
                column: "StaffEmail1",
                principalTable: "Staffs",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_News_Staffs_StaffEmail1",
                table: "News",
                column: "StaffEmail1",
                principalTable: "Staffs",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostComments_Posts_PostStaffEmail_PostSubjectDepartment_PostSubjectLevel",
                table: "PostComments",
                columns: new[] { "PostStaffEmail", "PostSubjectDepartment", "PostSubjectLevel" },
                principalTable: "Posts",
                principalColumns: new[] { "StaffEmail", "SubjectDepartment", "SubjectLevel" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Staffs_StaffEmail1",
                table: "Posts",
                column: "StaffEmail1",
                principalTable: "Staffs",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionAnss_Students_StudentEmail1",
                table: "QuestionAnss",
                column: "StudentEmail1",
                principalTable: "Students",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Registerations_Staffs_StaffEmail1",
                table: "Registerations",
                column: "StaffEmail1",
                principalTable: "Staffs",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
