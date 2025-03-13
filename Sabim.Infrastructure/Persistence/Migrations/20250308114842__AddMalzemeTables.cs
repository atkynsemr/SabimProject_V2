using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sabim.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _AddMalzemeTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MalzemeDurumu",
                columns: table => new
                {
                    MalzemeDurumuID = table.Column<byte>(type: "tinyint", nullable: false),
                    MalzemeDurumuAdi = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
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
                    table.PrimaryKey("PK_MalzemeDurumu", x => x.MalzemeDurumuID);
                    table.ForeignKey(
                        name: "FK_MalzemeDurumu_Durum_DurumId",
                        column: x => x.DurumId,
                        principalTable: "Durum",
                        principalColumn: "DurumID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MalzemeDurumu_Personel_GuncelleyenPersonelId",
                        column: x => x.GuncelleyenPersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MalzemeDurumu_Personel_OlusturanPersonelId",
                        column: x => x.OlusturanPersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MalzemeDurumu_Personel_SilenPersonelId",
                        column: x => x.SilenPersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MalzemeMarka",
                columns: table => new
                {
                    MalzemeMarkaID = table.Column<byte>(type: "tinyint", nullable: false),
                    MarkaAdi = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
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
                    table.PrimaryKey("PK_MalzemeMarka", x => x.MalzemeMarkaID);
                    table.ForeignKey(
                        name: "FK_MalzemeMarka_Durum_DurumId",
                        column: x => x.DurumId,
                        principalTable: "Durum",
                        principalColumn: "DurumID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MalzemeMarka_Personel_GuncelleyenPersonelId",
                        column: x => x.GuncelleyenPersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MalzemeMarka_Personel_OlusturanPersonelId",
                        column: x => x.OlusturanPersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MalzemeMarka_Personel_SilenPersonelId",
                        column: x => x.SilenPersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MalzemeTuru",
                columns: table => new
                {
                    MalzemeTuruID = table.Column<byte>(type: "tinyint", nullable: false),
                    TurAdi = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
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
                    table.PrimaryKey("PK_MalzemeTuru", x => x.MalzemeTuruID);
                    table.ForeignKey(
                        name: "FK_MalzemeTuru_Durum_DurumId",
                        column: x => x.DurumId,
                        principalTable: "Durum",
                        principalColumn: "DurumID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MalzemeTuru_Personel_GuncelleyenPersonelId",
                        column: x => x.GuncelleyenPersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MalzemeTuru_Personel_OlusturanPersonelId",
                        column: x => x.OlusturanPersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MalzemeTuru_Personel_SilenPersonelId",
                        column: x => x.SilenPersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MalzemeCinsi",
                columns: table => new
                {
                    MalzemeCinsiID = table.Column<byte>(type: "tinyint", nullable: false),
                    MalzemeCinsiAdi = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    MalzemeTuruId = table.Column<byte>(type: "tinyint", nullable: false),
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
                    table.PrimaryKey("PK_MalzemeCinsi", x => x.MalzemeCinsiID);
                    table.ForeignKey(
                        name: "FK_MalzemeCinsi_Durum_DurumId",
                        column: x => x.DurumId,
                        principalTable: "Durum",
                        principalColumn: "DurumID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MalzemeCinsi_MalzemeTuru_MalzemeTuruId",
                        column: x => x.MalzemeTuruId,
                        principalTable: "MalzemeTuru",
                        principalColumn: "MalzemeTuruID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MalzemeCinsi_Personel_GuncelleyenPersonelId",
                        column: x => x.GuncelleyenPersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MalzemeCinsi_Personel_OlusturanPersonelId",
                        column: x => x.OlusturanPersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MalzemeCinsi_Personel_SilenPersonelId",
                        column: x => x.SilenPersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MalzemeModel",
                columns: table => new
                {
                    MalzemeModelID = table.Column<byte>(type: "tinyint", nullable: false),
                    ModelAdi = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    MalzemeMarkaId = table.Column<byte>(type: "tinyint", nullable: false),
                    MalzemeCinsiId = table.Column<byte>(type: "tinyint", nullable: false),
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
                    table.PrimaryKey("PK_MalzemeModel", x => x.MalzemeModelID);
                    table.ForeignKey(
                        name: "FK_MalzemeModel_Durum_DurumId",
                        column: x => x.DurumId,
                        principalTable: "Durum",
                        principalColumn: "DurumID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MalzemeModel_MalzemeCinsi_MalzemeCinsiId",
                        column: x => x.MalzemeCinsiId,
                        principalTable: "MalzemeCinsi",
                        principalColumn: "MalzemeCinsiID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MalzemeModel_MalzemeMarka_MalzemeMarkaId",
                        column: x => x.MalzemeMarkaId,
                        principalTable: "MalzemeMarka",
                        principalColumn: "MalzemeMarkaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MalzemeModel_Personel_GuncelleyenPersonelId",
                        column: x => x.GuncelleyenPersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MalzemeModel_Personel_OlusturanPersonelId",
                        column: x => x.OlusturanPersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MalzemeModel_Personel_SilenPersonelId",
                        column: x => x.SilenPersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Malzeme",
                columns: table => new
                {
                    MalzemeID = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeriNumarasi = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    MalzemeModelId = table.Column<byte>(type: "tinyint", nullable: false),
                    MalzemeDurumuId = table.Column<byte>(type: "tinyint", nullable: false),
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
                    table.PrimaryKey("PK_Malzeme", x => x.MalzemeID);
                    table.ForeignKey(
                        name: "FK_Malzeme_Durum_DurumId",
                        column: x => x.DurumId,
                        principalTable: "Durum",
                        principalColumn: "DurumID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Malzeme_MalzemeDurumu_MalzemeDurumuId",
                        column: x => x.MalzemeDurumuId,
                        principalTable: "MalzemeDurumu",
                        principalColumn: "MalzemeDurumuID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Malzeme_MalzemeModel_MalzemeModelId",
                        column: x => x.MalzemeModelId,
                        principalTable: "MalzemeModel",
                        principalColumn: "MalzemeModelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Malzeme_Personel_GuncelleyenPersonelId",
                        column: x => x.GuncelleyenPersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Malzeme_Personel_OlusturanPersonelId",
                        column: x => x.OlusturanPersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Malzeme_Personel_SilenPersonelId",
                        column: x => x.SilenPersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonelGeciciGorevlendirilme_GorevlendirilmeTipiId",
                table: "PersonelGeciciGorevlendirilme",
                column: "GorevlendirilmeTipiId");

            migrationBuilder.CreateIndex(
                name: "IX_Malzeme_DurumId",
                table: "Malzeme",
                column: "DurumId");

            migrationBuilder.CreateIndex(
                name: "IX_Malzeme_GuncelleyenPersonelId",
                table: "Malzeme",
                column: "GuncelleyenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_Malzeme_MalzemeDurumuId",
                table: "Malzeme",
                column: "MalzemeDurumuId");

            migrationBuilder.CreateIndex(
                name: "IX_Malzeme_MalzemeModelId",
                table: "Malzeme",
                column: "MalzemeModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Malzeme_OlusturanPersonelId",
                table: "Malzeme",
                column: "OlusturanPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_Malzeme_SilenPersonelId",
                table: "Malzeme",
                column: "SilenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_MalzemeCinsi_DurumId",
                table: "MalzemeCinsi",
                column: "DurumId");

            migrationBuilder.CreateIndex(
                name: "IX_MalzemeCinsi_GuncelleyenPersonelId",
                table: "MalzemeCinsi",
                column: "GuncelleyenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_MalzemeCinsi_MalzemeCinsiAdi",
                table: "MalzemeCinsi",
                column: "MalzemeCinsiAdi",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MalzemeCinsi_MalzemeTuruId",
                table: "MalzemeCinsi",
                column: "MalzemeTuruId");

            migrationBuilder.CreateIndex(
                name: "IX_MalzemeCinsi_OlusturanPersonelId",
                table: "MalzemeCinsi",
                column: "OlusturanPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_MalzemeCinsi_SilenPersonelId",
                table: "MalzemeCinsi",
                column: "SilenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_MalzemeDurumu_DurumId",
                table: "MalzemeDurumu",
                column: "DurumId");

            migrationBuilder.CreateIndex(
                name: "IX_MalzemeDurumu_GuncelleyenPersonelId",
                table: "MalzemeDurumu",
                column: "GuncelleyenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_MalzemeDurumu_MalzemeDurumuAdi",
                table: "MalzemeDurumu",
                column: "MalzemeDurumuAdi",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MalzemeDurumu_OlusturanPersonelId",
                table: "MalzemeDurumu",
                column: "OlusturanPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_MalzemeDurumu_SilenPersonelId",
                table: "MalzemeDurumu",
                column: "SilenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_MalzemeMarka_DurumId",
                table: "MalzemeMarka",
                column: "DurumId");

            migrationBuilder.CreateIndex(
                name: "IX_MalzemeMarka_GuncelleyenPersonelId",
                table: "MalzemeMarka",
                column: "GuncelleyenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_MalzemeMarka_MarkaAdi",
                table: "MalzemeMarka",
                column: "MarkaAdi",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MalzemeMarka_OlusturanPersonelId",
                table: "MalzemeMarka",
                column: "OlusturanPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_MalzemeMarka_SilenPersonelId",
                table: "MalzemeMarka",
                column: "SilenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_MalzemeModel_DurumId",
                table: "MalzemeModel",
                column: "DurumId");

            migrationBuilder.CreateIndex(
                name: "IX_MalzemeModel_GuncelleyenPersonelId",
                table: "MalzemeModel",
                column: "GuncelleyenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_MalzemeModel_MalzemeCinsiId",
                table: "MalzemeModel",
                column: "MalzemeCinsiId");

            migrationBuilder.CreateIndex(
                name: "IX_MalzemeModel_MalzemeMarkaId",
                table: "MalzemeModel",
                column: "MalzemeMarkaId");

            migrationBuilder.CreateIndex(
                name: "IX_MalzemeModel_OlusturanPersonelId",
                table: "MalzemeModel",
                column: "OlusturanPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_MalzemeModel_SilenPersonelId",
                table: "MalzemeModel",
                column: "SilenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_MalzemeTuru_DurumId",
                table: "MalzemeTuru",
                column: "DurumId");

            migrationBuilder.CreateIndex(
                name: "IX_MalzemeTuru_GuncelleyenPersonelId",
                table: "MalzemeTuru",
                column: "GuncelleyenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_MalzemeTuru_OlusturanPersonelId",
                table: "MalzemeTuru",
                column: "OlusturanPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_MalzemeTuru_SilenPersonelId",
                table: "MalzemeTuru",
                column: "SilenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_MalzemeTuru_TurAdi",
                table: "MalzemeTuru",
                column: "TurAdi",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonelGeciciGorevlendirilme_GorevlendirilmeTipi_GorevlendirilmeTipiId",
                table: "PersonelGeciciGorevlendirilme",
                column: "GorevlendirilmeTipiId",
                principalTable: "GorevlendirilmeTipi",
                principalColumn: "GorevlendirilmeTipiID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonelGeciciGorevlendirilme_GorevlendirilmeTipi_GorevlendirilmeTipiId",
                table: "PersonelGeciciGorevlendirilme");

            migrationBuilder.DropTable(
                name: "Malzeme");

            migrationBuilder.DropTable(
                name: "MalzemeDurumu");

            migrationBuilder.DropTable(
                name: "MalzemeModel");

            migrationBuilder.DropTable(
                name: "MalzemeCinsi");

            migrationBuilder.DropTable(
                name: "MalzemeMarka");

            migrationBuilder.DropTable(
                name: "MalzemeTuru");

            migrationBuilder.DropIndex(
                name: "IX_PersonelGeciciGorevlendirilme_GorevlendirilmeTipiId",
                table: "PersonelGeciciGorevlendirilme");
        }
    }
}
