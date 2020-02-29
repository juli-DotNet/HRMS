using Microsoft.EntityFrameworkCore.Migrations;

namespace HRMS.Persistance.Migrations
{
    public partial class UpdatePayrollModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyPayroll_PayrollSeason_PayrollSeasonId",
                table: "CompanyPayroll");

            migrationBuilder.DropIndex(
                name: "IX_CompanyPayroll_PayrollSeasonId",
                table: "CompanyPayroll");

            migrationBuilder.DropColumn(
                name: "PayrollSeasonId",
                table: "CompanyPayroll");

            migrationBuilder.AddColumn<int>(
                name: "PayrollSeasonId",
                table: "PayrollSegment",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PayrollSegment_PayrollSeasonId",
                table: "PayrollSegment",
                column: "PayrollSeasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_PayrollSegment_PayrollSeason_PayrollSeasonId",
                table: "PayrollSegment",
                column: "PayrollSeasonId",
                principalTable: "PayrollSeason",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PayrollSegment_PayrollSeason_PayrollSeasonId",
                table: "PayrollSegment");

            migrationBuilder.DropIndex(
                name: "IX_PayrollSegment_PayrollSeasonId",
                table: "PayrollSegment");

            migrationBuilder.DropColumn(
                name: "PayrollSeasonId",
                table: "PayrollSegment");

            migrationBuilder.AddColumn<int>(
                name: "PayrollSeasonId",
                table: "CompanyPayroll",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPayroll_PayrollSeasonId",
                table: "CompanyPayroll",
                column: "PayrollSeasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyPayroll_PayrollSeason_PayrollSeasonId",
                table: "CompanyPayroll",
                column: "PayrollSeasonId",
                principalTable: "PayrollSeason",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
