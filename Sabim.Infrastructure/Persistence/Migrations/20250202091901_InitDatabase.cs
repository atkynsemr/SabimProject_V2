using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Sabim.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Durum",
                columns: table => new
                {
                    DurumID = table.Column<short>(type: "SMALLINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DurumAdi = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    AktifMi = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Durum", x => x.DurumID);
                });

            migrationBuilder.CreateTable(
                name: "AppRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OlusturanPersonelId = table.Column<short>(type: "SMALLINT", nullable: true),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    GuncelleyenPersonelId = table.Column<short>(type: "SMALLINT", nullable: true),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    SilenPersonelId = table.Column<short>(type: "SMALLINT", nullable: true),
                    SilinmeTarihi = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    DurumId = table.Column<short>(type: "SMALLINT", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NormalizedName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppRole_Durum_DurumId",
                        column: x => x.DurumId,
                        principalTable: "Durum",
                        principalColumn: "DurumID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonelId = table.Column<short>(type: "SMALLINT", nullable: false),
                    ProfilResmiYolu = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    SonOturumAcmaZamani = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OlusturanPersonelId = table.Column<short>(type: "SMALLINT", nullable: true),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GuncelleyenPersonelId = table.Column<short>(type: "SMALLINT", nullable: true),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SilenPersonelId = table.Column<short>(type: "SMALLINT", nullable: true),
                    SilinmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DurumId = table.Column<short>(type: "SMALLINT", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUser_Durum_DurumId",
                        column: x => x.DurumId,
                        principalTable: "Durum",
                        principalColumn: "DurumID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AppUser_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AppUser_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AppRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AppRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AppUser_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AppUser_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EkranId = table.Column<short>(type: "SMALLINT", nullable: false),
                    OlusturanPersonelId = table.Column<short>(type: "SMALLINT", nullable: true),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    GuncelleyenPersonelId = table.Column<short>(type: "SMALLINT", nullable: true),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    SilenPersonelId = table.Column<short>(type: "SMALLINT", nullable: true),
                    SilinmeTarihi = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    DurumId = table.Column<short>(type: "SMALLINT", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AppRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AppRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_Durum_DurumId",
                        column: x => x.DurumId,
                        principalTable: "Durum",
                        principalColumn: "DurumID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Birim",
                columns: table => new
                {
                    BirimID = table.Column<short>(type: "SMALLINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BirimAdi = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    BolumId = table.Column<short>(type: "SMALLINT", nullable: false),
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
                    table.PrimaryKey("PK_Birim", x => x.BirimID);
                    table.ForeignKey(
                        name: "FK_Birim_Durum_DurumId",
                        column: x => x.DurumId,
                        principalTable: "Durum",
                        principalColumn: "DurumID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bolum",
                columns: table => new
                {
                    BolumID = table.Column<short>(type: "SMALLINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BolumAdi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
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
                    table.PrimaryKey("PK_Bolum", x => x.BolumID);
                    table.ForeignKey(
                        name: "FK_Bolum_Durum_DurumId",
                        column: x => x.DurumId,
                        principalTable: "Durum",
                        principalColumn: "DurumID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CalismaDurumu",
                columns: table => new
                {
                    CalismaDurumuID = table.Column<short>(type: "SMALLINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CalismaDurumAdi = table.Column<string>(type: "nvarchar(85)", maxLength: 85, nullable: false),
                    KurumPersonelListesineDahilMi = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
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
                    table.PrimaryKey("PK_CalismaDurumu", x => x.CalismaDurumuID);
                    table.ForeignKey(
                        name: "FK_CalismaDurumu_Durum_DurumId",
                        column: x => x.DurumId,
                        principalTable: "Durum",
                        principalColumn: "DurumID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cinsiyet",
                columns: table => new
                {
                    CinsiyetID = table.Column<short>(type: "SMALLINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CinsiyetAdi = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
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
                    table.PrimaryKey("PK_Cinsiyet", x => x.CinsiyetID);
                    table.ForeignKey(
                        name: "FK_Cinsiyet_Durum_DurumId",
                        column: x => x.DurumId,
                        principalTable: "Durum",
                        principalColumn: "DurumID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ekran",
                columns: table => new
                {
                    EkranID = table.Column<short>(type: "SMALLINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SidebarMenuId = table.Column<short>(type: "SMALLINT", nullable: false),
                    EkranAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Varsayilan = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
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
                    table.PrimaryKey("PK_Ekran", x => x.EkranID);
                    table.ForeignKey(
                        name: "FK_Ekran_Durum_DurumId",
                        column: x => x.DurumId,
                        principalTable: "Durum",
                        principalColumn: "DurumID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GorevlendirilmeTipi",
                columns: table => new
                {
                    GorevlendirilmeTipiID = table.Column<short>(type: "SMALLINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GorevlendirilmeTipiAciklama = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
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
                    table.PrimaryKey("PK_GorevlendirilmeTipi", x => x.GorevlendirilmeTipiID);
                    table.ForeignKey(
                        name: "FK_GorevlendirilmeTipi_Durum_DurumId",
                        column: x => x.DurumId,
                        principalTable: "Durum",
                        principalColumn: "DurumID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GorevlendirilmeTuru",
                columns: table => new
                {
                    GorevlendirilmeTuruID = table.Column<short>(type: "SMALLINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GorevlendirilmeTuruAdi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    KurumPersonelListesineDahilMi = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
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
                    table.PrimaryKey("PK_GorevlendirilmeTuru", x => x.GorevlendirilmeTuruID);
                    table.ForeignKey(
                        name: "FK_GorevlendirilmeTuru_Durum_DurumId",
                        column: x => x.DurumId,
                        principalTable: "Durum",
                        principalColumn: "DurumID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KabinetBazliBolum",
                columns: table => new
                {
                    KabinetBazliBolumID = table.Column<short>(type: "SMALLINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KabinetBazliBolumAdi = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
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
                    table.PrimaryKey("PK_KabinetBazliBolum", x => x.KabinetBazliBolumID);
                    table.ForeignKey(
                        name: "FK_KabinetBazliBolum_Durum_DurumId",
                        column: x => x.DurumId,
                        principalTable: "Durum",
                        principalColumn: "DurumID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KadroTuru",
                columns: table => new
                {
                    KadroTuruID = table.Column<short>(type: "SMALLINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KadroTuruAdi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
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
                    table.PrimaryKey("PK_KadroTuru", x => x.KadroTuruID);
                    table.ForeignKey(
                        name: "FK_KadroTuru_Durum_DurumId",
                        column: x => x.DurumId,
                        principalTable: "Durum",
                        principalColumn: "DurumID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KanGrubu",
                columns: table => new
                {
                    KanGrubuID = table.Column<short>(type: "SMALLINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KanGrubuAdi = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
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
                    table.PrimaryKey("PK_KanGrubu", x => x.KanGrubuID);
                    table.ForeignKey(
                        name: "FK_KanGrubu_Durum_DurumId",
                        column: x => x.DurumId,
                        principalTable: "Durum",
                        principalColumn: "DurumID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Kisim",
                columns: table => new
                {
                    KisimID = table.Column<short>(type: "SMALLINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KisimAdi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BirimId = table.Column<short>(type: "SMALLINT", nullable: false),
                    KabinetBazliBolumId = table.Column<short>(type: "SMALLINT", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    DahiliTelefon = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
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
                    table.PrimaryKey("PK_Kisim", x => x.KisimID);
                    table.ForeignKey(
                        name: "FK_Kisim_Birim_BirimId",
                        column: x => x.BirimId,
                        principalTable: "Birim",
                        principalColumn: "BirimID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Kisim_Durum_DurumId",
                        column: x => x.DurumId,
                        principalTable: "Durum",
                        principalColumn: "DurumID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Kisim_KabinetBazliBolum_KabinetBazliBolumId",
                        column: x => x.KabinetBazliBolumId,
                        principalTable: "KabinetBazliBolum",
                        principalColumn: "KabinetBazliBolumID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Kurum",
                columns: table => new
                {
                    KurumID = table.Column<short>(type: "SMALLINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KurumAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SehirId = table.Column<short>(type: "SMALLINT", nullable: false),
                    KurumTipiId = table.Column<short>(type: "SMALLINT", nullable: false),
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
                    table.PrimaryKey("PK_Kurum", x => x.KurumID);
                    table.ForeignKey(
                        name: "FK_Kurum_Durum_DurumId",
                        column: x => x.DurumId,
                        principalTable: "Durum",
                        principalColumn: "DurumID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KurumTipi",
                columns: table => new
                {
                    KurumTipiID = table.Column<short>(type: "SMALLINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KurumTipiAdi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
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
                    table.PrimaryKey("PK_KurumTipi", x => x.KurumTipiID);
                    table.ForeignKey(
                        name: "FK_KurumTipi_Durum_DurumId",
                        column: x => x.DurumId,
                        principalTable: "Durum",
                        principalColumn: "DurumID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Personel",
                columns: table => new
                {
                    PersonelID = table.Column<short>(type: "SMALLINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    SicilNumarasi = table.Column<int>(type: "int", nullable: false),
                    CepTelefonu = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: true),
                    CinsiyetId = table.Column<short>(type: "SMALLINT", nullable: false),
                    KanGrubuId = table.Column<short>(type: "SMALLINT", nullable: false),
                    DogumTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DogumYeri = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: true),
                    Derece = table.Column<short>(type: "SMALLINT", nullable: true),
                    Kademe = table.Column<short>(type: "SMALLINT", nullable: true),
                    MeslegeGirisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BuradaGoreveBaslamaTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KimlikNo = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: true),
                    BuradanAyrilmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BirinciSinifaAyrilmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AracPlakasi = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: true),
                    UnvanId = table.Column<short>(type: "SMALLINT", nullable: false),
                    GorevlendirilmeTuruId = table.Column<short>(type: "SMALLINT", nullable: false),
                    KurumId = table.Column<short>(type: "SMALLINT", nullable: false),
                    KadroTuruId = table.Column<short>(type: "SMALLINT", nullable: false),
                    CalismaDurumuId = table.Column<short>(type: "SMALLINT", nullable: false),
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
                    table.PrimaryKey("PK_Personel", x => x.PersonelID);
                    table.CheckConstraint("CK_Personel_Derece", "[Derece] BETWEEN 1 AND 15");
                    table.CheckConstraint("CK_Personel_Kademe", "[Kademe] BETWEEN 1 AND 4");
                    table.CheckConstraint("CK_Personel_SicilNumarasi", "[SicilNumarasi] BETWEEN 10000 AND 9999999");
                    table.ForeignKey(
                        name: "FK_Personel_CalismaDurumu_CalismaDurumuId",
                        column: x => x.CalismaDurumuId,
                        principalTable: "CalismaDurumu",
                        principalColumn: "CalismaDurumuID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personel_Cinsiyet_CinsiyetId",
                        column: x => x.CinsiyetId,
                        principalTable: "Cinsiyet",
                        principalColumn: "CinsiyetID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personel_Durum_DurumId",
                        column: x => x.DurumId,
                        principalTable: "Durum",
                        principalColumn: "DurumID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personel_GorevlendirilmeTuru_GorevlendirilmeTuruId",
                        column: x => x.GorevlendirilmeTuruId,
                        principalTable: "GorevlendirilmeTuru",
                        principalColumn: "GorevlendirilmeTuruID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personel_KadroTuru_KadroTuruId",
                        column: x => x.KadroTuruId,
                        principalTable: "KadroTuru",
                        principalColumn: "KadroTuruID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personel_KanGrubu_KanGrubuId",
                        column: x => x.KanGrubuId,
                        principalTable: "KanGrubu",
                        principalColumn: "KanGrubuID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personel_Kurum_KurumId",
                        column: x => x.KurumId,
                        principalTable: "Kurum",
                        principalColumn: "KurumID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personel_Personel_GuncelleyenPersonelId",
                        column: x => x.GuncelleyenPersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personel_Personel_OlusturanPersonelId",
                        column: x => x.OlusturanPersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personel_Personel_SilenPersonelId",
                        column: x => x.SilenPersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonelGorevlendirilme",
                columns: table => new
                {
                    PersonelGorevlendirilmeID = table.Column<short>(type: "SMALLINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonelId = table.Column<short>(type: "SMALLINT", nullable: false),
                    KisimId = table.Column<short>(type: "SMALLINT", nullable: false),
                    AsilGorevlendirilmeYeriMi = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    GorevlendirilmeTipiId = table.Column<short>(type: "SMALLINT", nullable: false),
                    GorevlendirilmeAktifMi = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    GorevlendirilmeBaslangicTarihi = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    GorevlendirilmeBitisTarihi = table.Column<DateTime>(type: "DATETIME", nullable: true),
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
                    table.PrimaryKey("PK_PersonelGorevlendirilme", x => x.PersonelGorevlendirilmeID);
                    table.ForeignKey(
                        name: "FK_PersonelGorevlendirilme_Durum_DurumId",
                        column: x => x.DurumId,
                        principalTable: "Durum",
                        principalColumn: "DurumID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonelGorevlendirilme_GorevlendirilmeTipi_GorevlendirilmeTipiId",
                        column: x => x.GorevlendirilmeTipiId,
                        principalTable: "GorevlendirilmeTipi",
                        principalColumn: "GorevlendirilmeTipiID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonelGorevlendirilme_Kisim_KisimId",
                        column: x => x.KisimId,
                        principalTable: "Kisim",
                        principalColumn: "KisimID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonelGorevlendirilme_Personel_GuncelleyenPersonelId",
                        column: x => x.GuncelleyenPersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonelGorevlendirilme_Personel_OlusturanPersonelId",
                        column: x => x.OlusturanPersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonelGorevlendirilme_Personel_PersonelId",
                        column: x => x.PersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonelGorevlendirilme_Personel_SilenPersonelId",
                        column: x => x.SilenPersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sehir",
                columns: table => new
                {
                    SehirID = table.Column<short>(type: "SMALLINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SehirAdi = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SehirKodu = table.Column<short>(type: "SMALLINT", nullable: false),
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
                    table.PrimaryKey("PK_Sehir", x => x.SehirID);
                    table.ForeignKey(
                        name: "FK_Sehir_Durum_DurumId",
                        column: x => x.DurumId,
                        principalTable: "Durum",
                        principalColumn: "DurumID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sehir_Personel_GuncelleyenPersonelId",
                        column: x => x.GuncelleyenPersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sehir_Personel_OlusturanPersonelId",
                        column: x => x.OlusturanPersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sehir_Personel_SilenPersonelId",
                        column: x => x.SilenPersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SidebarMenu",
                columns: table => new
                {
                    SidebarMenuID = table.Column<short>(type: "SMALLINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SidebarMenuAdi = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
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
                    table.PrimaryKey("PK_SidebarMenu", x => x.SidebarMenuID);
                    table.ForeignKey(
                        name: "FK_SidebarMenu_Durum_DurumId",
                        column: x => x.DurumId,
                        principalTable: "Durum",
                        principalColumn: "DurumID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SidebarMenu_Personel_GuncelleyenPersonelId",
                        column: x => x.GuncelleyenPersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SidebarMenu_Personel_OlusturanPersonelId",
                        column: x => x.OlusturanPersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SidebarMenu_Personel_SilenPersonelId",
                        column: x => x.SilenPersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Unvan",
                columns: table => new
                {
                    UnvanID = table.Column<short>(type: "SMALLINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnvanAdi = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    OncelikSirasi = table.Column<short>(type: "SMALLINT", nullable: false),
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
                    table.PrimaryKey("PK_Unvan", x => x.UnvanID);
                    table.ForeignKey(
                        name: "FK_Unvan_Durum_DurumId",
                        column: x => x.DurumId,
                        principalTable: "Durum",
                        principalColumn: "DurumID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Unvan_Personel_GuncelleyenPersonelId",
                        column: x => x.GuncelleyenPersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Unvan_Personel_OlusturanPersonelId",
                        column: x => x.OlusturanPersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Unvan_Personel_SilenPersonelId",
                        column: x => x.SilenPersonelId,
                        principalTable: "Personel",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.InsertData(
                table: "Durum",
                columns: new[] { "DurumID", "AktifMi", "DurumAdi" },
                values: new object[,]
                {
                    { (short)1, true, "Aktif" },
                    { (short)2, true, "Pasif" },
                    { (short)3, true, "Silinmiş" },
                    { (short)4, true, "Dondurulmuş" },
                    { (short)5, true, "Kapatılmış" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppRole_DurumId",
                table: "AppRole",
                column: "DurumId");

            migrationBuilder.CreateIndex(
                name: "IX_AppRole_GuncelleyenPersonelId",
                table: "AppRole",
                column: "GuncelleyenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_AppRole_Name",
                table: "AppRole",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppRole_OlusturanPersonelId",
                table: "AppRole",
                column: "OlusturanPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_AppRole_SilenPersonelId",
                table: "AppRole",
                column: "SilenPersonelId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AppRole",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AppUser",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AppUser_DurumId",
                table: "AppUser",
                column: "DurumId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUser_GuncelleyenPersonelId",
                table: "AppUser",
                column: "GuncelleyenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUser_OlusturanPersonelId",
                table: "AppUser",
                column: "OlusturanPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUser_PersonelId",
                table: "AppUser",
                column: "PersonelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUser_SilenPersonelId",
                table: "AppUser",
                column: "SilenPersonelId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AppUser",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_DurumId",
                table: "AspNetRoleClaims",
                column: "DurumId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_EkranId",
                table: "AspNetRoleClaims",
                column: "EkranId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_GuncelleyenPersonelId",
                table: "AspNetRoleClaims",
                column: "GuncelleyenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_OlusturanPersonelId",
                table: "AspNetRoleClaims",
                column: "OlusturanPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_SilenPersonelId",
                table: "AspNetRoleClaims",
                column: "SilenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Birim_BirimAdi",
                table: "Birim",
                column: "BirimAdi",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Birim_BolumId",
                table: "Birim",
                column: "BolumId");

            migrationBuilder.CreateIndex(
                name: "IX_Birim_DurumId",
                table: "Birim",
                column: "DurumId");

            migrationBuilder.CreateIndex(
                name: "IX_Birim_GuncelleyenPersonelId",
                table: "Birim",
                column: "GuncelleyenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_Birim_OlusturanPersonelId",
                table: "Birim",
                column: "OlusturanPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_Birim_SilenPersonelId",
                table: "Birim",
                column: "SilenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_Bolum_BolumAdi",
                table: "Bolum",
                column: "BolumAdi",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bolum_DurumId",
                table: "Bolum",
                column: "DurumId");

            migrationBuilder.CreateIndex(
                name: "IX_Bolum_GuncelleyenPersonelId",
                table: "Bolum",
                column: "GuncelleyenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_Bolum_OlusturanPersonelId",
                table: "Bolum",
                column: "OlusturanPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_Bolum_SilenPersonelId",
                table: "Bolum",
                column: "SilenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_CalismaDurumu_CalismaDurumAdi",
                table: "CalismaDurumu",
                column: "CalismaDurumAdi",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CalismaDurumu_DurumId",
                table: "CalismaDurumu",
                column: "DurumId");

            migrationBuilder.CreateIndex(
                name: "IX_CalismaDurumu_GuncelleyenPersonelId",
                table: "CalismaDurumu",
                column: "GuncelleyenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_CalismaDurumu_OlusturanPersonelId",
                table: "CalismaDurumu",
                column: "OlusturanPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_CalismaDurumu_SilenPersonelId",
                table: "CalismaDurumu",
                column: "SilenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_Cinsiyet_DurumId",
                table: "Cinsiyet",
                column: "DurumId");

            migrationBuilder.CreateIndex(
                name: "IX_Cinsiyet_GuncelleyenPersonelId",
                table: "Cinsiyet",
                column: "GuncelleyenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_Cinsiyet_OlusturanPersonelId",
                table: "Cinsiyet",
                column: "OlusturanPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_Cinsiyet_SilenPersonelId",
                table: "Cinsiyet",
                column: "SilenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_Durum_DurumAdi",
                table: "Durum",
                column: "DurumAdi",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ekran_DurumId",
                table: "Ekran",
                column: "DurumId");

            migrationBuilder.CreateIndex(
                name: "IX_Ekran_EkranAdi",
                table: "Ekran",
                column: "EkranAdi",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ekran_GuncelleyenPersonelId",
                table: "Ekran",
                column: "GuncelleyenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_Ekran_OlusturanPersonelId",
                table: "Ekran",
                column: "OlusturanPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_Ekran_SidebarMenuId",
                table: "Ekran",
                column: "SidebarMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Ekran_SilenPersonelId",
                table: "Ekran",
                column: "SilenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_GorevlendirilmeTipi_DurumId",
                table: "GorevlendirilmeTipi",
                column: "DurumId");

            migrationBuilder.CreateIndex(
                name: "IX_GorevlendirilmeTipi_GorevlendirilmeTipiAciklama",
                table: "GorevlendirilmeTipi",
                column: "GorevlendirilmeTipiAciklama",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GorevlendirilmeTipi_GuncelleyenPersonelId",
                table: "GorevlendirilmeTipi",
                column: "GuncelleyenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_GorevlendirilmeTipi_OlusturanPersonelId",
                table: "GorevlendirilmeTipi",
                column: "OlusturanPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_GorevlendirilmeTipi_SilenPersonelId",
                table: "GorevlendirilmeTipi",
                column: "SilenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_GorevlendirilmeTuru_DurumId",
                table: "GorevlendirilmeTuru",
                column: "DurumId");

            migrationBuilder.CreateIndex(
                name: "IX_GorevlendirilmeTuru_GorevlendirilmeTuruAdi",
                table: "GorevlendirilmeTuru",
                column: "GorevlendirilmeTuruAdi",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GorevlendirilmeTuru_GuncelleyenPersonelId",
                table: "GorevlendirilmeTuru",
                column: "GuncelleyenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_GorevlendirilmeTuru_OlusturanPersonelId",
                table: "GorevlendirilmeTuru",
                column: "OlusturanPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_GorevlendirilmeTuru_SilenPersonelId",
                table: "GorevlendirilmeTuru",
                column: "SilenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_KabinetBazliBolum_DurumId",
                table: "KabinetBazliBolum",
                column: "DurumId");

            migrationBuilder.CreateIndex(
                name: "IX_KabinetBazliBolum_GuncelleyenPersonelId",
                table: "KabinetBazliBolum",
                column: "GuncelleyenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_KabinetBazliBolum_KabinetBazliBolumAdi",
                table: "KabinetBazliBolum",
                column: "KabinetBazliBolumAdi",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KabinetBazliBolum_OlusturanPersonelId",
                table: "KabinetBazliBolum",
                column: "OlusturanPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_KabinetBazliBolum_SilenPersonelId",
                table: "KabinetBazliBolum",
                column: "SilenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_KadroTuru_DurumId",
                table: "KadroTuru",
                column: "DurumId");

            migrationBuilder.CreateIndex(
                name: "IX_KadroTuru_GuncelleyenPersonelId",
                table: "KadroTuru",
                column: "GuncelleyenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_KadroTuru_KadroTuruAdi",
                table: "KadroTuru",
                column: "KadroTuruAdi",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KadroTuru_OlusturanPersonelId",
                table: "KadroTuru",
                column: "OlusturanPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_KadroTuru_SilenPersonelId",
                table: "KadroTuru",
                column: "SilenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_KanGrubu_DurumId",
                table: "KanGrubu",
                column: "DurumId");

            migrationBuilder.CreateIndex(
                name: "IX_KanGrubu_GuncelleyenPersonelId",
                table: "KanGrubu",
                column: "GuncelleyenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_KanGrubu_OlusturanPersonelId",
                table: "KanGrubu",
                column: "OlusturanPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_KanGrubu_SilenPersonelId",
                table: "KanGrubu",
                column: "SilenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_Kisim_BirimId",
                table: "Kisim",
                column: "BirimId");

            migrationBuilder.CreateIndex(
                name: "IX_Kisim_DurumId",
                table: "Kisim",
                column: "DurumId");

            migrationBuilder.CreateIndex(
                name: "IX_Kisim_GuncelleyenPersonelId",
                table: "Kisim",
                column: "GuncelleyenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_Kisim_KabinetBazliBolumId",
                table: "Kisim",
                column: "KabinetBazliBolumId");

            migrationBuilder.CreateIndex(
                name: "IX_Kisim_OlusturanPersonelId",
                table: "Kisim",
                column: "OlusturanPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_Kisim_SilenPersonelId",
                table: "Kisim",
                column: "SilenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_Kurum_DurumId",
                table: "Kurum",
                column: "DurumId");

            migrationBuilder.CreateIndex(
                name: "IX_Kurum_GuncelleyenPersonelId",
                table: "Kurum",
                column: "GuncelleyenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_Kurum_KurumAdi_SehirId",
                table: "Kurum",
                columns: new[] { "KurumAdi", "SehirId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Kurum_KurumTipiId",
                table: "Kurum",
                column: "KurumTipiId");

            migrationBuilder.CreateIndex(
                name: "IX_Kurum_OlusturanPersonelId",
                table: "Kurum",
                column: "OlusturanPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_Kurum_SehirId",
                table: "Kurum",
                column: "SehirId");

            migrationBuilder.CreateIndex(
                name: "IX_Kurum_SilenPersonelId",
                table: "Kurum",
                column: "SilenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_KurumTipi_DurumId",
                table: "KurumTipi",
                column: "DurumId");

            migrationBuilder.CreateIndex(
                name: "IX_KurumTipi_GuncelleyenPersonelId",
                table: "KurumTipi",
                column: "GuncelleyenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_KurumTipi_KurumTipiAdi",
                table: "KurumTipi",
                column: "KurumTipiAdi",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KurumTipi_OlusturanPersonelId",
                table: "KurumTipi",
                column: "OlusturanPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_KurumTipi_SilenPersonelId",
                table: "KurumTipi",
                column: "SilenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_Personel_CalismaDurumuId",
                table: "Personel",
                column: "CalismaDurumuId");

            migrationBuilder.CreateIndex(
                name: "IX_Personel_CinsiyetId",
                table: "Personel",
                column: "CinsiyetId");

            migrationBuilder.CreateIndex(
                name: "IX_Personel_DurumId",
                table: "Personel",
                column: "DurumId");

            migrationBuilder.CreateIndex(
                name: "IX_Personel_GorevlendirilmeTuruId",
                table: "Personel",
                column: "GorevlendirilmeTuruId");

            migrationBuilder.CreateIndex(
                name: "IX_Personel_GuncelleyenPersonelId",
                table: "Personel",
                column: "GuncelleyenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_Personel_KadroTuruId",
                table: "Personel",
                column: "KadroTuruId");

            migrationBuilder.CreateIndex(
                name: "IX_Personel_KanGrubuId",
                table: "Personel",
                column: "KanGrubuId");

            migrationBuilder.CreateIndex(
                name: "IX_Personel_KurumId",
                table: "Personel",
                column: "KurumId");

            migrationBuilder.CreateIndex(
                name: "IX_Personel_OlusturanPersonelId",
                table: "Personel",
                column: "OlusturanPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_Personel_SilenPersonelId",
                table: "Personel",
                column: "SilenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_Personel_UnvanId",
                table: "Personel",
                column: "UnvanId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelGorevlendirilme_DurumId",
                table: "PersonelGorevlendirilme",
                column: "DurumId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelGorevlendirilme_GorevlendirilmeTipiId",
                table: "PersonelGorevlendirilme",
                column: "GorevlendirilmeTipiId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelGorevlendirilme_GuncelleyenPersonelId",
                table: "PersonelGorevlendirilme",
                column: "GuncelleyenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelGorevlendirilme_KisimId",
                table: "PersonelGorevlendirilme",
                column: "KisimId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelGorevlendirilme_OlusturanPersonelId",
                table: "PersonelGorevlendirilme",
                column: "OlusturanPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelGorevlendirilme_PersonelId",
                table: "PersonelGorevlendirilme",
                column: "PersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelGorevlendirilme_SilenPersonelId",
                table: "PersonelGorevlendirilme",
                column: "SilenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelUnvanGecmisi_DurumId",
                table: "PersonelUnvanGecmisi",
                column: "DurumId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelUnvanGecmisi_GuncelleyenPersonelId",
                table: "PersonelUnvanGecmisi",
                column: "GuncelleyenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelUnvanGecmisi_OlusturanPersonelId",
                table: "PersonelUnvanGecmisi",
                column: "OlusturanPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelUnvanGecmisi_PersonelId",
                table: "PersonelUnvanGecmisi",
                column: "PersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelUnvanGecmisi_SilenPersonelId",
                table: "PersonelUnvanGecmisi",
                column: "SilenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelUnvanGecmisi_UnvanId",
                table: "PersonelUnvanGecmisi",
                column: "UnvanId");

            migrationBuilder.CreateIndex(
                name: "IX_Sehir_DurumId",
                table: "Sehir",
                column: "DurumId");

            migrationBuilder.CreateIndex(
                name: "IX_Sehir_GuncelleyenPersonelId",
                table: "Sehir",
                column: "GuncelleyenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_Sehir_OlusturanPersonelId",
                table: "Sehir",
                column: "OlusturanPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_Sehir_SehirAdi",
                table: "Sehir",
                column: "SehirAdi",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sehir_SehirKodu",
                table: "Sehir",
                column: "SehirKodu",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sehir_SilenPersonelId",
                table: "Sehir",
                column: "SilenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_SidebarMenu_DurumId",
                table: "SidebarMenu",
                column: "DurumId");

            migrationBuilder.CreateIndex(
                name: "IX_SidebarMenu_GuncelleyenPersonelId",
                table: "SidebarMenu",
                column: "GuncelleyenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_SidebarMenu_OlusturanPersonelId",
                table: "SidebarMenu",
                column: "OlusturanPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_SidebarMenu_SidebarMenuAdi",
                table: "SidebarMenu",
                column: "SidebarMenuAdi",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SidebarMenu_SilenPersonelId",
                table: "SidebarMenu",
                column: "SilenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_Unvan_DurumId",
                table: "Unvan",
                column: "DurumId");

            migrationBuilder.CreateIndex(
                name: "IX_Unvan_GuncelleyenPersonelId",
                table: "Unvan",
                column: "GuncelleyenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_Unvan_OlusturanPersonelId",
                table: "Unvan",
                column: "OlusturanPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_Unvan_SilenPersonelId",
                table: "Unvan",
                column: "SilenPersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_Unvan_UnvanAdi",
                table: "Unvan",
                column: "UnvanAdi",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AppRole_Personel_GuncelleyenPersonelId",
                table: "AppRole",
                column: "GuncelleyenPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppRole_Personel_OlusturanPersonelId",
                table: "AppRole",
                column: "OlusturanPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppRole_Personel_SilenPersonelId",
                table: "AppRole",
                column: "SilenPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUser_Personel_GuncelleyenPersonelId",
                table: "AppUser",
                column: "GuncelleyenPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUser_Personel_OlusturanPersonelId",
                table: "AppUser",
                column: "OlusturanPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUser_Personel_PersonelId",
                table: "AppUser",
                column: "PersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUser_Personel_SilenPersonelId",
                table: "AppUser",
                column: "SilenPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_Ekran_EkranId",
                table: "AspNetRoleClaims",
                column: "EkranId",
                principalTable: "Ekran",
                principalColumn: "EkranID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_Personel_GuncelleyenPersonelId",
                table: "AspNetRoleClaims",
                column: "GuncelleyenPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_Personel_OlusturanPersonelId",
                table: "AspNetRoleClaims",
                column: "OlusturanPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_Personel_SilenPersonelId",
                table: "AspNetRoleClaims",
                column: "SilenPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Birim_Bolum_BolumId",
                table: "Birim",
                column: "BolumId",
                principalTable: "Bolum",
                principalColumn: "BolumID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Birim_Personel_GuncelleyenPersonelId",
                table: "Birim",
                column: "GuncelleyenPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Birim_Personel_OlusturanPersonelId",
                table: "Birim",
                column: "OlusturanPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Birim_Personel_SilenPersonelId",
                table: "Birim",
                column: "SilenPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bolum_Personel_GuncelleyenPersonelId",
                table: "Bolum",
                column: "GuncelleyenPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bolum_Personel_OlusturanPersonelId",
                table: "Bolum",
                column: "OlusturanPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bolum_Personel_SilenPersonelId",
                table: "Bolum",
                column: "SilenPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CalismaDurumu_Personel_GuncelleyenPersonelId",
                table: "CalismaDurumu",
                column: "GuncelleyenPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CalismaDurumu_Personel_OlusturanPersonelId",
                table: "CalismaDurumu",
                column: "OlusturanPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CalismaDurumu_Personel_SilenPersonelId",
                table: "CalismaDurumu",
                column: "SilenPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cinsiyet_Personel_GuncelleyenPersonelId",
                table: "Cinsiyet",
                column: "GuncelleyenPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cinsiyet_Personel_OlusturanPersonelId",
                table: "Cinsiyet",
                column: "OlusturanPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cinsiyet_Personel_SilenPersonelId",
                table: "Cinsiyet",
                column: "SilenPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ekran_Personel_GuncelleyenPersonelId",
                table: "Ekran",
                column: "GuncelleyenPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ekran_Personel_OlusturanPersonelId",
                table: "Ekran",
                column: "OlusturanPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ekran_Personel_SilenPersonelId",
                table: "Ekran",
                column: "SilenPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ekran_SidebarMenu_SidebarMenuId",
                table: "Ekran",
                column: "SidebarMenuId",
                principalTable: "SidebarMenu",
                principalColumn: "SidebarMenuID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GorevlendirilmeTipi_Personel_GuncelleyenPersonelId",
                table: "GorevlendirilmeTipi",
                column: "GuncelleyenPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GorevlendirilmeTipi_Personel_OlusturanPersonelId",
                table: "GorevlendirilmeTipi",
                column: "OlusturanPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GorevlendirilmeTipi_Personel_SilenPersonelId",
                table: "GorevlendirilmeTipi",
                column: "SilenPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GorevlendirilmeTuru_Personel_GuncelleyenPersonelId",
                table: "GorevlendirilmeTuru",
                column: "GuncelleyenPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GorevlendirilmeTuru_Personel_OlusturanPersonelId",
                table: "GorevlendirilmeTuru",
                column: "OlusturanPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GorevlendirilmeTuru_Personel_SilenPersonelId",
                table: "GorevlendirilmeTuru",
                column: "SilenPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KabinetBazliBolum_Personel_GuncelleyenPersonelId",
                table: "KabinetBazliBolum",
                column: "GuncelleyenPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KabinetBazliBolum_Personel_OlusturanPersonelId",
                table: "KabinetBazliBolum",
                column: "OlusturanPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KabinetBazliBolum_Personel_SilenPersonelId",
                table: "KabinetBazliBolum",
                column: "SilenPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KadroTuru_Personel_GuncelleyenPersonelId",
                table: "KadroTuru",
                column: "GuncelleyenPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KadroTuru_Personel_OlusturanPersonelId",
                table: "KadroTuru",
                column: "OlusturanPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KadroTuru_Personel_SilenPersonelId",
                table: "KadroTuru",
                column: "SilenPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KanGrubu_Personel_GuncelleyenPersonelId",
                table: "KanGrubu",
                column: "GuncelleyenPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KanGrubu_Personel_OlusturanPersonelId",
                table: "KanGrubu",
                column: "OlusturanPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KanGrubu_Personel_SilenPersonelId",
                table: "KanGrubu",
                column: "SilenPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Kisim_Personel_GuncelleyenPersonelId",
                table: "Kisim",
                column: "GuncelleyenPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Kisim_Personel_OlusturanPersonelId",
                table: "Kisim",
                column: "OlusturanPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Kisim_Personel_SilenPersonelId",
                table: "Kisim",
                column: "SilenPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Kurum_KurumTipi_KurumTipiId",
                table: "Kurum",
                column: "KurumTipiId",
                principalTable: "KurumTipi",
                principalColumn: "KurumTipiID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Kurum_Personel_GuncelleyenPersonelId",
                table: "Kurum",
                column: "GuncelleyenPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Kurum_Personel_OlusturanPersonelId",
                table: "Kurum",
                column: "OlusturanPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Kurum_Personel_SilenPersonelId",
                table: "Kurum",
                column: "SilenPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Kurum_Sehir_SehirId",
                table: "Kurum",
                column: "SehirId",
                principalTable: "Sehir",
                principalColumn: "SehirID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KurumTipi_Personel_GuncelleyenPersonelId",
                table: "KurumTipi",
                column: "GuncelleyenPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KurumTipi_Personel_OlusturanPersonelId",
                table: "KurumTipi",
                column: "OlusturanPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KurumTipi_Personel_SilenPersonelId",
                table: "KurumTipi",
                column: "SilenPersonelId",
                principalTable: "Personel",
                principalColumn: "PersonelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Personel_Unvan_UnvanId",
                table: "Personel",
                column: "UnvanId",
                principalTable: "Unvan",
                principalColumn: "UnvanID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalismaDurumu_Durum_DurumId",
                table: "CalismaDurumu");

            migrationBuilder.DropForeignKey(
                name: "FK_Cinsiyet_Durum_DurumId",
                table: "Cinsiyet");

            migrationBuilder.DropForeignKey(
                name: "FK_GorevlendirilmeTuru_Durum_DurumId",
                table: "GorevlendirilmeTuru");

            migrationBuilder.DropForeignKey(
                name: "FK_KadroTuru_Durum_DurumId",
                table: "KadroTuru");

            migrationBuilder.DropForeignKey(
                name: "FK_KanGrubu_Durum_DurumId",
                table: "KanGrubu");

            migrationBuilder.DropForeignKey(
                name: "FK_Kurum_Durum_DurumId",
                table: "Kurum");

            migrationBuilder.DropForeignKey(
                name: "FK_KurumTipi_Durum_DurumId",
                table: "KurumTipi");

            migrationBuilder.DropForeignKey(
                name: "FK_Personel_Durum_DurumId",
                table: "Personel");

            migrationBuilder.DropForeignKey(
                name: "FK_Sehir_Durum_DurumId",
                table: "Sehir");

            migrationBuilder.DropForeignKey(
                name: "FK_Unvan_Durum_DurumId",
                table: "Unvan");

            migrationBuilder.DropForeignKey(
                name: "FK_CalismaDurumu_Personel_GuncelleyenPersonelId",
                table: "CalismaDurumu");

            migrationBuilder.DropForeignKey(
                name: "FK_CalismaDurumu_Personel_OlusturanPersonelId",
                table: "CalismaDurumu");

            migrationBuilder.DropForeignKey(
                name: "FK_CalismaDurumu_Personel_SilenPersonelId",
                table: "CalismaDurumu");

            migrationBuilder.DropForeignKey(
                name: "FK_Cinsiyet_Personel_GuncelleyenPersonelId",
                table: "Cinsiyet");

            migrationBuilder.DropForeignKey(
                name: "FK_Cinsiyet_Personel_OlusturanPersonelId",
                table: "Cinsiyet");

            migrationBuilder.DropForeignKey(
                name: "FK_Cinsiyet_Personel_SilenPersonelId",
                table: "Cinsiyet");

            migrationBuilder.DropForeignKey(
                name: "FK_GorevlendirilmeTuru_Personel_GuncelleyenPersonelId",
                table: "GorevlendirilmeTuru");

            migrationBuilder.DropForeignKey(
                name: "FK_GorevlendirilmeTuru_Personel_OlusturanPersonelId",
                table: "GorevlendirilmeTuru");

            migrationBuilder.DropForeignKey(
                name: "FK_GorevlendirilmeTuru_Personel_SilenPersonelId",
                table: "GorevlendirilmeTuru");

            migrationBuilder.DropForeignKey(
                name: "FK_KadroTuru_Personel_GuncelleyenPersonelId",
                table: "KadroTuru");

            migrationBuilder.DropForeignKey(
                name: "FK_KadroTuru_Personel_OlusturanPersonelId",
                table: "KadroTuru");

            migrationBuilder.DropForeignKey(
                name: "FK_KadroTuru_Personel_SilenPersonelId",
                table: "KadroTuru");

            migrationBuilder.DropForeignKey(
                name: "FK_KanGrubu_Personel_GuncelleyenPersonelId",
                table: "KanGrubu");

            migrationBuilder.DropForeignKey(
                name: "FK_KanGrubu_Personel_OlusturanPersonelId",
                table: "KanGrubu");

            migrationBuilder.DropForeignKey(
                name: "FK_KanGrubu_Personel_SilenPersonelId",
                table: "KanGrubu");

            migrationBuilder.DropForeignKey(
                name: "FK_Kurum_Personel_GuncelleyenPersonelId",
                table: "Kurum");

            migrationBuilder.DropForeignKey(
                name: "FK_Kurum_Personel_OlusturanPersonelId",
                table: "Kurum");

            migrationBuilder.DropForeignKey(
                name: "FK_Kurum_Personel_SilenPersonelId",
                table: "Kurum");

            migrationBuilder.DropForeignKey(
                name: "FK_KurumTipi_Personel_GuncelleyenPersonelId",
                table: "KurumTipi");

            migrationBuilder.DropForeignKey(
                name: "FK_KurumTipi_Personel_OlusturanPersonelId",
                table: "KurumTipi");

            migrationBuilder.DropForeignKey(
                name: "FK_KurumTipi_Personel_SilenPersonelId",
                table: "KurumTipi");

            migrationBuilder.DropForeignKey(
                name: "FK_Sehir_Personel_GuncelleyenPersonelId",
                table: "Sehir");

            migrationBuilder.DropForeignKey(
                name: "FK_Sehir_Personel_OlusturanPersonelId",
                table: "Sehir");

            migrationBuilder.DropForeignKey(
                name: "FK_Sehir_Personel_SilenPersonelId",
                table: "Sehir");

            migrationBuilder.DropForeignKey(
                name: "FK_Unvan_Personel_GuncelleyenPersonelId",
                table: "Unvan");

            migrationBuilder.DropForeignKey(
                name: "FK_Unvan_Personel_OlusturanPersonelId",
                table: "Unvan");

            migrationBuilder.DropForeignKey(
                name: "FK_Unvan_Personel_SilenPersonelId",
                table: "Unvan");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "PersonelGorevlendirilme");

            migrationBuilder.DropTable(
                name: "PersonelUnvanGecmisi");

            migrationBuilder.DropTable(
                name: "Ekran");

            migrationBuilder.DropTable(
                name: "AppRole");

            migrationBuilder.DropTable(
                name: "AppUser");

            migrationBuilder.DropTable(
                name: "GorevlendirilmeTipi");

            migrationBuilder.DropTable(
                name: "Kisim");

            migrationBuilder.DropTable(
                name: "SidebarMenu");

            migrationBuilder.DropTable(
                name: "Birim");

            migrationBuilder.DropTable(
                name: "KabinetBazliBolum");

            migrationBuilder.DropTable(
                name: "Bolum");

            migrationBuilder.DropTable(
                name: "Durum");

            migrationBuilder.DropTable(
                name: "Personel");

            migrationBuilder.DropTable(
                name: "CalismaDurumu");

            migrationBuilder.DropTable(
                name: "Cinsiyet");

            migrationBuilder.DropTable(
                name: "GorevlendirilmeTuru");

            migrationBuilder.DropTable(
                name: "KadroTuru");

            migrationBuilder.DropTable(
                name: "KanGrubu");

            migrationBuilder.DropTable(
                name: "Kurum");

            migrationBuilder.DropTable(
                name: "Unvan");

            migrationBuilder.DropTable(
                name: "KurumTipi");

            migrationBuilder.DropTable(
                name: "Sehir");
        }
    }
}
