using Microsoft.EntityFrameworkCore.Migrations;

namespace ReserbizAPP.LIB.Migrations.ReserbizClientData
{
    public partial class AlteredColumnUserIdOnTableErrorLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ErrorLogs_Accounts_UserId",
                table: "ErrorLogs");

            migrationBuilder.DropIndex(
                name: "IX_ErrorLogs_UserId",
                table: "ErrorLogs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ErrorLogs");

            migrationBuilder.AddColumn<string>(
                name: "UserInfo",
                table: "ErrorLogs",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ErrorLogs_Accounts_AccountId",
                table: "ErrorLogs");

            migrationBuilder.DropIndex(
                name: "IX_ErrorLogs_AccountId",
                table: "ErrorLogs");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "ErrorLogs");

            migrationBuilder.DropColumn(
                name: "UserInfo",
                table: "ErrorLogs");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ErrorLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ErrorLogs_UserId",
                table: "ErrorLogs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ErrorLogs_Accounts_UserId",
                table: "ErrorLogs",
                column: "UserId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
