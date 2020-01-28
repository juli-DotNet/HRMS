using Microsoft.EntityFrameworkCore.Migrations;

namespace HRMS.Persistance.Migrations
{
    public partial class AddCodeAndFixCountryID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountyId",
                table: "Region");

            migrationBuilder.DropColumn(
                name: "CountyId",
                table: "City");

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "Region",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Country",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "City",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Country");

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "Region",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "CountyId",
                table: "Region",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "City",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "CountyId",
                table: "City",
                nullable: false,
                defaultValue: 0);
        }
    }
}
