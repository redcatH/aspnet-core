﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mall.Migrations
{
    public partial class addProductSkuITenant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "Mall_ProductSku",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Mall_ProductSku");
        }
    }
}
