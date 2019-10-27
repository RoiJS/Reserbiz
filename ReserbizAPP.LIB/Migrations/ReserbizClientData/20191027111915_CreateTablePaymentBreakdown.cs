using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReserbizAPP.LIB.Migrations.ReserbizClientData
{
    public partial class CreateTablePaymentBreakdown : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentBreakdowns",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountStatementId = table.Column<int>(nullable: false),
                    Amount = table.Column<float>(nullable: false),
                    ReceivedById = table.Column<int>(nullable: false),
                    DateReceived = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: false),
                    DateDeactivated = table.Column<DateTime>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentBreakdowns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentBreakdowns_AccountStatements_AccountStatementId",
                        column: x => x.AccountStatementId,
                        principalTable: "AccountStatements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentBreakdowns_Accounts_ReceivedById",
                        column: x => x.ReceivedById,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentBreakdowns_AccountStatementId",
                table: "PaymentBreakdowns",
                column: "AccountStatementId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentBreakdowns_ReceivedById",
                table: "PaymentBreakdowns",
                column: "ReceivedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentBreakdowns");
        }
    }
}
