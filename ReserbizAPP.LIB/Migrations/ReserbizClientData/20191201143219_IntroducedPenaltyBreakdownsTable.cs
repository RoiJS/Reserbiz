using Microsoft.EntityFrameworkCore.Migrations;

namespace ReserbizAPP.LIB.Migrations.ReserbizClientData
{
    public partial class IntroducedPenaltyBreakdownsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PenaltyBreakdown_AccountStatements_AccountStatementId",
                table: "PenaltyBreakdown");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PenaltyBreakdown",
                table: "PenaltyBreakdown");

            migrationBuilder.RenameTable(
                name: "PenaltyBreakdown",
                newName: "PenaltyBreakdowns");

            migrationBuilder.RenameIndex(
                name: "IX_PenaltyBreakdown_AccountStatementId",
                table: "PenaltyBreakdowns",
                newName: "IX_PenaltyBreakdowns_AccountStatementId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PenaltyBreakdowns",
                table: "PenaltyBreakdowns",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PenaltyBreakdowns_AccountStatements_AccountStatementId",
                table: "PenaltyBreakdowns",
                column: "AccountStatementId",
                principalTable: "AccountStatements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PenaltyBreakdowns_AccountStatements_AccountStatementId",
                table: "PenaltyBreakdowns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PenaltyBreakdowns",
                table: "PenaltyBreakdowns");

            migrationBuilder.RenameTable(
                name: "PenaltyBreakdowns",
                newName: "PenaltyBreakdown");

            migrationBuilder.RenameIndex(
                name: "IX_PenaltyBreakdowns_AccountStatementId",
                table: "PenaltyBreakdown",
                newName: "IX_PenaltyBreakdown_AccountStatementId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PenaltyBreakdown",
                table: "PenaltyBreakdown",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PenaltyBreakdown_AccountStatements_AccountStatementId",
                table: "PenaltyBreakdown",
                column: "AccountStatementId",
                principalTable: "AccountStatements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
