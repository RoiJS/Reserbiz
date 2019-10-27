using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReserbizAPP.LIB.Migrations.ReserbizClientData
{
    public partial class CreateTableTermAndTermMiscellanous : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Terms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    SpaceTypeId = table.Column<int>(nullable: false),
                    Rate = table.Column<float>(nullable: false),
                    MaximumNumberOfOccupants = table.Column<int>(nullable: false),
                    DurationUnit = table.Column<int>(nullable: false),
                    AdvancedPaymentDurationValue = table.Column<int>(nullable: false),
                    DepositPaymentDurationValue = table.Column<int>(nullable: false),
                    ExcludeElectricBill = table.Column<bool>(nullable: false),
                    ElectricBillAmount = table.Column<float>(nullable: false),
                    ExcludeWaterBill = table.Column<bool>(nullable: false),
                    WaterBillAmount = table.Column<float>(nullable: false),
                    PenaltyValue = table.Column<float>(nullable: false),
                    PenaltyValueType = table.Column<int>(nullable: false),
                    PenaltyAmountPerDurationUnit = table.Column<int>(nullable: false),
                    PenaltyEffectiveAfterDurationValue = table.Column<int>(nullable: false),
                    PenaltyEffectiveAfterDurationUnit = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: false),
                    DateDeactivated = table.Column<DateTime>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Terms_SpaceTypes_SpaceTypeId",
                        column: x => x.SpaceTypeId,
                        principalTable: "SpaceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TermMiscellaneous",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Amount = table.Column<float>(nullable: false),
                    TermId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: false),
                    DateDeactivated = table.Column<DateTime>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TermMiscellaneous", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TermMiscellaneous_Terms_TermId",
                        column: x => x.TermId,
                        principalTable: "Terms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TermMiscellaneous_TermId",
                table: "TermMiscellaneous",
                column: "TermId");

            migrationBuilder.CreateIndex(
                name: "IX_Terms_SpaceTypeId",
                table: "Terms",
                column: "SpaceTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TermMiscellaneous");

            migrationBuilder.DropTable(
                name: "Terms");
        }
    }
}
