using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KykCamasirhaneRandevu.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RandevuOnayDurumu",
                table: "Randevular");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "RandevuOnayDurumu",
                table: "Randevular",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
