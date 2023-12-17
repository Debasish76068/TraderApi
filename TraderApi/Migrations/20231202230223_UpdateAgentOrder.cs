using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TraderApi.Migrations
{
    public partial class UpdateAgentOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AgentId",
                table: "AgentOrder",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeletedFromOrderWise",
                table: "AgentOrder",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ItemsId",
                table: "AgentOrder",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderDate",
                table: "AgentOrder",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgentId",
                table: "AgentOrder");

            migrationBuilder.DropColumn(
                name: "IsDeletedFromOrderWise",
                table: "AgentOrder");

            migrationBuilder.DropColumn(
                name: "ItemsId",
                table: "AgentOrder");

            migrationBuilder.DropColumn(
                name: "OrderDate",
                table: "AgentOrder");
        }
    }
}
