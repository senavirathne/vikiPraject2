using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace vikiProject.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dramas",
                columns: table => new
                {
                    DramaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ImageSource = table.Column<string>(type: "TEXT", nullable: false),
                    MainName = table.Column<string>(type: "TEXT", nullable: false),
                    NoOfEpisodes = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dramas", x => x.DramaId);
                });

            migrationBuilder.CreateTable(
                name: "Episodes",
                columns: table => new
                {
                    EpisodeNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    DramaId = table.Column<int>(type: "INTEGER", nullable: false),
                    ImageSource = table.Column<string>(type: "TEXT", nullable: false),
                    EpisodeSource = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episodes", x => new { x.EpisodeNumber, x.DramaId });
                    table.ForeignKey(
                        name: "FK_Episodes_Dramas_DramaId",
                        column: x => x.DramaId,
                        principalTable: "Dramas",
                        principalColumn: "DramaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DownloadLinks",
                columns: table => new
                {
                    Quality = table.Column<int>(type: "INTEGER", nullable: false),
                    EpisodeNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    DramaId = table.Column<int>(type: "INTEGER", nullable: false),
                    AudioLink = table.Column<string>(type: "TEXT", nullable: true),
                    VideoLink = table.Column<string>(type: "TEXT", nullable: true),
                    AddedTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DownloadLinks", x => new { x.DramaId, x.EpisodeNumber, x.Quality });
                    table.ForeignKey(
                        name: "FK_DownloadLinks_Episodes_EpisodeNumber_DramaId",
                        columns: x => new { x.EpisodeNumber, x.DramaId },
                        principalTable: "Episodes",
                        principalColumns: new[] { "EpisodeNumber", "DramaId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DownloadLinks_EpisodeNumber_DramaId",
                table: "DownloadLinks",
                columns: new[] { "EpisodeNumber", "DramaId" });

            migrationBuilder.CreateIndex(
                name: "IX_Dramas_MainName",
                table: "Dramas",
                column: "MainName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_DramaId",
                table: "Episodes",
                column: "DramaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DownloadLinks");

            migrationBuilder.DropTable(
                name: "Episodes");

            migrationBuilder.DropTable(
                name: "Dramas");
        }
    }
}
