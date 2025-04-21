using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KykCamasirhaneRandevu.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
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
                    OneriPuani = table.Column<int>(type: "int", nullable: false),
                    AnketTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    DuyuruKonu = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DuyuruMetin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DuyuruTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Duyurular", x => x.DuyuruID);
                });

            migrationBuilder.CreateTable(
                name: "Ogrenciler",
                columns: table => new
                {
                    OgrenciID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OgrenciTC = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    OgrenciAdSoyad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OgrenciEposta = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OgrenciSifre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Oda_YatakNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CezaDurumu = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ogrenciler", x => x.OgrenciID);
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
                name: "Mesajlar",
                columns: table => new
                {
                    MesajID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OgrenciID = table.Column<int>(type: "int", nullable: true),
                    Baslik = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Icerik = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Okundu = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mesajlar", x => x.MesajID);
                    table.ForeignKey(
                        name: "FK_Mesajlar_Ogrenciler_OgrenciID",
                        column: x => x.OgrenciID,
                        principalTable: "Ogrenciler",
                        principalColumn: "OgrenciID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Randevular",
                columns: table => new
                {
                    RandevuID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OgrenciID = table.Column<int>(type: "int", nullable: true),
                    RandevuTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MakineNo = table.Column<int>(type: "int", nullable: false),
                    Kurutma = table.Column<bool>(type: "bit", nullable: false),
                    TeslimeKalanSure = table.Column<int>(type: "int", nullable: false),
                    RandevuOnayDurumu = table.Column<bool>(type: "bit", nullable: false),
                    RandevuGerceklesti = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Randevular", x => x.RandevuID);
                    table.ForeignKey(
                        name: "FK_Randevular_Ogrenciler_OgrenciID",
                        column: x => x.OgrenciID,
                        principalTable: "Ogrenciler",
                        principalColumn: "OgrenciID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mesajlar_OgrenciID",
                table: "Mesajlar",
                column: "OgrenciID");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_OgrenciID",
                table: "Randevular",
                column: "OgrenciID");
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
                name: "Randevular");

            migrationBuilder.DropTable(
                name: "Yoneticiler");

            migrationBuilder.DropTable(
                name: "Ogrenciler");
        }
    }
}
