using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DotNetJobSeek.Infrastructure.EF.Migrations
{
    public partial class selfreference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KeywordNeighbors",
                columns: table => new
                {
                    LeftId = table.Column<int>(nullable: false),
                    RightId = table.Column<int>(nullable: false),
                    Weight = table.Column<int>(nullable: false, defaultValue: 99)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeywordNeighbors", x => new { x.LeftId, x.RightId });
                    table.ForeignKey(
                        name: "FK_KeywordNeighbors_Keywords_LeftId",
                        column: x => x.LeftId,
                        principalTable: "Keywords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KeywordNeighbors_Keywords_RightId",
                        column: x => x.RightId,
                        principalTable: "Keywords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KeywordNeighbors_RightId",
                table: "KeywordNeighbors",
                column: "RightId");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordNeighbors_Weight",
                table: "KeywordNeighbors",
                column: "Weight");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KeywordNeighbors");
        }
    }
}
