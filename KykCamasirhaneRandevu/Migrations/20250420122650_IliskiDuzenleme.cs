using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KykCamasirhaneRandevu.Migrations
{
    /// <inheritdoc />
    public partial class IliskiDuzenleme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ogrenciler_Randevular_RandevuID",
                table: "Ogrenciler");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Ogrenciler_OgrenciID1",
                table: "Randevular");

            migrationBuilder.DropIndex(
                name: "IX_Randevular_OgrenciID1",
                table: "Randevular");

            migrationBuilder.DropIndex(
                name: "IX_Ogrenciler_RandevuID",
                table: "Ogrenciler");

            migrationBuilder.DropColumn(
                name: "OgrenciID1",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "RandevuID",
                table: "Ogrenciler");

            migrationBuilder.AlterColumn<string>(
                name: "OgrenciAdSoyad",
                table: "Ogrenciler",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(70)",
                oldMaxLength: 70);

            migrationBuilder.AlterColumn<string>(
                name: "Oda_YatakNo",
                table: "Ogrenciler",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_OgrenciID",
                table: "Randevular",
                column: "OgrenciID");

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Ogrenciler_OgrenciID",
                table: "Randevular",
                column: "OgrenciID",
                principalTable: "Ogrenciler",
                principalColumn: "OgrenciID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Ogrenciler_OgrenciID",
                table: "Randevular");

            migrationBuilder.DropIndex(
                name: "IX_Randevular_OgrenciID",
                table: "Randevular");

            migrationBuilder.AddColumn<int>(
                name: "OgrenciID1",
                table: "Randevular",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "OgrenciAdSoyad",
                table: "Ogrenciler",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Oda_YatakNo",
                table: "Ogrenciler",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "RandevuID",
                table: "Ogrenciler",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_OgrenciID1",
                table: "Randevular",
                column: "OgrenciID1");

            migrationBuilder.CreateIndex(
                name: "IX_Ogrenciler_RandevuID",
                table: "Ogrenciler",
                column: "RandevuID");

            migrationBuilder.AddForeignKey(
                name: "FK_Ogrenciler_Randevular_RandevuID",
                table: "Ogrenciler",
                column: "RandevuID",
                principalTable: "Randevular",
                principalColumn: "RandevuID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Ogrenciler_OgrenciID1",
                table: "Randevular",
                column: "OgrenciID1",
                principalTable: "Ogrenciler",
                principalColumn: "OgrenciID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
