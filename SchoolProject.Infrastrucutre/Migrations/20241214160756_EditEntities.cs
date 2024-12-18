using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentSubjects",
                table: "StudentSubjects");

            migrationBuilder.DropIndex(
                name: "IX_StudentSubjects_StudentId",
                table: "StudentSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DepartmentSubjects",
                table: "DepartmentSubjects");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentSubjects_DepartmentId",
                table: "DepartmentSubjects");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "StudentSubjects");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DepartmentSubjects");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Departments");

            migrationBuilder.AddColumn<string>(
                name: "NameAr",
                table: "Subjects",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameEn",
                table: "Subjects",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameAr",
                table: "Students",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameEn",
                table: "Students",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "InstructorManager",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NameAr",
                table: "Departments",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameEn",
                table: "Departments",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentSubjects",
                table: "StudentSubjects",
                columns: new[] { "StudentId", "SubjectId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_DepartmentSubjects",
                table: "DepartmentSubjects",
                columns: new[] { "DepartmentId", "SubjectId" });

            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupervisorId = table.Column<int>(type: "int", nullable: true),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instructors_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Instructors_Instructors_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "Instructors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InstructorSubjects",
                columns: table => new
                {
                    InstructorId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructorSubjects", x => new { x.InstructorId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_InstructorSubjects_Instructors_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Instructors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstructorSubjects_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_InstructorManager",
                table: "Departments",
                column: "InstructorManager",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_DepartmentId",
                table: "Instructors",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_SupervisorId",
                table: "Instructors",
                column: "SupervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorSubjects_SubjectId",
                table: "InstructorSubjects",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Instructors_InstructorManager",
                table: "Departments",
                column: "InstructorManager",
                principalTable: "Instructors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Instructors_InstructorManager",
                table: "Departments");

            migrationBuilder.DropTable(
                name: "InstructorSubjects");

            migrationBuilder.DropTable(
                name: "Instructors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentSubjects",
                table: "StudentSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DepartmentSubjects",
                table: "DepartmentSubjects");

            migrationBuilder.DropIndex(
                name: "IX_Departments_InstructorManager",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "NameAr",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "NameEn",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "NameAr",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "NameEn",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "InstructorManager",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "NameAr",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "NameEn",
                table: "Departments");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Subjects",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "StudentSubjects",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "DepartmentSubjects",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Departments",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentSubjects",
                table: "StudentSubjects",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DepartmentSubjects",
                table: "DepartmentSubjects",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubjects_StudentId",
                table: "StudentSubjects",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentSubjects_DepartmentId",
                table: "DepartmentSubjects",
                column: "DepartmentId");
        }
    }
}
