using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KykCamasirhaneRandevu.Migrations
{
    /// <inheritdoc />
    public partial class CezaSuresiTablosu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mesajlar_Ogrenciler_OgrenciID1",
                table: "Mesajlar");

            migrationBuilder.DropIndex(
                name: "IX_Mesajlar_OgrenciID1",
                table: "Mesajlar");

            migrationBuilder.DropColumn(
                name: "OgrenciID1",
                table: "Mesajlar");

            migrationBuilder.CreateTable(
                name: "CezaSuresi",
                columns: table => new
                {
                    CezaSuresiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CezaSuresiDakika = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CezaSuresi", x => x.CezaSuresiID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CezaSuresi");

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
    }
}
