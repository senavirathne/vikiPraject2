using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace vikiProject.Migrations
{
    public partial class newUpdates2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episodes_Dramas_DramaId",
                table: "Episodes");

            migrationBuilder.DropColumn(
                name: "AudioLink",
                table: "Episodes");

            migrationBuilder.DropColumn(
                name: "VideoLink",
                table: "Episodes");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Dramas");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Dramas",
                newName: "MainName");

            migrationBuilder.AlterColumn<Guid>(
                name: "DramaId",
                table: "Episodes",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DownloadLinkId",
                table: "Episodes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DownloadLink",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AudioLink = table.Column<string>(type: "TEXT", nullable: true),
                    VideoLink = table.Column<string>(type: "TEXT", nullable: true),
                    AddedTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DownloadLink", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_DownloadLinkId",
                table: "Episodes",
                column: "DownloadLinkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Episodes_DownloadLink_DownloadLinkId",
                table: "Episodes",
                column: "DownloadLinkId",
                principalTable: "DownloadLink",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Episodes_Dramas_DramaId",
                table: "Episodes",
                column: "DramaId",
                principalTable: "Dramas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episodes_DownloadLink_DownloadLinkId",
                table: "Episodes");

            migrationBuilder.DropForeignKey(
                name: "FK_Episodes_Dramas_DramaId",
                table: "Episodes");

            migrationBuilder.DropTable(
                name: "DownloadLink");

            migrationBuilder.DropIndex(
                name: "IX_Episodes_DownloadLinkId",
                table: "Episodes");

            migrationBuilder.DropColumn(
                name: "DownloadLinkId",
                table: "Episodes");

            migrationBuilder.RenameColumn(
                name: "MainName",
                table: "Dramas",
                newName: "Name");

            migrationBuilder.AlterColumn<Guid>(
                name: "DramaId",
                table: "Episodes",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "AudioLink",
                table: "Episodes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VideoLink",
                table: "Episodes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "Dramas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Episodes_Dramas_DramaId",
                table: "Episodes",
                column: "DramaId",
                principalTable: "Dramas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
