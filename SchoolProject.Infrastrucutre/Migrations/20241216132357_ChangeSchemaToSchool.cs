using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSchemaToSchool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "school");

            migrationBuilder.RenameTable(
                name: "Subjects",
                newName: "Subjects",
                newSchema: "school");

            migrationBuilder.RenameTable(
                name: "StudentSubjects",
                newName: "StudentSubjects",
                newSchema: "school");

            migrationBuilder.RenameTable(
                name: "Students",
                newName: "Students",
                newSchema: "school");

            migrationBuilder.RenameTable(
                name: "InstructorSubjects",
                newName: "InstructorSubjects",
                newSchema: "school");

            migrationBuilder.RenameTable(
                name: "Instructors",
                newName: "Instructors",
                newSchema: "school");

            migrationBuilder.RenameTable(
                name: "DepartmentSubjects",
                newName: "DepartmentSubjects",
                newSchema: "school");

            migrationBuilder.RenameTable(
                name: "Departments",
                newName: "Departments",
                newSchema: "school");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Subjects",
                schema: "school",
                newName: "Subjects");

            migrationBuilder.RenameTable(
                name: "StudentSubjects",
                schema: "school",
                newName: "StudentSubjects");

            migrationBuilder.RenameTable(
                name: "Students",
                schema: "school",
                newName: "Students");

            migrationBuilder.RenameTable(
                name: "InstructorSubjects",
                schema: "school",
                newName: "InstructorSubjects");

            migrationBuilder.RenameTable(
                name: "Instructors",
                schema: "school",
                newName: "Instructors");

            migrationBuilder.RenameTable(
                name: "DepartmentSubjects",
                schema: "school",
                newName: "DepartmentSubjects");

            migrationBuilder.RenameTable(
                name: "Departments",
                schema: "school",
                newName: "Departments");
        }
    }
}
