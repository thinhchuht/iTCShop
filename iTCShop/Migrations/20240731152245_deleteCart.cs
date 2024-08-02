using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iTCShop.Migrations
{
    /// <inheritdoc />
    public partial class deleteCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartDetails_Carts_CartID",
                table: "CartDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Carts_CartId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_CartId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "CartID",
                table: "CartDetails",
                newName: "CartId");

            migrationBuilder.RenameIndex(
                name: "IX_CartDetails_CartID",
                table: "CartDetails",
                newName: "IX_CartDetails_CartId");

            migrationBuilder.AddColumn<string>(
                name: "CartDetailId",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerID",
                table: "CartDetails",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartDetails_CustomerID",
                table: "CartDetails",
                column: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetails_Carts_CartId",
                table: "CartDetails",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetails_Customers_CustomerID",
                table: "CartDetails",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartDetails_Carts_CartId",
                table: "CartDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_CartDetails_Customers_CustomerID",
                table: "CartDetails");

            migrationBuilder.DropIndex(
                name: "IX_CartDetails_CustomerID",
                table: "CartDetails");

            migrationBuilder.DropColumn(
                name: "CartDetailId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CustomerID",
                table: "CartDetails");

            migrationBuilder.RenameColumn(
                name: "CartId",
                table: "CartDetails",
                newName: "CartID");

            migrationBuilder.RenameIndex(
                name: "IX_CartDetails_CartId",
                table: "CartDetails",
                newName: "IX_CartDetails_CartID");

            migrationBuilder.AddColumn<string>(
                name: "CartId",
                table: "Customers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CartId",
                table: "Customers",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetails_Carts_CartID",
                table: "CartDetails",
                column: "CartID",
                principalTable: "Carts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Carts_CartId",
                table: "Customers",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id");
        }
    }
}
