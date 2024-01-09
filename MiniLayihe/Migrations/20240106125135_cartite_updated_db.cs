using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniLayihe.Migrations
{
    /// <inheritdoc />
    public partial class cartite_updated_db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_ProductImages_ImageId",
                table: "CartItems");

            migrationBuilder.DropTable(
                name: "CartProduct");

            migrationBuilder.DropTable(
                name: "CartProductImage");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_ImageId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "CartItems");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "Carts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ProductImageId",
                table: "Carts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ProductId",
                table: "Carts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ProductImageId",
                table: "Carts",
                column: "ProductImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_ProductImages_ProductImageId",
                table: "Carts",
                column: "ProductImageId",
                principalTable: "ProductImages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Products_ProductId",
                table: "Carts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_ProductImages_ProductImageId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Products_ProductId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_ProductId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_ProductImageId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "ProductImageId",
                table: "Carts");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Carts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "Carts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "CartItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CartProduct",
                columns: table => new
                {
                    CartsId = table.Column<int>(type: "int", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartProduct", x => new { x.CartsId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_CartProduct_Carts_CartsId",
                        column: x => x.CartsId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartProductImage",
                columns: table => new
                {
                    CartsId = table.Column<int>(type: "int", nullable: false),
                    ImageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartProductImage", x => new { x.CartsId, x.ImageId });
                    table.ForeignKey(
                        name: "FK_CartProductImage_Carts_CartsId",
                        column: x => x.CartsId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartProductImage_ProductImages_ImageId",
                        column: x => x.ImageId,
                        principalTable: "ProductImages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ImageId",
                table: "CartItems",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_CartProduct_ProductsId",
                table: "CartProduct",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_CartProductImage_ImageId",
                table: "CartProductImage",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_ProductImages_ImageId",
                table: "CartItems",
                column: "ImageId",
                principalTable: "ProductImages",
                principalColumn: "Id");
        }
    }
}
