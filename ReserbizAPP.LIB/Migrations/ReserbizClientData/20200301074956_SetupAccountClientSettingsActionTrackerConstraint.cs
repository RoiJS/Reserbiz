using Microsoft.EntityFrameworkCore.Migrations;

namespace ReserbizAPP.LIB.Migrations.ReserbizClientData
{
    public partial class SetupAccountClientSettingsActionTrackerConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "ClientSettings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeactivatedById",
                table: "ClientSettings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedById",
                table: "ClientSettings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedById",
                table: "ClientSettings",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientSettings_CreatedById",
                table: "ClientSettings",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ClientSettings_DeactivatedById",
                table: "ClientSettings",
                column: "DeactivatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ClientSettings_DeletedById",
                table: "ClientSettings",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_ClientSettings_UpdatedById",
                table: "ClientSettings",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientSettings_CreatedById_Accounts_AccountId",
                table: "ClientSettings",
                column: "CreatedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientSettings_DeactivatedById_Accounts_AccountId",
                table: "ClientSettings",
                column: "DeactivatedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientSettings_DeletedById_Accounts_AccountId",
                table: "ClientSettings",
                column: "DeletedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientSettings_UpdatedById_Accounts_AccountId",
                table: "ClientSettings",
                column: "UpdatedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientSettings_CreatedById_Accounts_AccountId",
                table: "ClientSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientSettings_DeactivatedById_Accounts_AccountId",
                table: "ClientSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientSettings_DeletedById_Accounts_AccountId",
                table: "ClientSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientSettings_UpdatedById_Accounts_AccountId",
                table: "ClientSettings");

            migrationBuilder.DropIndex(
                name: "IX_ClientSettings_CreatedById",
                table: "ClientSettings");

            migrationBuilder.DropIndex(
                name: "IX_ClientSettings_DeactivatedById",
                table: "ClientSettings");

            migrationBuilder.DropIndex(
                name: "IX_ClientSettings_DeletedById",
                table: "ClientSettings");

            migrationBuilder.DropIndex(
                name: "IX_ClientSettings_UpdatedById",
                table: "ClientSettings");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "ClientSettings");

            migrationBuilder.DropColumn(
                name: "DeactivatedById",
                table: "ClientSettings");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "ClientSettings");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "ClientSettings");
        }
    }
}
