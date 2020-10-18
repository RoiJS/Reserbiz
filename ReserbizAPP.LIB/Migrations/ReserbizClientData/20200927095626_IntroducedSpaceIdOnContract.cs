using Microsoft.EntityFrameworkCore.Migrations;

namespace ReserbizAPP.LIB.Migrations.ReserbizClientData
{
    public partial class IntroducedSpaceIdOnContract : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SpaceId",
                table: "Contracts",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_SpaceId",
                table: "Contracts",
                column: "SpaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Spaces_SpaceId",
                table: "Contracts",
                column: "SpaceId",
                principalTable: "Spaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Spaces_SpaceId",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_SpaceId",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SpaceId",
                table: "Contracts");
        }
    }
}
