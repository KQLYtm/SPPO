using Microsoft.EntityFrameworkCore.Migrations;

namespace sppo.Migrations
{
    public partial class moje33 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_advertisements_companies_CompanyId",
                table: "advertisements");

            migrationBuilder.DropForeignKey(
                name: "FK_advertisements_users_UserId",
                table: "advertisements");

            migrationBuilder.DropIndex(
                name: "IX_advertisements_CompanyId",
                table: "advertisements");

            migrationBuilder.DropIndex(
                name: "IX_advertisements_UserId",
                table: "advertisements");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "advertisements");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "advertisements");

            migrationBuilder.AddColumn<int>(
                name: "ProfileId",
                table: "advertisements",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProfileId1",
                table: "advertisements",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_advertisements_AspNetUsers_ProfileId1",
                table: "advertisements");

            migrationBuilder.DropIndex(
                name: "IX_advertisements_ProfileId1",
                table: "advertisements");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "advertisements");

            migrationBuilder.DropColumn(
                name: "ProfileId1",
                table: "advertisements");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "advertisements",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "advertisements",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_advertisements_CompanyId",
                table: "advertisements",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_advertisements_UserId",
                table: "advertisements",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_advertisements_companies_CompanyId",
                table: "advertisements",
                column: "CompanyId",
                principalTable: "companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_advertisements_users_UserId",
                table: "advertisements",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
