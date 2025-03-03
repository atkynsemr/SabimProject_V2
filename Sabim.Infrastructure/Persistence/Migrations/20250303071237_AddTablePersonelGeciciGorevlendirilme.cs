using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sabim.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTablePersonelGeciciGorevlendirilme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersonelGeciciGorevlendirilme",
                columns: table => new
                {
                    PersonelGeciciGorevlendirilmeID = table.Column<short>(type: "SMALLINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonelId = table.Column<short>(type: "SMALLINT", nullable: false),
                    GorevlendirilmeTipiId = table.Column<short>(type: "SMALLINT", nullable: false),
                    BaslangicTarihi = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    BitisTarihi = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    GorevlendirilmeAktifMi = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    PersonelAyrilisYeriId = table.Column<short>(type: "SMALLINT", nullable: false),
                    OlusturanPersonelId = table.Column<short>(type: "SMALLINT", nullable: true),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GuncelleyenPersonelId = table.Column<short>(type: "SMALLINT", nullable: true),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SilenPersonelId = table.Column<short>(type: "SMALLINT", nullable: true),
                    SilinmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DurumId = table.Column<short>(type: "SMALLINT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonelGeciciGorevlendirilme", x => x.PersonelGeciciGorevlendirilmeID);
                    table.ForeignKey(
                        name: "FK_PersonelGeciciGorevlendirilme_Durum_DurumId",
                        column: x => x.DurumId,
                        principalTable: "Durum",
                        principalColumn: "DurumID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonelGeciciGorevlendirilme_Kurum_PersonelAyrilisYeriId",
                        column: x => x.PersonelAyrilisYeriId,
                        principalTable: "Kurum",
                        principalColumn: "KurumID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonelGeciciGorevlendirilme_Personel_GuncelleyenPersonelId",
                        column: x => x.GuncelleyenPersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonelGeciciGorevlendirilme_Personel_OlusturanPersonelId",
                        column: x => x.OlusturanPersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonelGeciciGorevlendirilme_Personel_PersonelId",
                        column: x => x.PersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonelGeciciGorevlendirilme_Personel_SilenPersonelId",
                        column: x => x.SilenPersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonelGeciciGorevlendirilme_DurumId",
                table: "PersonelGeciciGorevlendirilme",
                column: "DurumId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelGeciciGorevlendirilme_GuncelleyenPersonelId",
                table: "PersonelGeciciGorevlendirilme",
                column: "GuncelleyenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelGeciciGorevlendirilme_OlusturanPersonelId",
                table: "PersonelGeciciGorevlendirilme",
                column: "OlusturanPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelGeciciGorevlendirilme_PersonelAyrilisYeriId",
                table: "PersonelGeciciGorevlendirilme",
                column: "PersonelAyrilisYeriId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelGeciciGorevlendirilme_PersonelId",
                table: "PersonelGeciciGorevlendirilme",
                column: "PersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelGeciciGorevlendirilme_SilenPersonelId",
                table: "PersonelGeciciGorevlendirilme",
                column: "SilenPersonelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonelGeciciGorevlendirilme");
        }
    }
}
