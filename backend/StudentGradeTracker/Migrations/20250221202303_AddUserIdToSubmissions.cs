using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentGradeTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToSubmissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Users_StudentId",
                table: "Submissions");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Submissions",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Submissions_StudentId",
                table: "Submissions",
                newName: "IX_Submissions_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Users_UserId",
                table: "Submissions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Users_UserId",
                table: "Submissions");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Submissions",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Submissions_UserId",
                table: "Submissions",
                newName: "IX_Submissions_StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Users_StudentId",
                table: "Submissions",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
