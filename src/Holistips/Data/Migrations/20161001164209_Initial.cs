using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Holistips.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tips",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TipAnalogy = table.Column<string>(nullable: true),
                    TipExplanation = table.Column<string>(nullable: true),
                    TipHashtags = table.Column<string>(nullable: true),
                    TipRefs = table.Column<string>(nullable: true),
                    TipTitle = table.Column<string>(nullable: true),
                    TipWhenTo = table.Column<string>(nullable: true),
                    TipWhere = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tips", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tips");
        }
    }
}
