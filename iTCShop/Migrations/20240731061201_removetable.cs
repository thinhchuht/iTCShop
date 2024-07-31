using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iTCShop.Migrations
{
    /// <inheritdoc />
    public partial class removetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "StockIns");

            migrationBuilder.DropTable(
                name: "StockOuts");

            migrationBuilder.DropTable(
                name: "Suppliers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductIMEI = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SupplierID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Inventories_Products_ProductIMEI",
                        column: x => x.ProductIMEI,
                        principalTable: "Products",
                        principalColumn: "IMEI");
                    table.ForeignKey(
                        name: "FK_Inventories_Suppliers_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Suppliers",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "StockIns",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SupplierID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PriceIn = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TransInDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockIns", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StockIns_ProductTypes_ProductID",
                        column: x => x.ProductID,
                        principalTable: "ProductTypes",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_StockIns_Suppliers_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Suppliers",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "StockOuts",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SupplierID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PriceOut = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TransOutDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockOuts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StockOuts_ProductTypes_ProductID",
                        column: x => x.ProductID,
                        principalTable: "ProductTypes",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_StockOuts_Suppliers_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Suppliers",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_ProductIMEI",
                table: "Inventories",
                column: "ProductIMEI");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_SupplierID",
                table: "Inventories",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_StockIns_ProductID",
                table: "StockIns",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_StockIns_SupplierID",
                table: "StockIns",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_StockOuts_ProductID",
                table: "StockOuts",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_StockOuts_SupplierID",
                table: "StockOuts",
                column: "SupplierID");
        }
    }
}
