using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SVE.Mediatek.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addManagerFeatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Salt",
                table: "Staffs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salt",
                table: "Staffs");
        }
    }
}
