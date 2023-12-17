using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TraderApi.Migrations
{
    public partial class UpdateSaleBillAgentOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderNo",
                table: "SalesBill",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "RemainingAmount",
                table: "SalesBill",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Purchaser",
                table: "AgentOrder",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PurchaserId",
                table: "AgentOrder",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderNo",
                table: "SalesBill");

            migrationBuilder.DropColumn(
                name: "RemainingAmount",
                table: "SalesBill");

            migrationBuilder.DropColumn(
                name: "Purchaser",
                table: "AgentOrder");

            migrationBuilder.DropColumn(
                name: "PurchaserId",
                table: "AgentOrder");
        }
    }
}
