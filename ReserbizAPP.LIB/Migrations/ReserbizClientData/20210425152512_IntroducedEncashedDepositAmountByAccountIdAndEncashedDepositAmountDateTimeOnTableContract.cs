using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReserbizAPP.LIB.Migrations.ReserbizClientData
{
    public partial class IntroducedEncashedDepositAmountByAccountIdAndEncashedDepositAmountDateTimeOnTableContract : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EncashedDepositAmountByAccountId",
                table: "Contracts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EncashedDepositAmountDateTime",
                table: "Contracts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_EncashedDepositAmountByAccountId",
                table: "Contracts",
                column: "EncashedDepositAmountByAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Accounts_EncashedDepositAmountByAccountId",
                table: "Contracts",
                column: "EncashedDepositAmountByAccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Accounts_EncashedDepositAmountByAccountId",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_EncashedDepositAmountByAccountId",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "EncashedDepositAmountByAccountId",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "EncashedDepositAmountDateTime",
                table: "Contracts");
        }
    }
}
