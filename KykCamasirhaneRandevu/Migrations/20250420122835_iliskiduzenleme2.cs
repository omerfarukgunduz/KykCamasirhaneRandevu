using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KykCamasirhaneRandevu.Migrations
{
    /// <inheritdoc />
    public partial class iliskiduzenleme2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Ogrenciler_OgrenciID",
                table: "Randevular");

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Ogrenciler_OgrenciID",
                table: "Randevular",
                column: "OgrenciID",
                principalTable: "Ogrenciler",
                principalColumn: "OgrenciID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Ogrenciler_OgrenciID",
                table: "Randevular");

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Ogrenciler_OgrenciID",
                table: "Randevular",
                column: "OgrenciID",
                principalTable: "Ogrenciler",
                principalColumn: "OgrenciID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
