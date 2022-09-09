using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureManagement.Infrastructure.Migrations
{
    public partial class init14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Account",
                table: "Account");

            migrationBuilder.RenameTable(
                name: "Account",
                newName: "Accounts");

            migrationBuilder.RenameIndex(
                name: "IX_Account_Username",
                table: "Accounts",
                newName: "IX_Accounts_Username");

            migrationBuilder.RenameIndex(
                name: "IX_Account_Email",
                table: "Accounts",
                newName: "IX_Accounts_Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.RenameTable(
                name: "Accounts",
                newName: "Account");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_Username",
                table: "Account",
                newName: "IX_Account_Username");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_Email",
                table: "Account",
                newName: "IX_Account_Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Account",
                table: "Account",
                column: "Id");
        }
    }
}
