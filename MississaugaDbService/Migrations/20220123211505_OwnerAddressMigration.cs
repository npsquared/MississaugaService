using Microsoft.EntityFrameworkCore.Migrations;

namespace MississaugaDbService.Migrations
{
    public partial class OwnerAddressMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Owners");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Owners",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Owners_AddressId",
                table: "Owners",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Owners_Addresses_AddressId",
                table: "Owners",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Owners_Addresses_AddressId",
                table: "Owners");

            migrationBuilder.DropIndex(
                name: "IX_Owners_AddressId",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Owners");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Owners",
                type: "TEXT",
                nullable: true);
        }
    }
}
