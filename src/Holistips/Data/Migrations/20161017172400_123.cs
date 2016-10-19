using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Holistips.Data.Migrations
{
    public partial class _123 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PackageAuthorID",
                table: "TipPackages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "TipPackageCreationDate",
                table: "TipPackages",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TipAuthorID",
                table: "Tips",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "TipCreationDate",
                table: "Tips",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PackageAuthorID",
                table: "TipPackages");

            migrationBuilder.DropColumn(
                name: "TipPackageCreationDate",
                table: "TipPackages");

            migrationBuilder.DropColumn(
                name: "TipAuthorID",
                table: "Tips");

            migrationBuilder.DropColumn(
                name: "TipCreationDate",
                table: "Tips");
        }
    }
}
