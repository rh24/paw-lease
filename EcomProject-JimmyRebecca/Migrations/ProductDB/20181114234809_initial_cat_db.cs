using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EcomProject_JimmyRebecca.Migrations.ProductDB
{
    public partial class initial_cat_db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductName = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    SuggestedDonation = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ID", "Description", "ProductName", "SuggestedDonation" },
                values: new object[,]
                {
                    { 1, "It's garfield, who doesn't want garfield???", "Tabby", 900m },
                    { 2, "Endless fun! For your cat too, I guess...", "Laser Pointer", 30m },
                    { 3, "When you just don't wanna deal with them anymore", "Catnip", 35m },
                    { 4, "Scottish folds are normal cats that fold their ears.", "Scottish Fold", 1100m },
                    { 5, "They might not like it, but it'll keep them warm", "Cat-shirt", 40m },
                    { 6, "This is where lion king started", "Cat Post", 75m },
                    { 7, "It's like a bengal, but less dangerous.", "Bengal", 900m },
                    { 8, "Buy a cat a home, they'll leave you forever.", "Cat-stle", 120m },
                    { 9, "When you want mindless zombies...", "Cat Food", 50m },
                    { 10, "If you enjoy vodka, this is your friend.", "Russian Blue", 900m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
