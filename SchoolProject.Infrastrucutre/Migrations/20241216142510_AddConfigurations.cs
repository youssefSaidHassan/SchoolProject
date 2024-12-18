using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_InstructorSubjects",
                schema: "school",
                table: "InstructorSubjects");

            migrationBuilder.DropIndex(
                name: "IX_InstructorSubjects_SubjectId",
                schema: "school",
                table: "InstructorSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DepartmentSubjects",
                schema: "school",
                table: "DepartmentSubjects");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentSubjects_SubjectId",
                schema: "school",
                table: "DepartmentSubjects");

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                schema: "school",
                table: "Departments",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                schema: "school",
                table: "Departments",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_InstructorSubjects",
                schema: "school",
                table: "InstructorSubjects",
                columns: new[] { "SubjectId", "InstructorId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_DepartmentSubjects",
                schema: "school",
                table: "DepartmentSubjects",
                columns: new[] { "SubjectId", "DepartmentId" });

            migrationBuilder.CreateIndex(
                name: "IX_InstructorSubjects_InstructorId",
                schema: "school",
                table: "InstructorSubjects",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentSubjects_DepartmentId",
                schema: "school",
                table: "DepartmentSubjects",
                column: "DepartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_InstructorSubjects",
                schema: "school",
                table: "InstructorSubjects");

            migrationBuilder.DropIndex(
                name: "IX_InstructorSubjects_InstructorId",
                schema: "school",
                table: "InstructorSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DepartmentSubjects",
                schema: "school",
                table: "DepartmentSubjects");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentSubjects_DepartmentId",
                schema: "school",
                table: "DepartmentSubjects");

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                schema: "school",
                table: "Departments",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                schema: "school",
                table: "Departments",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_InstructorSubjects",
                schema: "school",
                table: "InstructorSubjects",
                columns: new[] { "InstructorId", "SubjectId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_DepartmentSubjects",
                schema: "school",
                table: "DepartmentSubjects",
                columns: new[] { "DepartmentId", "SubjectId" });

            migrationBuilder.CreateIndex(
                name: "IX_InstructorSubjects_SubjectId",
                schema: "school",
                table: "InstructorSubjects",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentSubjects_SubjectId",
                schema: "school",
                table: "DepartmentSubjects",
                column: "SubjectId");
        }
    }
}
