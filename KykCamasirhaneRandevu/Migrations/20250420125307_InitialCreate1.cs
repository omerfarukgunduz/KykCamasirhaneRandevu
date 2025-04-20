using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KykCamasirhaneRandevu.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Ogrenciler_OgrenciID",
                table: "Randevular");

            migrationBuilder.AlterColumn<int>(
                name: "OgrenciID",
                table: "Randevular",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Ogrenciler_OgrenciID",
                table: "Randevular",
                column: "OgrenciID",
                principalTable: "Ogrenciler",
                principalColumn: "OgrenciID",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Ogrenciler_OgrenciID",
                table: "Randevular");

            migrationBuilder.AlterColumn<int>(
                name: "OgrenciID",
                table: "Randevular",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Ogrenciler_OgrenciID",
                table: "Randevular",
                column: "OgrenciID",
                principalTable: "Ogrenciler",
                principalColumn: "OgrenciID");
        }
    }
}
