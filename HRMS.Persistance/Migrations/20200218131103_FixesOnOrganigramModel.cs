using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HRMS.Persistance.Migrations
{
    public partial class FixesOnOrganigramModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "CompanySiteId",
                table: "Organigram",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Organigram",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Organigram_CompanyId",
                table: "Organigram",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Organigram_Company_CompanyId",
                table: "Organigram",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organigram_Company_CompanyId",
                table: "Organigram");

            migrationBuilder.DropIndex(
                name: "IX_Organigram_CompanyId",
                table: "Organigram");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Organigram");

            migrationBuilder.AlterColumn<Guid>(
                name: "CompanySiteId",
                table: "Organigram",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);
        }
    }
}
