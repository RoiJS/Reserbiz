using Microsoft.EntityFrameworkCore.Migrations;

namespace ReserbizAPP.LIB.Migrations.ReserbizClientData
{
    public partial class RemovedColumnsIncludeRentalFeeIncludeUtilityBillsIncludeMiscellaneousFeesAndIncludePenaltyAmountFromTableColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IncludeMiscellaneousFees",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "IncludePenaltyAmount",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "IncludeRentalFee",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "IncludeUtilityBills",
                table: "Contracts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IncludeMiscellaneousFees",
                table: "Contracts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IncludePenaltyAmount",
                table: "Contracts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IncludeRentalFee",
                table: "Contracts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IncludeUtilityBills",
                table: "Contracts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
