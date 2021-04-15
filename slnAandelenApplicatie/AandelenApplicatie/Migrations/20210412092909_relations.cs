using Microsoft.EntityFrameworkCore.Migrations;

namespace AandelenApplicatie.Migrations
{
    public partial class relations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ownerships_Buyers_BuyerId",
                table: "Ownerships");

            migrationBuilder.DropForeignKey(
                name: "FK_Ownerships_Stocks_StockId",
                table: "Ownerships");

            migrationBuilder.DropForeignKey(
                name: "FK_Prices_Stocks_StockId",
                table: "Prices");

            migrationBuilder.DropForeignKey(
                name: "FK_StockLists_Companies_CompanyId",
                table: "StockLists");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_StockLists_StockListId",
                table: "Stocks");

            migrationBuilder.AddForeignKey(
                name: "FK_Ownerships_Buyers_BuyerId",
                table: "Ownerships",
                column: "BuyerId",
                principalTable: "Buyers",
                principalColumn: "BuyerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ownerships_Stocks_StockId",
                table: "Ownerships",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "StockId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_Stocks_StockId",
                table: "Prices",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "StockId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockLists_Companies_CompanyId",
                table: "StockLists",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_StockLists_StockListId",
                table: "Stocks",
                column: "StockListId",
                principalTable: "StockLists",
                principalColumn: "StockListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ownerships_Buyers_BuyerId",
                table: "Ownerships");

            migrationBuilder.DropForeignKey(
                name: "FK_Ownerships_Stocks_StockId",
                table: "Ownerships");

            migrationBuilder.DropForeignKey(
                name: "FK_Prices_Stocks_StockId",
                table: "Prices");

            migrationBuilder.DropForeignKey(
                name: "FK_StockLists_Companies_CompanyId",
                table: "StockLists");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_StockLists_StockListId",
                table: "Stocks");

            migrationBuilder.AddForeignKey(
                name: "FK_Ownerships_Buyers_BuyerId",
                table: "Ownerships",
                column: "BuyerId",
                principalTable: "Buyers",
                principalColumn: "BuyerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ownerships_Stocks_StockId",
                table: "Ownerships",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "StockId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_Stocks_StockId",
                table: "Prices",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "StockId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockLists_Companies_CompanyId",
                table: "StockLists",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_StockLists_StockListId",
                table: "Stocks",
                column: "StockListId",
                principalTable: "StockLists",
                principalColumn: "StockListId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
