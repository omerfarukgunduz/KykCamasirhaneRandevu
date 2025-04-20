using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KykCamasirhaneRandevu.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Anketler",
                columns: table => new
                {
                    AnketId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GirisKolayligiPuani = table.Column<int>(type: "int", nullable: false),
                    RandevuIslemiPuani = table.Column<int>(type: "int", nullable: false),
                    PerformansPuani = table.Column<int>(type: "int", nullable: false),
                    ArayuzPuani = table.Column<int>(type: "int", nullable: false),
                    GenelMemnuniyetPuani = table.Column<int>(type: "int", nullable: false),
                    OneriPuani = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anketler", x => x.AnketId);
                });

            migrationBuilder.CreateTable(
                name: "Duyurular",
                columns: table => new
                {
                    DuyuruID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DuyuruKonu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DuyuruMetin = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Duyurular", x => x.DuyuruID);
                });

            migrationBuilder.CreateTable(
                name: "Mesajlar",
                columns: table => new
                {
                    MesajID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OgrenciAdSoyad = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    OgrenciEposta = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Konu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MesajIcerik = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cevaplandi = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mesajlar", x => x.MesajID);
                });

            migrationBuilder.CreateTable(
                name: "Randevular",
                columns: table => new
                {
                    RandevuID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RandevuGunSaat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MakineNo = table.Column<byte>(type: "tinyint", nullable: false),
                    KurutmaSecenegi = table.Column<bool>(type: "bit", nullable: false),
                    RandevuSahibi = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    RandevuOnayDurumu = table.Column<bool>(type: "bit", nullable: false),
                    RandevuGerceklesmeDurumu = table.Column<bool>(type: "bit", nullable: false),
                    TeslimeKalanSure = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Randevular", x => x.RandevuID);
                });

            migrationBuilder.CreateTable(
                name: "Yoneticiler",
                columns: table => new
                {
                    YoneticiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YoneticiAdSoyad = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    YoneticiEposta = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    YoneticiSifre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    YoneticiTC = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Yoneticiler", x => x.YoneticiID);
                });

            migrationBuilder.CreateTable(
                name: "Ogrenciler",
                columns: table => new
                {
                    OgrenciID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OgrenciAdSoyad = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Oda_YatakNo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    OgrenciTC = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    OgrenciEposta = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OgrenciSifre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CezaDurumu = table.Column<bool>(type: "bit", nullable: false),
                    RandevuID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ogrenciler", x => x.OgrenciID);
                    table.ForeignKey(
                        name: "FK_Ogrenciler_Randevular_RandevuID",
                        column: x => x.RandevuID,
                        principalTable: "Randevular",
                        principalColumn: "RandevuID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ogrenciler_RandevuID",
                table: "Ogrenciler",
                column: "RandevuID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Anketler");

            migrationBuilder.DropTable(
                name: "Duyurular");

            migrationBuilder.DropTable(
                name: "Mesajlar");

            migrationBuilder.DropTable(
                name: "Ogrenciler");

            migrationBuilder.DropTable(
                name: "Yoneticiler");

            migrationBuilder.DropTable(
                name: "Randevular");
        }
    }
}
