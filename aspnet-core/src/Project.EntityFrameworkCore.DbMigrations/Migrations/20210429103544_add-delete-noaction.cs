using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Migrations
{
    public partial class adddeletenoaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectComments_ProjectComments_IDParent",
                table: "ProjectComments");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectComments_ProjectComments_IDParent",
                table: "ProjectComments",
                column: "IDParent",
                principalTable: "ProjectComments",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectComments_ProjectComments_IDParent",
                table: "ProjectComments");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectComments_ProjectComments_IDParent",
                table: "ProjectComments",
                column: "IDParent",
                principalTable: "ProjectComments",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
