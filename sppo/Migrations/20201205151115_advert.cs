using Microsoft.EntityFrameworkCore.Migrations;

namespace sppo.Migrations
{
    public partial class advert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_forms_advertisements_AdvertisementId",
                table: "forms");

            migrationBuilder.AlterColumn<int>(
                name: "AdvertisementId",
                table: "forms",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_forms_advertisements_AdvertisementId",
                table: "forms",
                column: "AdvertisementId",
                principalTable: "advertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_forms_advertisements_AdvertisementId",
                table: "forms");

            migrationBuilder.AlterColumn<int>(
                name: "AdvertisementId",
                table: "forms",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_forms_advertisements_AdvertisementId",
                table: "forms",
                column: "AdvertisementId",
                principalTable: "advertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
