using Microsoft.EntityFrameworkCore.Migrations;

namespace ReserbizAPP.LIB.Migrations.ReserbizClientData
{
    public partial class SetupAccountPaymentBreakdownsActionTrackerConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "PaymentBreakdowns",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeactivatedById",
                table: "PaymentBreakdowns",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedById",
                table: "PaymentBreakdowns",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedById",
                table: "PaymentBreakdowns",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentBreakdowns_CreatedById",
                table: "PaymentBreakdowns",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentBreakdowns_DeactivatedById",
                table: "PaymentBreakdowns",
                column: "DeactivatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentBreakdowns_DeletedById",
                table: "PaymentBreakdowns",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentBreakdowns_UpdatedById",
                table: "PaymentBreakdowns",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentBreakdown_CreatedById_Accounts_AccountId",
                table: "PaymentBreakdowns",
                column: "CreatedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentBreakdowns_DeactivatedById_Accounts_AccountId",
                table: "PaymentBreakdowns",
                column: "DeactivatedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentBreakdown_DeletedById_Accounts_AccountId",
                table: "PaymentBreakdowns",
                column: "DeletedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentBreakdowns_UpdatedById_Accounts_AccountId",
                table: "PaymentBreakdowns",
                column: "UpdatedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentBreakdown_CreatedById_Accounts_AccountId",
                table: "PaymentBreakdowns");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentBreakdowns_DeactivatedById_Accounts_AccountId",
                table: "PaymentBreakdowns");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentBreakdown_DeletedById_Accounts_AccountId",
                table: "PaymentBreakdowns");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentBreakdowns_UpdatedById_Accounts_AccountId",
                table: "PaymentBreakdowns");

            migrationBuilder.DropIndex(
                name: "IX_PaymentBreakdowns_CreatedById",
                table: "PaymentBreakdowns");

            migrationBuilder.DropIndex(
                name: "IX_PaymentBreakdowns_DeactivatedById",
                table: "PaymentBreakdowns");

            migrationBuilder.DropIndex(
                name: "IX_PaymentBreakdowns_DeletedById",
                table: "PaymentBreakdowns");

            migrationBuilder.DropIndex(
                name: "IX_PaymentBreakdowns_UpdatedById",
                table: "PaymentBreakdowns");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "PaymentBreakdowns");

            migrationBuilder.DropColumn(
                name: "DeactivatedById",
                table: "PaymentBreakdowns");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "PaymentBreakdowns");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "PaymentBreakdowns");
        }
    }
}
