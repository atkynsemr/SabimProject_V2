using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sabim.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeMarkaModelTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MalzemeModel_MalzemeCinsi_MalzemeCinsiId",
                table: "MalzemeModel");

            migrationBuilder.DropIndex(
                name: "IX_MalzemeModel_MalzemeCinsiId",
                table: "MalzemeModel");

            migrationBuilder.DropColumn(
                name: "MalzemeCinsiId",
                table: "MalzemeModel");

            migrationBuilder.AddColumn<byte>(
                name: "MalzemeCinsiId",
                table: "MalzemeMarka",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateIndex(
                name: "IX_MalzemeMarka_MalzemeCinsiId",
                table: "MalzemeMarka",
                column: "MalzemeCinsiId");

            migrationBuilder.AddForeignKey(
                name: "FK_MalzemeMarka_MalzemeCinsi_MalzemeCinsiId",
                table: "MalzemeMarka",
                column: "MalzemeCinsiId",
                principalTable: "MalzemeCinsi",
                principalColumn: "MalzemeCinsiID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MalzemeMarka_MalzemeCinsi_MalzemeCinsiId",
                table: "MalzemeMarka");

            migrationBuilder.DropIndex(
                name: "IX_MalzemeMarka_MalzemeCinsiId",
                table: "MalzemeMarka");

            migrationBuilder.DropColumn(
                name: "MalzemeCinsiId",
                table: "MalzemeMarka");

            migrationBuilder.AddColumn<byte>(
                name: "MalzemeCinsiId",
                table: "MalzemeModel",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateIndex(
                name: "IX_MalzemeModel_MalzemeCinsiId",
                table: "MalzemeModel",
                column: "MalzemeCinsiId");

            migrationBuilder.AddForeignKey(
                name: "FK_MalzemeModel_MalzemeCinsi_MalzemeCinsiId",
                table: "MalzemeModel",
                column: "MalzemeCinsiId",
                principalTable: "MalzemeCinsi",
                principalColumn: "MalzemeCinsiID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
