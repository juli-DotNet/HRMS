using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HRMS.Persistance.Migrations
{
    public partial class AddToOrganigramCompanySiteId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompanySiteId",
                table: "Organigram",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Organigram_CompanySiteId",
                table: "Organigram",
                column: "CompanySiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Organigram_CompanySite_CompanySiteId",
                table: "Organigram",
                column: "CompanySiteId",
                principalTable: "CompanySite",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organigram_CompanySite_CompanySiteId",
                table: "Organigram");

            migrationBuilder.DropIndex(
                name: "IX_Organigram_CompanySiteId",
                table: "Organigram");

            migrationBuilder.DropColumn(
                name: "CompanySiteId",
                table: "Organigram");
        }
    }
}
