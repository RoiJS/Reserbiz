using Microsoft.EntityFrameworkCore.Migrations;

namespace ReserbizAPP.LIB.Migrations.ReserbizClientData
{
    public partial class MakeTermParentIdColumnNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Spaces_SpaceId",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Terms_SpaceTypeId",
                table: "Terms");

            migrationBuilder.AlterColumn<int>(
                name: "TermParentId",
                table: "Terms",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SpaceId",
                table: "Contracts",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Terms_SpaceTypeId",
                table: "Terms",
                column: "SpaceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Spaces_SpaceId",
                table: "Contracts",
                column: "SpaceId",
                principalTable: "Spaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Spaces_SpaceId",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Terms_SpaceTypeId",
                table: "Terms");

            migrationBuilder.AlterColumn<int>(
                name: "TermParentId",
                table: "Terms",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SpaceId",
                table: "Contracts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Terms_SpaceTypeId",
                table: "Terms",
                column: "SpaceTypeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Spaces_SpaceId",
                table: "Contracts",
                column: "SpaceId",
                principalTable: "Spaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
