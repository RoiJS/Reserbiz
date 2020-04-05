using Microsoft.EntityFrameworkCore.Migrations;

namespace ReserbizAPP.LIB.Migrations.ReserbizClientData
{
    public partial class SetupAccountSpaceTypesActionTrackerConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "SpaceTypes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeactivatedById",
                table: "SpaceTypes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedById",
                table: "SpaceTypes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedById",
                table: "SpaceTypes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SpaceTypes_CreatedById",
                table: "SpaceTypes",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SpaceTypes_DeactivatedById",
                table: "SpaceTypes",
                column: "DeactivatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SpaceTypes_DeletedById",
                table: "SpaceTypes",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_SpaceTypes_UpdatedById",
                table: "SpaceTypes",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_SpaceTypes_CreatedById_Accounts_AccountId",
                table: "SpaceTypes",
                column: "CreatedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SpaceTypes_DeactivatedById_Accounts_AccountId",
                table: "SpaceTypes",
                column: "DeactivatedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SpaceTypes_DeletedById_Accounts_AccountId",
                table: "SpaceTypes",
                column: "DeletedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SpaceTypes_UpdatedById_Accounts_AccountId",
                table: "SpaceTypes",
                column: "UpdatedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpaceTypes_CreatedById_Accounts_AccountId",
                table: "SpaceTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_SpaceTypes_DeactivatedById_Accounts_AccountId",
                table: "SpaceTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_SpaceTypes_DeletedById_Accounts_AccountId",
                table: "SpaceTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_SpaceTypes_UpdatedById_Accounts_AccountId",
                table: "SpaceTypes");

            migrationBuilder.DropIndex(
                name: "IX_SpaceTypes_CreatedById",
                table: "SpaceTypes");

            migrationBuilder.DropIndex(
                name: "IX_SpaceTypes_DeactivatedById",
                table: "SpaceTypes");

            migrationBuilder.DropIndex(
                name: "IX_SpaceTypes_DeletedById",
                table: "SpaceTypes");

            migrationBuilder.DropIndex(
                name: "IX_SpaceTypes_UpdatedById",
                table: "SpaceTypes");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "SpaceTypes");

            migrationBuilder.DropColumn(
                name: "DeactivatedById",
                table: "SpaceTypes");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "SpaceTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "SpaceTypes");
        }
    }
}
