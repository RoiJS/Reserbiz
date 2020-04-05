using Microsoft.EntityFrameworkCore.Migrations;

namespace ReserbizAPP.LIB.Migrations.ReserbizClientData
{
    public partial class SetupAccountTenantActionTrackerConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Tenants",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeactivatedById",
                table: "Tenants",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedById",
                table: "Tenants",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedById",
                table: "Tenants",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_CreatedById",
                table: "Tenants",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_DeactivatedById",
                table: "Tenants",
                column: "DeactivatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_DeletedById",
                table: "Tenants",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_UpdatedById",
                table: "Tenants",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Tenants_CreatedById_Accounts_AccountId",
                table: "Tenants",
                column: "CreatedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Tenants_DeactivatedById_Accounts_AccountId",
                table: "Tenants",
                column: "DeactivatedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Tenants_DeletedById_Accounts_AccountId",
                table: "Tenants",
                column: "DeletedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Tenants_UpdatedById_Accounts_AccountId",
                table: "Tenants",
                column: "UpdatedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tenants_CreatedById_Accounts_AccountId",
                table: "Tenants");

            migrationBuilder.DropForeignKey(
                name: "FK_Tenants_DeactivatedById_Accounts_AccountId",
                table: "Tenants");

            migrationBuilder.DropForeignKey(
                name: "FK_Tenants_DeletedById_Accounts_AccountId",
                table: "Tenants");

            migrationBuilder.DropForeignKey(
                name: "FK_Tenants_UpdatedById_Accounts_AccountId",
                table: "Tenants");

            migrationBuilder.DropIndex(
                name: "IX_Tenants_CreatedById",
                table: "Tenants");

            migrationBuilder.DropIndex(
                name: "IX_Tenants_DeactivatedById",
                table: "Tenants");

            migrationBuilder.DropIndex(
                name: "IX_Tenants_DeletedById",
                table: "Tenants");

            migrationBuilder.DropIndex(
                name: "IX_Tenants_UpdatedById",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "DeactivatedById",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Tenants");
        }
    }
}
