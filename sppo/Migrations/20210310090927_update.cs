using Microsoft.EntityFrameworkCore.Migrations;

namespace sppo.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotiBody",
                table: "notifications");

            migrationBuilder.DropColumn(
                name: "NotiHeader",
                table: "notifications");

            migrationBuilder.AddColumn<string>(
                name: "FromUserName",
                table: "notifications",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromUserName",
                table: "notifications");

            migrationBuilder.AddColumn<string>(
                name: "NotiBody",
                table: "notifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NotiHeader",
                table: "notifications",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
