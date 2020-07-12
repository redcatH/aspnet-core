using Microsoft.EntityFrameworkCore.Migrations;

namespace Mall.Migrations
{
    public partial class FixProductCategoryShortName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortNmae",
                table: "Mall_ProductCategory");

            migrationBuilder.AddColumn<string>(
                name: "ShortName",
                table: "Mall_ProductCategory",
                maxLength: 64,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortName",
                table: "Mall_ProductCategory");

            migrationBuilder.AddColumn<string>(
                name: "ShortNmae",
                table: "Mall_ProductCategory",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);
        }
    }
}
