using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sabim.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updaterelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavciCalisilanKatip_Personel_KatipId",
                table: "SavciCalisilanKatip");

            migrationBuilder.AddForeignKey(
                name: "FK_SavciCalisilanKatip_Personel_KatipId",
                table: "SavciCalisilanKatip",
                column: "KatipId",
                principalTable: "Personel",
                principalColumn: "PersonelID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavciCalisilanKatip_Personel_KatipId",
                table: "SavciCalisilanKatip");

            migrationBuilder.AddForeignKey(
                name: "FK_SavciCalisilanKatip_Personel_KatipId",
                table: "SavciCalisilanKatip",
                column: "KatipId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
