using Microsoft.EntityFrameworkCore.Migrations;

namespace ReserbizAPP.LIB.Migrations.ReserbizClientData
{
    public partial class IntroducedEncashDepositAmountOnTableContract : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EncashDepositAmount",
                table: "Contracts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EncashDepositAmount",
                table: "Contracts");
        }
    }
}
