using Microsoft.EntityFrameworkCore.Migrations;

namespace ReserbizAPP.LIB.Migrations.ReserbizClientData
{
    public partial class RenameIncludeRentalFeesToIncludeRentalFeeColumnOnTableContracts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IncludeRentalFees",
                table: "Contracts",
                newName: "IncludeRentalFee");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IncludeRentalFee",
                table: "Contracts",
                newName: "IncludeRentalFees");
        }
    }
}
