using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectFinSession.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCartAndAppUser89 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Carts_cartId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "cartId",
                table: "Products",
                newName: "CartId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_cartId",
                table: "Products",
                newName: "IX_Products_CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Carts_CartId",
                table: "Products",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Carts_CartId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "CartId",
                table: "Products",
                newName: "cartId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CartId",
                table: "Products",
                newName: "IX_Products_cartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Carts_cartId",
                table: "Products",
                column: "cartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
