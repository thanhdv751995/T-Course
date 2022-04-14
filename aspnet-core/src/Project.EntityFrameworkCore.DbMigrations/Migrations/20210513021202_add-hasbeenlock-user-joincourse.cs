using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Migrations
{
    public partial class addhasbeenlockuserjoincourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasBeenLock",
                table: "ProjectJoinCourses",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasBeenLock",
                table: "ProjectJoinCourses");
        }
    }
}
