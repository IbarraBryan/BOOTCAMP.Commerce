using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Catalog.Persistence.Database.Migrations
{
    /// <inheritdoc />
    public partial class CatalogInitialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Catalog");

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "Catalog",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "ProductInStocks",
                schema: "Catalog",
                columns: table => new
                {
                    ProductInStockId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInStocks", x => x.ProductInStockId);
                    table.ForeignKey(
                        name: "FK_ProductInStocks_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Catalog",
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Catalog",
                table: "Products",
                columns: new[] { "ProductId", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Description for product 1", "Product 1", 847m },
                    { 2, "Description for product 2", "Product 2", 450m },
                    { 3, "Description for product 3", "Product 3", 213m },
                    { 4, "Description for product 4", "Product 4", 962m },
                    { 5, "Description for product 5", "Product 5", 425m },
                    { 6, "Description for product 6", "Product 6", 779m },
                    { 7, "Description for product 7", "Product 7", 131m },
                    { 8, "Description for product 8", "Product 8", 958m },
                    { 9, "Description for product 9", "Product 9", 304m },
                    { 10, "Description for product 10", "Product 10", 543m },
                    { 11, "Description for product 11", "Product 11", 839m },
                    { 12, "Description for product 12", "Product 12", 204m },
                    { 13, "Description for product 13", "Product 13", 492m },
                    { 14, "Description for product 14", "Product 14", 717m },
                    { 15, "Description for product 15", "Product 15", 475m },
                    { 16, "Description for product 16", "Product 16", 241m },
                    { 17, "Description for product 17", "Product 17", 154m },
                    { 18, "Description for product 18", "Product 18", 194m },
                    { 19, "Description for product 19", "Product 19", 802m },
                    { 20, "Description for product 20", "Product 20", 467m },
                    { 21, "Description for product 21", "Product 21", 201m },
                    { 22, "Description for product 22", "Product 22", 525m },
                    { 23, "Description for product 23", "Product 23", 482m },
                    { 24, "Description for product 24", "Product 24", 938m },
                    { 25, "Description for product 25", "Product 25", 139m },
                    { 26, "Description for product 26", "Product 26", 503m },
                    { 27, "Description for product 27", "Product 27", 246m },
                    { 28, "Description for product 28", "Product 28", 996m },
                    { 29, "Description for product 29", "Product 29", 345m },
                    { 30, "Description for product 30", "Product 30", 502m },
                    { 31, "Description for product 31", "Product 31", 527m },
                    { 32, "Description for product 32", "Product 32", 512m },
                    { 33, "Description for product 33", "Product 33", 906m },
                    { 34, "Description for product 34", "Product 34", 950m },
                    { 35, "Description for product 35", "Product 35", 819m },
                    { 36, "Description for product 36", "Product 36", 819m },
                    { 37, "Description for product 37", "Product 37", 160m },
                    { 38, "Description for product 38", "Product 38", 521m },
                    { 39, "Description for product 39", "Product 39", 769m },
                    { 40, "Description for product 40", "Product 40", 636m },
                    { 41, "Description for product 41", "Product 41", 423m },
                    { 42, "Description for product 42", "Product 42", 859m },
                    { 43, "Description for product 43", "Product 43", 119m },
                    { 44, "Description for product 44", "Product 44", 723m },
                    { 45, "Description for product 45", "Product 45", 408m },
                    { 46, "Description for product 46", "Product 46", 778m },
                    { 47, "Description for product 47", "Product 47", 747m },
                    { 48, "Description for product 48", "Product 48", 986m },
                    { 49, "Description for product 49", "Product 49", 976m },
                    { 50, "Description for product 50", "Product 50", 438m },
                    { 51, "Description for product 51", "Product 51", 899m },
                    { 52, "Description for product 52", "Product 52", 799m },
                    { 53, "Description for product 53", "Product 53", 785m },
                    { 54, "Description for product 54", "Product 54", 332m },
                    { 55, "Description for product 55", "Product 55", 717m },
                    { 56, "Description for product 56", "Product 56", 574m },
                    { 57, "Description for product 57", "Product 57", 733m },
                    { 58, "Description for product 58", "Product 58", 582m },
                    { 59, "Description for product 59", "Product 59", 672m },
                    { 60, "Description for product 60", "Product 60", 354m },
                    { 61, "Description for product 61", "Product 61", 347m },
                    { 62, "Description for product 62", "Product 62", 959m },
                    { 63, "Description for product 63", "Product 63", 483m },
                    { 64, "Description for product 64", "Product 64", 746m },
                    { 65, "Description for product 65", "Product 65", 339m },
                    { 66, "Description for product 66", "Product 66", 438m },
                    { 67, "Description for product 67", "Product 67", 328m },
                    { 68, "Description for product 68", "Product 68", 512m },
                    { 69, "Description for product 69", "Product 69", 528m },
                    { 70, "Description for product 70", "Product 70", 568m },
                    { 71, "Description for product 71", "Product 71", 673m },
                    { 72, "Description for product 72", "Product 72", 585m },
                    { 73, "Description for product 73", "Product 73", 535m },
                    { 74, "Description for product 74", "Product 74", 487m },
                    { 75, "Description for product 75", "Product 75", 429m },
                    { 76, "Description for product 76", "Product 76", 459m },
                    { 77, "Description for product 77", "Product 77", 988m },
                    { 78, "Description for product 78", "Product 78", 280m },
                    { 79, "Description for product 79", "Product 79", 472m },
                    { 80, "Description for product 80", "Product 80", 413m },
                    { 81, "Description for product 81", "Product 81", 667m },
                    { 82, "Description for product 82", "Product 82", 771m },
                    { 83, "Description for product 83", "Product 83", 736m },
                    { 84, "Description for product 84", "Product 84", 155m },
                    { 85, "Description for product 85", "Product 85", 551m },
                    { 86, "Description for product 86", "Product 86", 438m },
                    { 87, "Description for product 87", "Product 87", 235m },
                    { 88, "Description for product 88", "Product 88", 374m },
                    { 89, "Description for product 89", "Product 89", 456m },
                    { 90, "Description for product 90", "Product 90", 876m },
                    { 91, "Description for product 91", "Product 91", 896m },
                    { 92, "Description for product 92", "Product 92", 704m },
                    { 93, "Description for product 93", "Product 93", 884m },
                    { 94, "Description for product 94", "Product 94", 740m },
                    { 95, "Description for product 95", "Product 95", 726m },
                    { 96, "Description for product 96", "Product 96", 373m },
                    { 97, "Description for product 97", "Product 97", 787m },
                    { 98, "Description for product 98", "Product 98", 846m },
                    { 99, "Description for product 99", "Product 99", 768m },
                    { 100, "Description for product 100", "Product 100", 685m }
                });

            migrationBuilder.InsertData(
                schema: "Catalog",
                table: "ProductInStocks",
                columns: new[] { "ProductInStockId", "ProductId", "Stock" },
                values: new object[,]
                {
                    { 1, 1, 22 },
                    { 2, 2, 25 },
                    { 3, 3, 20 },
                    { 4, 4, 12 },
                    { 5, 5, 8 },
                    { 6, 6, 19 },
                    { 7, 7, 29 },
                    { 8, 8, 6 },
                    { 9, 9, 34 },
                    { 10, 10, 35 },
                    { 11, 11, 29 },
                    { 12, 12, 17 },
                    { 13, 13, 34 },
                    { 14, 14, 12 },
                    { 15, 15, 1 },
                    { 16, 16, 32 },
                    { 17, 17, 13 },
                    { 18, 18, 19 },
                    { 19, 19, 37 },
                    { 20, 20, 3 },
                    { 21, 21, 22 },
                    { 22, 22, 9 },
                    { 23, 23, 12 },
                    { 24, 24, 0 },
                    { 25, 25, 37 },
                    { 26, 26, 34 },
                    { 27, 27, 31 },
                    { 28, 28, 11 },
                    { 29, 29, 31 },
                    { 30, 30, 29 },
                    { 31, 31, 29 },
                    { 32, 32, 3 },
                    { 33, 33, 1 },
                    { 34, 34, 26 },
                    { 35, 35, 31 },
                    { 36, 36, 37 },
                    { 37, 37, 32 },
                    { 38, 38, 6 },
                    { 39, 39, 37 },
                    { 40, 40, 23 },
                    { 41, 41, 38 },
                    { 42, 42, 18 },
                    { 43, 43, 28 },
                    { 44, 44, 35 },
                    { 45, 45, 32 },
                    { 46, 46, 2 },
                    { 47, 47, 11 },
                    { 48, 48, 0 },
                    { 49, 49, 38 },
                    { 50, 50, 9 },
                    { 51, 51, 12 },
                    { 52, 52, 36 },
                    { 53, 53, 38 },
                    { 54, 54, 36 },
                    { 55, 55, 22 },
                    { 56, 56, 39 },
                    { 57, 57, 12 },
                    { 58, 58, 10 },
                    { 59, 59, 9 },
                    { 60, 60, 6 },
                    { 61, 61, 8 },
                    { 62, 62, 31 },
                    { 63, 63, 4 },
                    { 64, 64, 0 },
                    { 65, 65, 16 },
                    { 66, 66, 6 },
                    { 67, 67, 10 },
                    { 68, 68, 32 },
                    { 69, 69, 36 },
                    { 70, 70, 27 },
                    { 71, 71, 39 },
                    { 72, 72, 15 },
                    { 73, 73, 21 },
                    { 74, 74, 2 },
                    { 75, 75, 7 },
                    { 76, 76, 0 },
                    { 77, 77, 29 },
                    { 78, 78, 25 },
                    { 79, 79, 36 },
                    { 80, 80, 33 },
                    { 81, 81, 13 },
                    { 82, 82, 23 },
                    { 83, 83, 16 },
                    { 84, 84, 36 },
                    { 85, 85, 9 },
                    { 86, 86, 31 },
                    { 87, 87, 39 },
                    { 88, 88, 28 },
                    { 89, 89, 14 },
                    { 90, 90, 37 },
                    { 91, 91, 26 },
                    { 92, 92, 16 },
                    { 93, 93, 34 },
                    { 94, 94, 17 },
                    { 95, 95, 8 },
                    { 96, 96, 29 },
                    { 97, 97, 16 },
                    { 98, 98, 31 },
                    { 99, 99, 36 },
                    { 100, 100, 29 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductInStocks_ProductId",
                schema: "Catalog",
                table: "ProductInStocks",
                column: "ProductId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductInStocks",
                schema: "Catalog");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "Catalog");
        }
    }
}
