using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sabim.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePersonelTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BirinciSinifaAyrilmaTarihi",
                table: "Personel",
                type: "datetime2",
                nullable: true); // Nullable olduğu için "true" yazılmıştır.
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirinciSinifaAyrilmaTarihi",
                table: "Personel");
        }
    }
}
