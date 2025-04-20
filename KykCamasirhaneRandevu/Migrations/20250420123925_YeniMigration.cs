using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KykCamasirhaneRandevu.Migrations
{
    /// <inheritdoc />
    public partial class YeniMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Anketler");

            migrationBuilder.DropTable(
                name: "Mesajlar");

            migrationBuilder.AlterColumn<string>(
                name: "DuyuruKonu",
                table: "Duyurular",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<DateTime>(
                name: "DuyuruTarihi",
                table: "Duyurular",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DuyuruTarihi",
                table: "Duyurular");

            migrationBuilder.AlterColumn<string>(
                name: "DuyuruKonu",
                table: "Duyurular",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.CreateTable(
                name: "Anketler",
                columns: table => new
                {
                    AnketId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArayuzPuani = table.Column<int>(type: "int", nullable: false),
                    GenelMemnuniyetPuani = table.Column<int>(type: "int", nullable: false),
                    GirisKolayligiPuani = table.Column<int>(type: "int", nullable: false),
                    OneriPuani = table.Column<int>(type: "int", nullable: false),
                    PerformansPuani = table.Column<int>(type: "int", nullable: false),
                    RandevuIslemiPuani = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anketler", x => x.AnketId);
                });

            migrationBuilder.CreateTable(
                name: "Mesajlar",
                columns: table => new
                {
                    MesajID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cevaplandi = table.Column<bool>(type: "bit", nullable: false),
                    Konu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MesajIcerik = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OgrenciAdSoyad = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    OgrenciEposta = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mesajlar", x => x.MesajID);
                });
        }
    }
}
