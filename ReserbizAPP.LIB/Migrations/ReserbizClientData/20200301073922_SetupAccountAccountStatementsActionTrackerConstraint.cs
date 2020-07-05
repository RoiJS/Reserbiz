using Microsoft.EntityFrameworkCore.Migrations;

namespace ReserbizAPP.LIB.Migrations.ReserbizClientData
{
    public partial class SetupAccountAccountStatementsActionTrackerConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "AccountStatements",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeactivatedById",
                table: "AccountStatements",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedById",
                table: "AccountStatements",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedById",
                table: "AccountStatements",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountStatements_CreatedById",
                table: "AccountStatements",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AccountStatements_DeactivatedById",
                table: "AccountStatements",
                column: "DeactivatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AccountStatements_DeletedById",
                table: "AccountStatements",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_AccountStatements_UpdatedById",
                table: "AccountStatements",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountStatements_CreatedById_Accounts_AccountId",
                table: "AccountStatements",
                column: "CreatedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountStatements_DeactivatedById_Accounts_AccountId",
                table: "AccountStatements",
                column: "DeactivatedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountStatements_DeletedById_Accounts_AccountId",
                table: "AccountStatements",
                column: "DeletedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountStatements_UpdatedById_Accounts_AccountId",
                table: "AccountStatements",
                column: "UpdatedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountStatements_CreatedById_Accounts_AccountId",
                table: "AccountStatements");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountStatements_DeactivatedById_Accounts_AccountId",
                table: "AccountStatements");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountStatements_DeletedById_Accounts_AccountId",
                table: "AccountStatements");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountStatements_UpdatedById_Accounts_AccountId",
                table: "AccountStatements");

            migrationBuilder.DropIndex(
                name: "IX_AccountStatements_CreatedById",
                table: "AccountStatements");

            migrationBuilder.DropIndex(
                name: "IX_AccountStatements_DeactivatedById",
                table: "AccountStatements");

            migrationBuilder.DropIndex(
                name: "IX_AccountStatements_DeletedById",
                table: "AccountStatements");

            migrationBuilder.DropIndex(
                name: "IX_AccountStatements_UpdatedById",
                table: "AccountStatements");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "AccountStatements");

            migrationBuilder.DropColumn(
                name: "DeactivatedById",
                table: "AccountStatements");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "AccountStatements");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "AccountStatements");
        }
    }
}
