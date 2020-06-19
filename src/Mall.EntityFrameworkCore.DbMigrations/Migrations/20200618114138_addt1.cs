using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mall.Migrations
{
    public partial class addt1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mall_ProductCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    ShortNmae = table.Column<string>(maxLength: 64, nullable: true),
                    LogoImage = table.Column<string>(maxLength: 128, nullable: true),
                    RedirectUrl = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mall_ProductCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shops_Shop",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    ShortName = table.Column<string>(maxLength: 64, nullable: false),
                    LogoImage = table.Column<string>(maxLength: 254, nullable: true),
                    CoverImage = table.Column<string>(maxLength: 254, nullable: true),
                    Description = table.Column<string>(maxLength: 512, nullable: true),
                    TenantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shops_Shop", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mall_AppProductCategory",
                columns: table => new
                {
                    AppName = table.Column<string>(maxLength: 64, nullable: false),
                    ProductCategoryId = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mall_AppProductCategory", x => new { x.AppName, x.ProductCategoryId });
                    table.ForeignKey(
                        name: "FK_Mall_AppProductCategory_Mall_ProductCategory_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "Mall_ProductCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mall_ProductSpu",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    ShopId = table.Column<Guid>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    DescCommon = table.Column<string>(nullable: true),
                    PurchaseNotesCommon = table.Column<string>(nullable: true),
                    DateTimeStart = table.Column<DateTime>(nullable: true),
                    DateTimeEnd = table.Column<DateTime>(nullable: true),
                    LimitBuyCount = table.Column<int>(nullable: true),
                    SoldCount = table.Column<int>(nullable: true, defaultValue: 0),
                    CategoryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mall_ProductSpu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mall_ProductSpu_Mall_ProductCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Mall_ProductCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mall_ProductSku",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    OriginPrice = table.Column<decimal>(nullable: true),
                    VipPrice = table.Column<decimal>(nullable: true),
                    CoverImageUrls = table.Column<string>(nullable: true),
                    DateTimeStart = table.Column<DateTime>(nullable: true),
                    DateTimeEnd = table.Column<DateTime>(nullable: true),
                    LimitBuyCount = table.Column<int>(nullable: true),
                    SoldCount = table.Column<int>(nullable: true),
                    StockCount = table.Column<int>(nullable: false, defaultValue: 0),
                    Code = table.Column<string>(nullable: true),
                    Unit = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    Desc = table.Column<string>(nullable: true),
                    PurchaseNotes = table.Column<string>(nullable: true),
                    SpuId = table.Column<Guid>(nullable: false),
                    ShopId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mall_ProductSku", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mall_ProductSku_Mall_ProductSpu_SpuId",
                        column: x => x.SpuId,
                        principalTable: "Mall_ProductSpu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mall_AppProductCategory_ProductCategoryId",
                table: "Mall_AppProductCategory",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Mall_ProductSku_SpuId",
                table: "Mall_ProductSku",
                column: "SpuId");

            migrationBuilder.CreateIndex(
                name: "IX_Mall_ProductSpu_CategoryId",
                table: "Mall_ProductSpu",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mall_AppProductCategory");

            migrationBuilder.DropTable(
                name: "Mall_ProductSku");

            migrationBuilder.DropTable(
                name: "Shops_Shop");

            migrationBuilder.DropTable(
                name: "Mall_ProductSpu");

            migrationBuilder.DropTable(
                name: "Mall_ProductCategory");
        }
    }
}
