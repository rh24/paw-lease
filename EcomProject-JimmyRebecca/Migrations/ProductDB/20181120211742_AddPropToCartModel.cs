using Microsoft.EntityFrameworkCore.Migrations;

namespace EcomProject_JimmyRebecca.Migrations.ProductDB
{
    public partial class AddPropToCartModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "OrderFulfilled",
                table: "Carts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderFulfilled",
                table: "Carts");
        }
    }
}
