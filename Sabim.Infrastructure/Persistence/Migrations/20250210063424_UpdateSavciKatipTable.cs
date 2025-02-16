using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sabim.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSavciKatipTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SavciCalisilanKatip",
                columns: table => new
                {
                    SavciCalisilanKatipID = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SavciId = table.Column<short>(type: "SMALLINT", nullable: false),
                    KatipId = table.Column<short>(type: "SMALLINT", nullable: false),
                    GorevlendirilmeBaslamaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    GorevlendirilmeBitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GorevlendirilmeAktifMi = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
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
                    table.PrimaryKey("PK_SavciCalisilanKatip", x => x.SavciCalisilanKatipID);
                    table.ForeignKey(
                        name: "FK_SavciCalisilanKatip_Durum_DurumId",
                        column: x => x.DurumId,
                        principalTable: "Durum",
                        principalColumn: "DurumID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SavciCalisilanKatip_Personel_GuncelleyenPersonelId",
                        column: x => x.GuncelleyenPersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SavciCalisilanKatip_Personel_KatipId",
                        column: x => x.KatipId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SavciCalisilanKatip_Personel_OlusturanPersonelId",
                        column: x => x.OlusturanPersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SavciCalisilanKatip_Personel_SavciId",
                        column: x => x.SavciId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SavciCalisilanKatip_Personel_SilenPersonelId",
                        column: x => x.SilenPersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SavciCalisilanKatip_DurumId",
                table: "SavciCalisilanKatip",
                column: "DurumId");

            migrationBuilder.CreateIndex(
                name: "IX_SavciCalisilanKatip_GuncelleyenPersonelId",
                table: "SavciCalisilanKatip",
                column: "GuncelleyenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_SavciCalisilanKatip_KatipId",
                table: "SavciCalisilanKatip",
                column: "KatipId");

            migrationBuilder.CreateIndex(
                name: "IX_SavciCalisilanKatip_OlusturanPersonelId",
                table: "SavciCalisilanKatip",
                column: "OlusturanPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_SavciCalisilanKatip_SavciId",
                table: "SavciCalisilanKatip",
                column: "SavciId");

            migrationBuilder.CreateIndex(
                name: "IX_SavciCalisilanKatip_SilenPersonelId",
                table: "SavciCalisilanKatip",
                column: "SilenPersonelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SavciCalisilanKatip");
        }
    }
}
