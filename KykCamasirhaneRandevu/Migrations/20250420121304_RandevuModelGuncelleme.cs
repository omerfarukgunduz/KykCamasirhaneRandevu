using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KykCamasirhaneRandevu.Migrations
{
    /// <inheritdoc />
    public partial class RandevuModelGuncelleme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KurutmaSecenegi",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "MakineNo",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "RandevuSahibi",
                table: "Randevular");

            migrationBuilder.RenameColumn(
                name: "RandevuGunSaat",
                table: "Randevular",
                newName: "RandevuTarihi");

            migrationBuilder.RenameColumn(
                name: "RandevuGerceklesmeDurumu",
                table: "Randevular",
                newName: "Kurutma");

            migrationBuilder.AddColumn<int>(
                name: "OgrenciID",
                table: "Randevular",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OgrenciID1",
                table: "Randevular",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "RandevuGerceklesti",
                table: "Randevular",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_OgrenciID1",
                table: "Randevular",
                column: "OgrenciID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Ogrenciler_OgrenciID1",
                table: "Randevular",
                column: "OgrenciID1",
                principalTable: "Ogrenciler",
                principalColumn: "OgrenciID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Ogrenciler_OgrenciID1",
                table: "Randevular");

            migrationBuilder.DropIndex(
                name: "IX_Randevular_OgrenciID1",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "OgrenciID",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "OgrenciID1",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "RandevuGerceklesti",
                table: "Randevular");

            migrationBuilder.RenameColumn(
                name: "RandevuTarihi",
                table: "Randevular",
                newName: "RandevuGunSaat");

            migrationBuilder.RenameColumn(
                name: "Kurutma",
                table: "Randevular",
                newName: "RandevuGerceklesmeDurumu");

            migrationBuilder.AddColumn<bool>(
                name: "KurutmaSecenegi",
                table: "Randevular",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte>(
                name: "MakineNo",
                table: "Randevular",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<string>(
                name: "RandevuSahibi",
                table: "Randevular",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: true);
        }
    }
}
