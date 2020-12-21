using Microsoft.EntityFrameworkCore.Migrations;

namespace sppo.Migrations
{
    public partial class Brisanje : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DrivingLicence",
                table: "forms");

            migrationBuilder.DropColumn(
                name: "Employed",
                table: "forms");

            migrationBuilder.DropColumn(
                name: "NumberOfLanguages",
                table: "forms");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DrivingLicence",
                table: "forms",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Employed",
                table: "forms",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfLanguages",
                table: "forms",
                type: "int",
                nullable: true);
        }
    }
}
