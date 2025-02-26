using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sabim.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTablePersonelAyrilisNedenleriPersonelAyrilis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersonelAyrilisNedenleri",
                columns: table => new
                {
                    PersonelAyrilisNedenleriID = table.Column<byte>(type: "TINYINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Aciklama = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    KaliciAyrilisMi = table.Column<bool>(type: "BIT", nullable: false, defaultValue: false),
                    DonanimUyarisi = table.Column<bool>(type: "BIT", nullable: false, defaultValue: false),
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
                    table.PrimaryKey("PK_PersonelAyrilisNedenleri", x => x.PersonelAyrilisNedenleriID);
                    table.ForeignKey(
                        name: "FK_PersonelAyrilisNedenleri_Durum_DurumId",
                        column: x => x.DurumId,
                        principalTable: "Durum",
                        principalColumn: "DurumID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonelAyrilisNedenleri_Personel_GuncelleyenPersonelId",
                        column: x => x.GuncelleyenPersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonelAyrilisNedenleri_Personel_OlusturanPersonelId",
                        column: x => x.OlusturanPersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonelAyrilisNedenleri_Personel_SilenPersonelId",
                        column: x => x.SilenPersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonelAyrilis",
                columns: table => new
                {
                    PersonelAyrilisID = table.Column<short>(type: "SMALLINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonelId = table.Column<short>(type: "SMALLINT", nullable: false),
                    PersonelAyrilisNedenleriId = table.Column<byte>(type: "TINYINT", nullable: false),
                    BaslangicTarihi = table.Column<DateTime>(type: "DATE", nullable: true),
                    BitisTarihi = table.Column<DateTime>(type: "DATE", nullable: true),
                    PersonelAyrilisYeriId = table.Column<short>(type: "SMALLINT", nullable: true),
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
                    table.PrimaryKey("PK_PersonelAyrilis", x => x.PersonelAyrilisID);
                    table.CheckConstraint("CHK_PersonelAyrilis_Tarih", "BaslangicTarihi IS NOT NULL OR BitisTarihi IS NOT NULL");
                    table.ForeignKey(
                        name: "FK_PersonelAyrilis_Durum_DurumId",
                        column: x => x.DurumId,
                        principalTable: "Durum",
                        principalColumn: "DurumID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonelAyrilis_Kurum_PersonelAyrilisYeriId",
                        column: x => x.PersonelAyrilisYeriId,
                        principalTable: "Kurum",
                        principalColumn: "KurumID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonelAyrilis_PersonelAyrilisNedenleri_PersonelAyrilisNedenleriId",
                        column: x => x.PersonelAyrilisNedenleriId,
                        principalTable: "PersonelAyrilisNedenleri",
                        principalColumn: "PersonelAyrilisNedenleriID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonelAyrilis_Personel_GuncelleyenPersonelId",
                        column: x => x.GuncelleyenPersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonelAyrilis_Personel_OlusturanPersonelId",
                        column: x => x.OlusturanPersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonelAyrilis_Personel_PersonelId",
                        column: x => x.PersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonelAyrilis_Personel_SilenPersonelId",
                        column: x => x.SilenPersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonelAyrilis_DurumId",
                table: "PersonelAyrilis",
                column: "DurumId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelAyrilis_GuncelleyenPersonelId",
                table: "PersonelAyrilis",
                column: "GuncelleyenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelAyrilis_OlusturanPersonelId",
                table: "PersonelAyrilis",
                column: "OlusturanPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelAyrilis_PersonelAyrilisNedenleriId",
                table: "PersonelAyrilis",
                column: "PersonelAyrilisNedenleriId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelAyrilis_PersonelAyrilisYeriId",
                table: "PersonelAyrilis",
                column: "PersonelAyrilisYeriId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelAyrilis_PersonelId",
                table: "PersonelAyrilis",
                column: "PersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelAyrilis_SilenPersonelId",
                table: "PersonelAyrilis",
                column: "SilenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelAyrilisNedenleri_DurumId",
                table: "PersonelAyrilisNedenleri",
                column: "DurumId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelAyrilisNedenleri_GuncelleyenPersonelId",
                table: "PersonelAyrilisNedenleri",
                column: "GuncelleyenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelAyrilisNedenleri_OlusturanPersonelId",
                table: "PersonelAyrilisNedenleri",
                column: "OlusturanPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelAyrilisNedenleri_SilenPersonelId",
                table: "PersonelAyrilisNedenleri",
                column: "SilenPersonelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonelAyrilis");

            migrationBuilder.DropTable(
                name: "PersonelAyrilisNedenleri");
        }
    }
}
