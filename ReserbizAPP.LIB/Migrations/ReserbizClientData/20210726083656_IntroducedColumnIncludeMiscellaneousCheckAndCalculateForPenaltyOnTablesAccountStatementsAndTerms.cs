using Microsoft.EntityFrameworkCore.Migrations;

namespace ReserbizAPP.LIB.Migrations.ReserbizClientData
{
    public partial class IntroducedColumnIncludeMiscellaneousCheckAndCalculateForPenaltyOnTablesAccountStatementsAndTerms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IncludeMiscellaneousCheckAndCalculateForPenalty",
                table: "Terms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IncludeMiscellaneousCheckAndCalculateForPenalty",
                table: "AccountStatements",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IncludeMiscellaneousCheckAndCalculateForPenalty",
                table: "Terms");

            migrationBuilder.DropColumn(
                name: "IncludeMiscellaneousCheckAndCalculateForPenalty",
                table: "AccountStatements");
        }
    }
}
