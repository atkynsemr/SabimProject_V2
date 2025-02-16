using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sabim.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavciCalisilanKatip_Personel_KatipId",
                table: "SavciCalisilanKatip");

            migrationBuilder.DropForeignKey(
                name: "FK_SavciCalisilanKatip_Personel_SavciId",
                table: "SavciCalisilanKatip");

            migrationBuilder.AddForeignKey(
                name: "FK_SavciCalisilanKatip_Personel_KatipId",
                table: "SavciCalisilanKatip",
                column: "KatipId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SavciCalisilanKatip_Personel_SavciId",
                table: "SavciCalisilanKatip",
                column: "SavciId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavciCalisilanKatip_Personel_KatipId",
                table: "SavciCalisilanKatip");

            migrationBuilder.DropForeignKey(
                name: "FK_SavciCalisilanKatip_Personel_SavciId",
                table: "SavciCalisilanKatip");

            migrationBuilder.AddForeignKey(
                name: "FK_SavciCalisilanKatip_Personel_KatipId",
                table: "SavciCalisilanKatip",
                column: "KatipId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SavciCalisilanKatip_Personel_SavciId",
                table: "SavciCalisilanKatip",
                column: "SavciId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
