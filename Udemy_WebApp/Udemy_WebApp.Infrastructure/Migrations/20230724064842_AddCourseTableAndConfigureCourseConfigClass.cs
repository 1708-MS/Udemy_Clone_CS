using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Udemy_WebApp.Infrastructure.Migrations
{
    public partial class AddCourseTableAndConfigureCourseConfigClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseListPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CourseCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CourseDurationTime = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
