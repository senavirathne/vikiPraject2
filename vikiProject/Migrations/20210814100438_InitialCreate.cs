using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace vikiProject.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dramas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    NoOfEpisodes = table.Column<int>(type: "INTEGER", nullable: false),
                    ImageSource = table.Column<string>(type: "TEXT", nullable: true),
                    Priority = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dramas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Episodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    EpisodeNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    EpisodeSource = table.Column<string>(type: "TEXT", nullable: true),
                    ImageSource = table.Column<string>(type: "TEXT", nullable: true),
                    AudioLink = table.Column<string>(type: "TEXT", nullable: true),
                    VideoLink = table.Column<string>(type: "TEXT", nullable: true),
                    DramaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episodes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dramas");

            migrationBuilder.DropTable(
                name: "Episodes");
        }
    }
}
