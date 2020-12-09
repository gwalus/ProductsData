using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoesApp.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Brand = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ProductCode = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    Size = table.Column<string>(nullable: true),
                    Box = table.Column<bool>(nullable: true),
                    Source = table.Column<string>(nullable: true),
                    DateOfPurchase = table.Column<string>(nullable: true),
                    SaleDate = table.Column<string>(nullable: true),
                    PurchasePrice = table.Column<double>(nullable: false),
                    SellingPrice = table.Column<double>(nullable: true),
                    ShippingPrice = table.Column<double>(nullable: true),
                    PriceWithoutShipping = table.Column<double>(nullable: true),
                    Profit = table.Column<double>(nullable: true),
                    IsSold = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
