using Microsoft.EntityFrameworkCore.Migrations;

namespace ReserbizAPP.LIB.Migrations.ReserbizClientData
{
    public partial class AddTermParentIdForeignKeyConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Terms_TermParentId",
                table: "Terms",
                column: "TermParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Terms_Terms_TermParentId",
                table: "Terms",
                column: "TermParentId",
                principalTable: "Terms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Terms_Terms_TermParentId",
                table: "Terms");

            migrationBuilder.DropIndex(
                name: "IX_Terms_TermParentId",
                table: "Terms");
        }
    }
}
