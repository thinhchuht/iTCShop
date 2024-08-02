using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iTCShop.Migrations
{
    /// <inheritdoc />
    public partial class Add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypes_CartDetails_CartDetailsID",
                table: "ProductTypes");

            migrationBuilder.DropIndex(
                name: "IX_ProductTypes_CartDetailsID",
                table: "ProductTypes");

            migrationBuilder.DropColumn(
                name: "CartDetailsID",
                table: "ProductTypes");

            migrationBuilder.AlterColumn<string>(
                name: "ProductTypeID",
                table: "CartDetails",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartDetails_ProductTypeID",
                table: "CartDetails",
                column: "ProductTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetails_ProductTypes_ProductTypeID",
                table: "CartDetails",
                column: "ProductTypeID",
                principalTable: "ProductTypes",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartDetails_ProductTypes_ProductTypeID",
                table: "CartDetails");

            migrationBuilder.DropIndex(
                name: "IX_CartDetails_ProductTypeID",
                table: "CartDetails");

            migrationBuilder.AddColumn<string>(
                name: "CartDetailsID",
                table: "ProductTypes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProductTypeID",
                table: "CartDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypes_CartDetailsID",
                table: "ProductTypes",
                column: "CartDetailsID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypes_CartDetails_CartDetailsID",
                table: "ProductTypes",
                column: "CartDetailsID",
                principalTable: "CartDetails",
                principalColumn: "ID");
        }
    }
}
