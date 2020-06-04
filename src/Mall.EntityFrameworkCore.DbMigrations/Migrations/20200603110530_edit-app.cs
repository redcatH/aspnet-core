using Microsoft.EntityFrameworkCore.Migrations;

namespace Mall.Migrations
{
    public partial class editapp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "AppManagementApps",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "AppManagementApps");
        }
    }
}
