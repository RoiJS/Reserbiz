using Microsoft.EntityFrameworkCore.Migrations;

namespace ReserbizAPP.LIB.Migrations.ReserbizClientData
{
    public partial class SetupAccountContactPersonsActionTrackerConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "UpdatedById",
                table: "Tenants",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "DeletedById",
                table: "Tenants",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "DeactivatedById",
                table: "Tenants",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CreatedById",
                table: "Tenants",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "ContactPersons",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeactivatedById",
                table: "ContactPersons",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedById",
                table: "ContactPersons",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedById",
                table: "ContactPersons",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UpdatedById",
                table: "Accounts",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "DeletedById",
                table: "Accounts",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "DeactivatedById",
                table: "Accounts",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CreatedById",
                table: "Accounts",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_ContactPersons_CreatedById",
                table: "ContactPersons",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ContactPersons_DeactivatedById",
                table: "ContactPersons",
                column: "DeactivatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ContactPersons_DeletedById",
                table: "ContactPersons",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_ContactPersons_UpdatedById",
                table: "ContactPersons",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_CreatedById_Accounts_AccountId",
                table: "Accounts",
                column: "CreatedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_DeactivatedById_Accounts_AccountId",
                table: "Accounts",
                column: "DeactivatedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_DeletedById_Accounts_AccountId",
                table: "Accounts",
                column: "DeletedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_UpdatedById_Accounts_AccountId",
                table: "Accounts",
                column: "UpdatedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactPersons_CreatedById_Accounts_AccountId",
                table: "ContactPersons",
                column: "CreatedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactPersons_DeactivatedById_Accounts_AccountId",
                table: "ContactPersons",
                column: "DeactivatedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactPersons_DeletedById_Accounts_AccountId",
                table: "ContactPersons",
                column: "DeletedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactPersons_UpdatedById_Accounts_AccountId",
                table: "ContactPersons",
                column: "UpdatedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tenants_CreatedById_Accounts_AccountId",
                table: "Tenants",
                column: "CreatedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tenants_DeactivatedById_Accounts_AccountId",
                table: "Tenants",
                column: "DeactivatedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tenants_DeletedById_Accounts_AccountId",
                table: "Tenants",
                column: "DeletedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tenants_UpdatedById_Accounts_AccountId",
                table: "Tenants",
                column: "UpdatedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.DropForeignKey(
                name: "FK_ContactPersons_CreatedById_Accounts_AccountId",
                table: "ContactPersons");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactPersons_DeactivatedById_Accounts_AccountId",
                table: "ContactPersons");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactPersons_DeletedById_Accounts_AccountId",
                table: "ContactPersons");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactPersons_UpdatedById_Accounts_AccountId",
                table: "ContactPersons");

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
                name: "IX_ContactPersons_CreatedById",
                table: "ContactPersons");

            migrationBuilder.DropIndex(
                name: "IX_ContactPersons_DeactivatedById",
                table: "ContactPersons");

            migrationBuilder.DropIndex(
                name: "IX_ContactPersons_DeletedById",
                table: "ContactPersons");

            migrationBuilder.DropIndex(
                name: "IX_ContactPersons_UpdatedById",
                table: "ContactPersons");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "ContactPersons");

            migrationBuilder.DropColumn(
                name: "DeactivatedById",
                table: "ContactPersons");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "ContactPersons");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "ContactPersons");

            migrationBuilder.AlterColumn<int>(
                name: "UpdatedById",
                table: "Tenants",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DeletedById",
                table: "Tenants",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DeactivatedById",
                table: "Tenants",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedById",
                table: "Tenants",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UpdatedById",
                table: "Accounts",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DeletedById",
                table: "Accounts",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DeactivatedById",
                table: "Accounts",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedById",
                table: "Accounts",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_CreatedById_Accounts_AccountId",
                table: "Accounts",
                column: "CreatedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_DeactivatedById_Accounts_AccountId",
                table: "Accounts",
                column: "DeactivatedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_DeletedById_Accounts_AccountId",
                table: "Accounts",
                column: "DeletedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_UpdatedById_Accounts_AccountId",
                table: "Accounts",
                column: "UpdatedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tenants_CreatedById_Accounts_AccountId",
                table: "Tenants",
                column: "CreatedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tenants_DeactivatedById_Accounts_AccountId",
                table: "Tenants",
                column: "DeactivatedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tenants_DeletedById_Accounts_AccountId",
                table: "Tenants",
                column: "DeletedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tenants_UpdatedById_Accounts_AccountId",
                table: "Tenants",
                column: "UpdatedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
