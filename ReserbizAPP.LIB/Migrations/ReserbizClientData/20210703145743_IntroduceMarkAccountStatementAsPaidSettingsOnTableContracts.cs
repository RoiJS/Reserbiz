using Microsoft.EntityFrameworkCore.Migrations;

namespace ReserbizAPP.LIB.Migrations.ReserbizClientData
{
    public partial class IntroduceMarkAccountStatementAsPaidSettingsOnTableContracts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IncludeMiscellaneousFees",
                table: "Contracts",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IncludePenaltyAmount",
                table: "Contracts",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IncludeRentalFees",
                table: "Contracts",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IncludeUtilityBills",
                table: "Contracts",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IncludeMiscellaneousFees",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "IncludePenaltyAmount",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "IncludeRentalFees",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "IncludeUtilityBills",
                table: "Contracts");
        }
    }
}
