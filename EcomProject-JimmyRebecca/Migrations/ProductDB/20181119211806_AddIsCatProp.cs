using Microsoft.EntityFrameworkCore.Migrations;

namespace EcomProject_JimmyRebecca.Migrations.ProductDB
{
    public partial class AddIsCatProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCat",
                table: "Products",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCat",
                table: "Products");
        }
    }
}
