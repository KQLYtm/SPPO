using Microsoft.EntityFrameworkCore.Migrations;

namespace sppo.Migrations
{
    public partial class moje344 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reviews_companies_CompanyId",
                table: "reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_reviews_users_UserId",
                table: "reviews");

            migrationBuilder.DropIndex(
                name: "IX_reviews_CompanyId",
                table: "reviews");

            migrationBuilder.DropIndex(
                name: "IX_reviews_UserId",
                table: "reviews");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "reviews");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "reviews");

            migrationBuilder.AddColumn<int>(
                name: "ProfileId",
                table: "reviews",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProfileId1",
                table: "reviews",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_reviews_ProfileId1",
                table: "reviews",
                column: "ProfileId1");

            migrationBuilder.AddForeignKey(
                name: "FK_reviews_AspNetUsers_ProfileId1",
                table: "reviews",
                column: "ProfileId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reviews_AspNetUsers_ProfileId1",
                table: "reviews");

            migrationBuilder.DropIndex(
                name: "IX_reviews_ProfileId1",
                table: "reviews");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "reviews");

            migrationBuilder.DropColumn(
                name: "ProfileId1",
                table: "reviews");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "reviews",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "reviews",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_reviews_CompanyId",
                table: "reviews",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_reviews_UserId",
                table: "reviews",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_reviews_companies_CompanyId",
                table: "reviews",
                column: "CompanyId",
                principalTable: "companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_reviews_users_UserId",
                table: "reviews",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
