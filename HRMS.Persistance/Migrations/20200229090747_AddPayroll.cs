using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HRMS.Persistance.Migrations
{
    public partial class AddPayroll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeCompanyPayroll",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    IsValid = table.Column<bool>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: false),
                    OrganigramEmployeeId = table.Column<Guid>(nullable: false),
                    BrutoAmount = table.Column<decimal>(nullable: false),
                    NetoAmount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeCompanyPayroll", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeCompanyPayroll_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeCompanyPayroll_OrganigramEmployee_OrganigramEmployeeId",
                        column: x => x.OrganigramEmployeeId,
                        principalTable: "OrganigramEmployee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PayrollSeason",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    IsValid = table.Column<bool>(nullable: false),
                    year = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayrollSeason", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PayrollSegment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    IsValid = table.Column<bool>(nullable: false),
                    Nr = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayrollSegment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyPayroll",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    IsValid = table.Column<bool>(nullable: false),
                    IsPayed = table.Column<bool>(nullable: false),
                    PayrollSegmentId = table.Column<int>(nullable: false),
                    PayrollSeasonId = table.Column<int>(nullable: false),
                    TotalAmounBruto = table.Column<decimal>(nullable: false),
                    TotalAmounNeto = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyPayroll", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyPayroll_PayrollSeason_PayrollSeasonId",
                        column: x => x.PayrollSeasonId,
                        principalTable: "PayrollSeason",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyPayroll_PayrollSegment_PayrollSegmentId",
                        column: x => x.PayrollSegmentId,
                        principalTable: "PayrollSegment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPayroll_PayrollSeasonId",
                table: "CompanyPayroll",
                column: "PayrollSeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPayroll_PayrollSegmentId",
                table: "CompanyPayroll",
                column: "PayrollSegmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeCompanyPayroll_EmployeeId",
                table: "EmployeeCompanyPayroll",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeCompanyPayroll_OrganigramEmployeeId",
                table: "EmployeeCompanyPayroll",
                column: "OrganigramEmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyPayroll");

            migrationBuilder.DropTable(
                name: "EmployeeCompanyPayroll");

            migrationBuilder.DropTable(
                name: "PayrollSeason");

            migrationBuilder.DropTable(
                name: "PayrollSegment");
        }
    }
}
