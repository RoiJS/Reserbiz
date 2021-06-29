using Microsoft.EntityFrameworkCore.Migrations;

namespace ReserbizAPP.LIB.Migrations.ReserbizClientData
{
    public partial class RenamePaymentTypeToPaymentForTypeColumnOnTablePaymentBreakdownsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentType",
                table: "PaymentBreakdowns",
                newName: "PaymentForType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentForType",
                table: "PaymentBreakdowns",
                newName: "PaymentType");
        }
    }
}
