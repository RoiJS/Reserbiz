using Microsoft.EntityFrameworkCore.Migrations;

namespace ReserbizAPP.LIB.Migrations.ReserbizClientData
{
    public partial class SetupAccountAccountActionTrackerConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Accounts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeactivatedById",
                table: "Accounts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedById",
                table: "Accounts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedById",
                table: "Accounts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CreatedById",
                table: "Accounts",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_DeactivatedById",
                table: "Accounts",
                column: "DeactivatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_DeletedById",
                table: "Accounts",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UpdatedById",
                table: "Accounts",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_CreatedById_Accounts_AccountId",
                table: "Accounts",
                column: "CreatedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_DeactivatedById_Accounts_AccountId",
                table: "Accounts",
                column: "DeactivatedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_DeletedById_Accounts_AccountId",
                table: "Accounts",
                column: "DeletedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_UpdatedById_Accounts_AccountId",
                table: "Accounts",
                column: "UpdatedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_CreatedById_Accounts_AccountId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_DeactivatedById_Accounts_AccountId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_DeletedById_Accounts_AccountId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_UpdatedById_Accounts_AccountId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_CreatedById",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_DeactivatedById",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_DeletedById",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_UpdatedById",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "DeactivatedById",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Accounts");
        }
    }
}
