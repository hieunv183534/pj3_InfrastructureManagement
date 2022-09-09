using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureManagement.Infrastructure.Migrations
{
    public partial class init15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PositionString",
                table: "Reports",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CategoryCode",
                table: "Items",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Items",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ItemId",
                table: "Reports",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_PositionId",
                table: "Reports",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_CategoryId",
                table: "Items",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemLogs_ItemId",
                table: "ItemLogs",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemLogs_Items_ItemId",
                table: "ItemLogs",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Categories_CategoryId",
                table: "Items",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Items_ItemId",
                table: "Reports",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Items_PositionId",
                table: "Reports",
                column: "PositionId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemLogs_Items_ItemId",
                table: "ItemLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Categories_CategoryId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Items_ItemId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Items_PositionId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_ItemId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_PositionId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Items_CategoryId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_ItemLogs_ItemId",
                table: "ItemLogs");

            migrationBuilder.DropColumn(
                name: "PositionString",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "CategoryCode",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Items");
        }
    }
}
