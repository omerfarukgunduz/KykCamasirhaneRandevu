using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KykCamasirhaneRandevu.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "YoneticiSifre",
                table: "Yoneticiler",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "YoneticiAdSoyad",
                table: "Yoneticiler",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(70)",
                oldMaxLength: 70);

            migrationBuilder.AddColumn<int>(
                name: "VarsayilanCezaSuresiDakika",
                table: "Yoneticiler",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "OgrenciSifre",
                table: "Ogrenciler",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "OgrenciAdSoyad",
                table: "Ogrenciler",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Oda_YatakNo",
                table: "Ogrenciler",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<DateTime>(
                name: "CezaBitisTarihi",
                table: "Ogrenciler",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OgrenciID1",
                table: "Mesajlar",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mesajlar_OgrenciID1",
                table: "Mesajlar",
                column: "OgrenciID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Mesajlar_Ogrenciler_OgrenciID1",
                table: "Mesajlar",
                column: "OgrenciID1",
                principalTable: "Ogrenciler",
                principalColumn: "OgrenciID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mesajlar_Ogrenciler_OgrenciID1",
                table: "Mesajlar");

            migrationBuilder.DropIndex(
                name: "IX_Mesajlar_OgrenciID1",
                table: "Mesajlar");

            migrationBuilder.DropColumn(
                name: "VarsayilanCezaSuresiDakika",
                table: "Yoneticiler");

            migrationBuilder.DropColumn(
                name: "CezaBitisTarihi",
                table: "Ogrenciler");

            migrationBuilder.DropColumn(
                name: "OgrenciID1",
                table: "Mesajlar");

            migrationBuilder.AlterColumn<string>(
                name: "YoneticiSifre",
                table: "Yoneticiler",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "YoneticiAdSoyad",
                table: "Yoneticiler",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "OgrenciSifre",
                table: "Ogrenciler",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "OgrenciAdSoyad",
                table: "Ogrenciler",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Oda_YatakNo",
                table: "Ogrenciler",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);
        }
    }
}
