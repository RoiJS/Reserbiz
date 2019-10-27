using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReserbizAPP.LIB.Migrations.ReserbizClientData
{
    public partial class CreateTableAccountStatementAndAccountStatementMiscellanous : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountStatements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContractId = table.Column<int>(nullable: false),
                    Rate = table.Column<float>(nullable: false),
                    ElectricBill = table.Column<float>(nullable: false),
                    WaterBill = table.Column<float>(nullable: false),
                    Penalty = table.Column<float>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: false),
                    DateDeactivated = table.Column<DateTime>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountStatements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountStatements_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountStatementMiscellaneous",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Amount = table.Column<float>(nullable: false),
                    AccountStatementId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: false),
                    DateDeactivated = table.Column<DateTime>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountStatementMiscellaneous", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountStatementMiscellaneous_AccountStatements_AccountStatementId",
                        column: x => x.AccountStatementId,
                        principalTable: "AccountStatements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountStatementMiscellaneous_AccountStatementId",
                table: "AccountStatementMiscellaneous",
                column: "AccountStatementId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountStatements_ContractId",
                table: "AccountStatements",
                column: "ContractId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountStatementMiscellaneous");

            migrationBuilder.DropTable(
                name: "AccountStatements");
        }
    }
}
