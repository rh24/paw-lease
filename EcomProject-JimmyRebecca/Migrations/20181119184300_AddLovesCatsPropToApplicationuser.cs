using Microsoft.EntityFrameworkCore.Migrations;

namespace EcomProject_JimmyRebecca.Migrations
{
    public partial class AddLovesCatsPropToApplicationuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "LovesCats",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LovesCats",
                table: "AspNetUsers");
        }
    }
}
