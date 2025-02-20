using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentGradeTracker.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAssignmentStudentAssignmentTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Users_TeacherId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAssignments_Users_StudentId",
                table: "StudentAssignments");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "StudentAssignments",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "TeacherId",
                table: "Assignments",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Assignments_TeacherId",
                table: "Assignments",
                newName: "IX_Assignments_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Users_UserId",
                table: "Assignments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAssignments_Users_UserId",
                table: "StudentAssignments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Users_UserId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAssignments_Users_UserId",
                table: "StudentAssignments");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "StudentAssignments",
                newName: "StudentId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Assignments",
                newName: "TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_Assignments_UserId",
                table: "Assignments",
                newName: "IX_Assignments_TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Users_TeacherId",
                table: "Assignments",
                column: "TeacherId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAssignments_Users_StudentId",
                table: "StudentAssignments",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
