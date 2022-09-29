using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureManagement.Infrastructure.Migrations
{
    public partial class init28 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Items_ItemId",
                table: "Reports");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "Reports",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_ItemId",
                table: "Reports",
                newName: "IX_Reports_CategoryId");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Reports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Categorys_CategoryId",
                table: "Reports",
                column: "CategoryId",
                principalTable: "Categorys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Categorys_CategoryId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Reports");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Reports",
                newName: "ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_CategoryId",
                table: "Reports",
                newName: "IX_Reports_ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Items_ItemId",
                table: "Reports",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
