using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KykCamasirhaneRandevu.Migrations
{
    /// <inheritdoc />
    public partial class anketaylık1kez : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Kurutma",
                table: "Randevular",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<int>(
                name: "OgrenciID",
                table: "Anketler",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Anketler_OgrenciID",
                table: "Anketler",
                column: "OgrenciID");

            migrationBuilder.AddForeignKey(
                name: "FK_Anketler_Ogrenciler_OgrenciID",
                table: "Anketler",
                column: "OgrenciID",
                principalTable: "Ogrenciler",
                principalColumn: "OgrenciID",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Anketler_Ogrenciler_OgrenciID",
                table: "Anketler");

            migrationBuilder.DropIndex(
                name: "IX_Anketler_OgrenciID",
                table: "Anketler");

            migrationBuilder.DropColumn(
                name: "OgrenciID",
                table: "Anketler");

            migrationBuilder.AlterColumn<bool>(
                name: "Kurutma",
                table: "Randevular",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }
    }
}
