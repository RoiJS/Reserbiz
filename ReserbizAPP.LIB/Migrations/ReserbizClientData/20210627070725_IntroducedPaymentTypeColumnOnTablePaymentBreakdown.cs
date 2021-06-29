using Microsoft.EntityFrameworkCore.Migrations;

namespace ReserbizAPP.LIB.Migrations.ReserbizClientData
{
    public partial class IntroducedPaymentTypeColumnOnTablePaymentBreakdown : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentType",
                table: "PaymentBreakdowns",
                type: "int",
                nullable: false,
                defaultValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentType",
                table: "PaymentBreakdowns");
        }
    }
}
