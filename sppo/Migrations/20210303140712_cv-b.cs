using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace sppo.Migrations
{
    public partial class cvb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Cv",
                table: "forms",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cv",
                table: "forms");
        }
    }
}
