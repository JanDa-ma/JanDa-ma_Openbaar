using Microsoft.EntityFrameworkCore.Migrations;

namespace AandelenApplicatie.Migrations
{
    public partial class metadata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "CompanyId", "CompanyName" },
                values: new object[] { 1, "KBC" });

            migrationBuilder.InsertData(
                table: "StockLists",
                columns: new[] { "StockListId", "CompanyId", "IsDeleted", "Name" },
                values: new object[] { 1, 1, false, "KBC-unam" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StockLists",
                keyColumn: "StockListId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "CompanyId",
                keyValue: 1);
        }
    }
}
