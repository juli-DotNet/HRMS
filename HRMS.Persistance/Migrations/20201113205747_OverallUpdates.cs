using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HRMS.Persistance.Migrations
{
    public partial class OverallUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompanyPayrollId",
                table: "EmployeeCompanyPayroll",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "CompanyPayroll",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeCompanyPayroll_CompanyPayrollId",
                table: "EmployeeCompanyPayroll",
                column: "CompanyPayrollId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPayroll_CompanyId",
                table: "CompanyPayroll",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyPayroll_Company_CompanyId",
                table: "CompanyPayroll",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeCompanyPayroll_CompanyPayroll_CompanyPayrollId",
                table: "EmployeeCompanyPayroll",
                column: "CompanyPayrollId",
                principalTable: "CompanyPayroll",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyPayroll_Company_CompanyId",
                table: "CompanyPayroll");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeCompanyPayroll_CompanyPayroll_CompanyPayrollId",
                table: "EmployeeCompanyPayroll");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeCompanyPayroll_CompanyPayrollId",
                table: "EmployeeCompanyPayroll");

            migrationBuilder.DropIndex(
                name: "IX_CompanyPayroll_CompanyId",
                table: "CompanyPayroll");

            migrationBuilder.DropColumn(
                name: "CompanyPayrollId",
                table: "EmployeeCompanyPayroll");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "CompanyPayroll");
        }
    }
}
