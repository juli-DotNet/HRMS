using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HRMS.Persistance.Migrations
{
    public partial class ModelsRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organigram_CompanySite_CompanySiteId",
                table: "Organigram");

            migrationBuilder.DropTable(
                name: "CompanySite");

            migrationBuilder.DropTable(
                name: "Site");

            migrationBuilder.RenameColumn(
                name: "CompanySiteId",
                table: "Organigram",
                newName: "CompanyDepartamentId");

            migrationBuilder.RenameIndex(
                name: "IX_Organigram_CompanySiteId",
                table: "Organigram",
                newName: "IX_Organigram_CompanyDepartamentId");

            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                table: "Company",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Departament",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    IsValid = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departament", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyDepartament",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    IsValid = table.Column<bool>(nullable: false),
                    DepartamentId = table.Column<Guid>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyDepartament", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyDepartament_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyDepartament_Departament_DepartamentId",
                        column: x => x.DepartamentId,
                        principalTable: "Departament",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Company_AddressId",
                table: "Company",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDepartament_CompanyId",
                table: "CompanyDepartament",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDepartament_DepartamentId",
                table: "CompanyDepartament",
                column: "DepartamentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Company_Address_AddressId",
                table: "Company",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Organigram_CompanyDepartament_CompanyDepartamentId",
                table: "Organigram",
                column: "CompanyDepartamentId",
                principalTable: "CompanyDepartament",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_Address_AddressId",
                table: "Company");

            migrationBuilder.DropForeignKey(
                name: "FK_Organigram_CompanyDepartament_CompanyDepartamentId",
                table: "Organigram");

            migrationBuilder.DropTable(
                name: "CompanyDepartament");

            migrationBuilder.DropTable(
                name: "Departament");

            migrationBuilder.DropIndex(
                name: "IX_Company_AddressId",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Company");

            migrationBuilder.RenameColumn(
                name: "CompanyDepartamentId",
                table: "Organigram",
                newName: "CompanySiteId");

            migrationBuilder.RenameIndex(
                name: "IX_Organigram_CompanyDepartamentId",
                table: "Organigram",
                newName: "IX_Organigram_CompanySiteId");

            migrationBuilder.CreateTable(
                name: "Site",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AddressId = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    IsValid = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Site", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Site_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanySite",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    IsValid = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    SiteId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanySite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanySite_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanySite_Site_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanySite_CompanyId",
                table: "CompanySite",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanySite_SiteId",
                table: "CompanySite",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_Site_AddressId",
                table: "Site",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Organigram_CompanySite_CompanySiteId",
                table: "Organigram",
                column: "CompanySiteId",
                principalTable: "CompanySite",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
