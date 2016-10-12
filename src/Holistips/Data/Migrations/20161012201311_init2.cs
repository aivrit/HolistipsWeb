using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Holistips.Data.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipPackages",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PacakgeTitle = table.Column<string>(nullable: true),
                    PackageDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipPackages", x => x.ID);
                });

            migrationBuilder.AddColumn<int>(
                name: "TipPackageID",
                table: "Tips",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tips_TipPackageID",
                table: "Tips",
                column: "TipPackageID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tips_TipPackages_TipPackageID",
                table: "Tips",
                column: "TipPackageID",
                principalTable: "TipPackages",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tips_TipPackages_TipPackageID",
                table: "Tips");

            migrationBuilder.DropIndex(
                name: "IX_Tips_TipPackageID",
                table: "Tips");

            migrationBuilder.DropColumn(
                name: "TipPackageID",
                table: "Tips");

            migrationBuilder.DropTable(
                name: "TipPackages");
        }
    }
}
