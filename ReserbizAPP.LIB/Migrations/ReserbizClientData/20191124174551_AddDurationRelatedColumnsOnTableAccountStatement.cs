using Microsoft.EntityFrameworkCore.Migrations;

namespace ReserbizAPP.LIB.Migrations.ReserbizClientData
{
    public partial class AddDurationRelatedColumnsOnTableAccountStatement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdvancedPaymentDurationValue",
                table: "AccountStatements",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DepositPaymentDurationValue",
                table: "AccountStatements",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DurationUnit",
                table: "AccountStatements",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdvancedPaymentDurationValue",
                table: "AccountStatements");

            migrationBuilder.DropColumn(
                name: "DepositPaymentDurationValue",
                table: "AccountStatements");

            migrationBuilder.DropColumn(
                name: "DurationUnit",
                table: "AccountStatements");
        }
    }
}
