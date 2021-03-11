using Microsoft.EntityFrameworkCore.Migrations;

namespace sppo.Migrations
{
    public partial class updatea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NotiBody",
                table: "notifications",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotiBody",
                table: "notifications");
        }
    }
}
