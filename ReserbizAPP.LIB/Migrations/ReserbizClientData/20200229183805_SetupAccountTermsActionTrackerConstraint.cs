using Microsoft.EntityFrameworkCore.Migrations;

namespace ReserbizAPP.LIB.Migrations.ReserbizClientData
{
    public partial class SetupAccountTermsActionTrackerConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Terms",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeactivatedById",
                table: "Terms",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedById",
                table: "Terms",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedById",
                table: "Terms",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Terms_CreatedById",
                table: "Terms",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Terms_DeactivatedById",
                table: "Terms",
                column: "DeactivatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Terms_DeletedById",
                table: "Terms",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Terms_UpdatedById",
                table: "Terms",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Terms_CreatedById_Accounts_AccountId",
                table: "Terms",
                column: "CreatedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Terms_DeactivatedById_Accounts_AccountId",
                table: "Terms",
                column: "DeactivatedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Terms_DeletedById_Accounts_AccountId",
                table: "Terms",
                column: "DeletedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Terms_UpdatedById_Accounts_AccountId",
                table: "Terms",
                column: "UpdatedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Terms_CreatedById_Accounts_AccountId",
                table: "Terms");

            migrationBuilder.DropForeignKey(
                name: "FK_Terms_DeactivatedById_Accounts_AccountId",
                table: "Terms");

            migrationBuilder.DropForeignKey(
                name: "FK_Terms_DeletedById_Accounts_AccountId",
                table: "Terms");

            migrationBuilder.DropForeignKey(
                name: "FK_Terms_UpdatedById_Accounts_AccountId",
                table: "Terms");

            migrationBuilder.DropIndex(
                name: "IX_Terms_CreatedById",
                table: "Terms");

            migrationBuilder.DropIndex(
                name: "IX_Terms_DeactivatedById",
                table: "Terms");

            migrationBuilder.DropIndex(
                name: "IX_Terms_DeletedById",
                table: "Terms");

            migrationBuilder.DropIndex(
                name: "IX_Terms_UpdatedById",
                table: "Terms");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Terms");

            migrationBuilder.DropColumn(
                name: "DeactivatedById",
                table: "Terms");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "Terms");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Terms");
        }
    }
}
