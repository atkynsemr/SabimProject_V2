using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sabim.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeMarkaModelTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MalzemeMarka_MarkaAdi",
                table: "MalzemeMarka");

            migrationBuilder.CreateIndex(
                name: "IX_MalzemeMarka_MarkaAdi_MalzemeCinsiId",
                table: "MalzemeMarka",
                columns: new[] { "MarkaAdi", "MalzemeCinsiId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MalzemeMarka_MarkaAdi_MalzemeCinsiId",
                table: "MalzemeMarka");

            migrationBuilder.CreateIndex(
                name: "IX_MalzemeMarka_MarkaAdi",
                table: "MalzemeMarka",
                column: "MarkaAdi",
                unique: true);
        }
    }
}
