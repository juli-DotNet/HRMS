using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HRMS.Persistance.Migrations
{
    public partial class EmployeeTypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BithDate",
                table: "Employee",
                newName: "BirthDate");

            migrationBuilder.AlterColumn<Guid>(
                name: "ContactId",
                table: "Employee",
                nullable: true,
                oldClrType: typeof(Guid));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BirthDate",
                table: "Employee",
                newName: "BithDate");

            migrationBuilder.AlterColumn<Guid>(
                name: "ContactId",
                table: "Employee",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);
        }
    }
}
