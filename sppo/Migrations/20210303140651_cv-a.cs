using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace sppo.Migrations
{
    public partial class cva : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cv",
                table: "forms");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Cv",
                table: "forms",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
