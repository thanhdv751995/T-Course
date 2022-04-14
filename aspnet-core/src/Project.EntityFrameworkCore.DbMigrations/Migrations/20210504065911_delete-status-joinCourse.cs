using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Migrations
{
    public partial class deletestatusjoinCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "ProjectJoinCourses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "ProjectJoinCourses",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
