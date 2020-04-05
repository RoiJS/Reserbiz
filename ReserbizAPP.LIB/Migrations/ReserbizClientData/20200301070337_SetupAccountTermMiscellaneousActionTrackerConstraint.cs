using Microsoft.EntityFrameworkCore.Migrations;

namespace ReserbizAPP.LIB.Migrations.ReserbizClientData
{
    public partial class SetupAccountTermMiscellaneousActionTrackerConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "TermMiscellaneous",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeactivatedById",
                table: "TermMiscellaneous",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedById",
                table: "TermMiscellaneous",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedById",
                table: "TermMiscellaneous",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TermMiscellaneous_CreatedById",
                table: "TermMiscellaneous",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TermMiscellaneous_DeactivatedById",
                table: "TermMiscellaneous",
                column: "DeactivatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TermMiscellaneous_DeletedById",
                table: "TermMiscellaneous",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_TermMiscellaneous_UpdatedById",
                table: "TermMiscellaneous",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_TermMiscellaneous_CreatedById_Accounts_AccountId",
                table: "TermMiscellaneous",
                column: "CreatedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TermMiscellaneous_DeactivatedById_Accounts_AccountId",
                table: "TermMiscellaneous",
                column: "DeactivatedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TermMiscellaneous_DeletedById_Accounts_AccountId",
                table: "TermMiscellaneous",
                column: "DeletedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TermMiscellaneous_UpdatedById_Accounts_AccountId",
                table: "TermMiscellaneous",
                column: "UpdatedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TermMiscellaneous_CreatedById_Accounts_AccountId",
                table: "TermMiscellaneous");

            migrationBuilder.DropForeignKey(
                name: "FK_TermMiscellaneous_DeactivatedById_Accounts_AccountId",
                table: "TermMiscellaneous");

            migrationBuilder.DropForeignKey(
                name: "FK_TermMiscellaneous_DeletedById_Accounts_AccountId",
                table: "TermMiscellaneous");

            migrationBuilder.DropForeignKey(
                name: "FK_TermMiscellaneous_UpdatedById_Accounts_AccountId",
                table: "TermMiscellaneous");

            migrationBuilder.DropIndex(
                name: "IX_TermMiscellaneous_CreatedById",
                table: "TermMiscellaneous");

            migrationBuilder.DropIndex(
                name: "IX_TermMiscellaneous_DeactivatedById",
                table: "TermMiscellaneous");

            migrationBuilder.DropIndex(
                name: "IX_TermMiscellaneous_DeletedById",
                table: "TermMiscellaneous");

            migrationBuilder.DropIndex(
                name: "IX_TermMiscellaneous_UpdatedById",
                table: "TermMiscellaneous");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "TermMiscellaneous");

            migrationBuilder.DropColumn(
                name: "DeactivatedById",
                table: "TermMiscellaneous");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "TermMiscellaneous");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "TermMiscellaneous");
        }
    }
}
