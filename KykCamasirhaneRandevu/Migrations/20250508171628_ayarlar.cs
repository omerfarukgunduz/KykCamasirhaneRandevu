using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KykCamasirhaneRandevu.Migrations
{
    /// <inheritdoc />
    public partial class ayarlar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HatirlatmaGonderildi",
                table: "Randevular",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "EmailAyarlari",
                columns: table => new
                {
                    EmailAyarlariID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SmtpServer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SmtpPort = table.Column<int>(type: "int", nullable: false),
                    SmtpUsername = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SmtpPassword = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FromEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SonGuncellemeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailAyarlari", x => x.EmailAyarlariID);
                });

            migrationBuilder.CreateTable(
                name: "RandevuHatirlatma",
                columns: table => new
                {
                    RandevuHatirlatmaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HatirlatmaSuresiDakika = table.Column<int>(type: "int", nullable: false),
                    SonGuncellemeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RandevuHatirlatma", x => x.RandevuHatirlatmaID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailAyarlari");

            migrationBuilder.DropTable(
                name: "RandevuHatirlatma");

            migrationBuilder.DropColumn(
                name: "HatirlatmaGonderildi",
                table: "Randevular");
        }
    }
}
