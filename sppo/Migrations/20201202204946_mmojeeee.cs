using Microsoft.EntityFrameworkCore.Migrations;

namespace sppo.Migrations
{
    public partial class mmojeeee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_advertisements_AspNetUsers_ProfileId1",
                table: "advertisements");

            migrationBuilder.DropIndex(
                name: "IX_advertisements_ProfileId1",
                table: "advertisements");

            migrationBuilder.DropColumn(
                name: "ProfileId1",
                table: "advertisements");

            migrationBuilder.AlterColumn<string>(
                name: "ProfileId",
                table: "advertisements",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_advertisements_ProfileId",
                table: "advertisements",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_advertisements_AspNetUsers_ProfileId",
                table: "advertisements",
                column: "ProfileId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_advertisements_AspNetUsers_ProfileId",
                table: "advertisements");

            migrationBuilder.DropIndex(
                name: "IX_advertisements_ProfileId",
                table: "advertisements");

            migrationBuilder.AlterColumn<int>(
                name: "ProfileId",
                table: "advertisements",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfileId1",
                table: "advertisements",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_advertisements_ProfileId1",
                table: "advertisements",
                column: "ProfileId1");

            migrationBuilder.AddForeignKey(
                name: "FK_advertisements_AspNetUsers_ProfileId1",
                table: "advertisements",
                column: "ProfileId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
