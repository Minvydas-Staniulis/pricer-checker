using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pricer_checker.Migrations
{
    /// <inheritdoc />
    public partial class productprice2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceRecord_Products_ProductId",
                table: "PriceRecord");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PriceRecord",
                table: "PriceRecord");

            migrationBuilder.RenameTable(
                name: "PriceRecord",
                newName: "PriceRecords");

            migrationBuilder.RenameIndex(
                name: "IX_PriceRecord_ProductId",
                table: "PriceRecords",
                newName: "IX_PriceRecords_ProductId");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "PriceRecords",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PriceRecords",
                table: "PriceRecords",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PriceRecords_Products_ProductId",
                table: "PriceRecords",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceRecords_Products_ProductId",
                table: "PriceRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PriceRecords",
                table: "PriceRecords");

            migrationBuilder.RenameTable(
                name: "PriceRecords",
                newName: "PriceRecord");

            migrationBuilder.RenameIndex(
                name: "IX_PriceRecords_ProductId",
                table: "PriceRecord",
                newName: "IX_PriceRecord_ProductId");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "PriceRecord",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PriceRecord",
                table: "PriceRecord",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PriceRecord_Products_ProductId",
                table: "PriceRecord",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
