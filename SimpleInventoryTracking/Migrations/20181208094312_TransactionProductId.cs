using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleInventoryTracking.Migrations
{
    public partial class TransactionProductId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Transactions",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Transactions");
        }
    }
}
