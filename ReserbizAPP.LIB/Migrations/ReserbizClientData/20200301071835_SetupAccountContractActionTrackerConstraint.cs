using Microsoft.EntityFrameworkCore.Migrations;

namespace ReserbizAPP.LIB.Migrations.ReserbizClientData
{
    public partial class SetupAccountContractActionTrackerConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Contracts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeactivatedById",
                table: "Contracts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedById",
                table: "Contracts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedById",
                table: "Contracts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_CreatedById",
                table: "Contracts",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_DeactivatedById",
                table: "Contracts",
                column: "DeactivatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_DeletedById",
                table: "Contracts",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_UpdatedById",
                table: "Contracts",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_CreatedById_Accounts_AccountId",
                table: "Contracts",
                column: "CreatedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_DeactivatedById_Accounts_AccountId",
                table: "Contracts",
                column: "DeactivatedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_DeletedById_Accounts_AccountId",
                table: "Contracts",
                column: "DeletedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_UpdatedById_Accounts_AccountId",
                table: "Contracts",
                column: "UpdatedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_CreatedById_Accounts_AccountId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_DeactivatedById_Accounts_AccountId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_DeletedById_Accounts_AccountId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_UpdatedById_Accounts_AccountId",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_CreatedById",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_DeactivatedById",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_DeletedById",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_UpdatedById",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "DeactivatedById",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Contracts");
        }
    }
}
