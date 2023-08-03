using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Udemy_WebApp.Infrastructure.Migrations
{
    public partial class AddCourseCategoryTableAndConfigureCourseCategoryConfigClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseCategoryId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CourseCategories",
                columns: table => new
                {
                    CourseCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseCategories", x => x.CourseCategoryId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseCategoryId",
                table: "Courses",
                column: "CourseCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseCategories_CourseCategoryId",
                table: "Courses",
                column: "CourseCategoryId",
                principalTable: "CourseCategories",
                principalColumn: "CourseCategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseCategories_CourseCategoryId",
                table: "Courses");

            migrationBuilder.DropTable(
                name: "CourseCategories");

            migrationBuilder.DropIndex(
                name: "IX_Courses_CourseCategoryId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CourseCategoryId",
                table: "Courses");
        }
    }
}
