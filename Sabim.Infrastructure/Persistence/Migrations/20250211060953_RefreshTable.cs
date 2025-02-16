using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sabim.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RefreshTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "PersonelUnvanGecmisi",
               columns: table => new
               {
                   PersonelUnvanGecmisiID = table.Column<short>(type: "SMALLINT", nullable: false)
                       .Annotation("SqlServer:Identity", "1, 1"),
                   PersonelId = table.Column<short>(type: "SMALLINT", nullable: false),
                   UnvanId = table.Column<short>(type: "SMALLINT", nullable: false),
                   UnvanaSahipOlduguTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                   UnvanDegisimTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                   table.PrimaryKey("PK_PersonelUnvanGecmisi", x => x.PersonelUnvanGecmisiID);
                   table.ForeignKey(
                       name: "FK_PersonelUnvanGecmisi_Durum_DurumId",
                       column: x => x.DurumId,
                       principalTable: "Durum",
                       principalColumn: "DurumID",
                       onDelete: ReferentialAction.Restrict);
                   table.ForeignKey(
                       name: "FK_PersonelUnvanGecmisi_Personel_GuncelleyenPersonelId",
                       column: x => x.GuncelleyenPersonelId,
                       principalTable: "Personel",
                       principalColumn: "PersonelID",
                       onDelete: ReferentialAction.Restrict);
                   table.ForeignKey(
                       name: "FK_PersonelUnvanGecmisi_Personel_OlusturanPersonelId",
                       column: x => x.OlusturanPersonelId,
                       principalTable: "Personel",
                       principalColumn: "PersonelID",
                       onDelete: ReferentialAction.Restrict);
                   table.ForeignKey(
                       name: "FK_PersonelUnvanGecmisi_Personel_PersonelId",
                       column: x => x.PersonelId,
                       principalTable: "Personel",
                       principalColumn: "PersonelID",
                       onDelete: ReferentialAction.Restrict);
                   table.ForeignKey(
                       name: "FK_PersonelUnvanGecmisi_Personel_SilenPersonelId",
                       column: x => x.SilenPersonelId,
                       principalTable: "Personel",
                       principalColumn: "PersonelID",
                       onDelete: ReferentialAction.Restrict);
                   table.ForeignKey(
                       name: "FK_PersonelUnvanGecmisi_Unvan_UnvanId",
                       column: x => x.UnvanId,
                       principalTable: "Unvan",
                       principalColumn: "UnvanID",
                       onDelete: ReferentialAction.Restrict);
               });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
            name: "PersonelUnvanGecmisi");
        }
    }
}
