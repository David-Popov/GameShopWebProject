using Microsoft.EntityFrameworkCore.Migrations;

namespace CarShopWebProject.Migrations
{
    public partial class SecondProductTableUpgrade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Category_CategoryId1",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Platform_PlatformId1",
                table: "Games");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Games",
                table: "Games");

            migrationBuilder.RenameTable(
                name: "Games",
                newName: "Product");

            migrationBuilder.RenameIndex(
                name: "IX_Games_PlatformId1",
                table: "Product",
                newName: "IX_Product_PlatformId1");

            migrationBuilder.RenameIndex(
                name: "IX_Games_CategoryId1",
                table: "Product",
                newName: "IX_Product_CategoryId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_CategoryId1",
                table: "Product",
                column: "CategoryId1",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Platform_PlatformId1",
                table: "Product",
                column: "PlatformId1",
                principalTable: "Platform",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_CategoryId1",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Platform_PlatformId1",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Games");

            migrationBuilder.RenameIndex(
                name: "IX_Product_PlatformId1",
                table: "Games",
                newName: "IX_Games_PlatformId1");

            migrationBuilder.RenameIndex(
                name: "IX_Product_CategoryId1",
                table: "Games",
                newName: "IX_Games_CategoryId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Games",
                table: "Games",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Category_CategoryId1",
                table: "Games",
                column: "CategoryId1",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Platform_PlatformId1",
                table: "Games",
                column: "PlatformId1",
                principalTable: "Platform",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
