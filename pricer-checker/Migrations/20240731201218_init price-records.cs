using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pricer_checker.Migrations
{
    /// <inheritdoc />
    public partial class initpricerecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceRecords_Products_ProductId",
                table: "PriceRecords");

            migrationBuilder.DropIndex(
                name: "IX_PriceRecords_ProductId",
                table: "PriceRecords");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PriceRecords_ProductId",
                table: "PriceRecords",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_PriceRecords_Products_ProductId",
                table: "PriceRecords",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
