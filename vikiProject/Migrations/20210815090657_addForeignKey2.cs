using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace vikiProject.Migrations
{
    public partial class addForeignKey2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episodes_Dramas_DramaId1",
                table: "Episodes");

            migrationBuilder.DropIndex(
                name: "IX_Episodes_DramaId1",
                table: "Episodes");

            migrationBuilder.DropColumn(
                name: "DramaId1",
                table: "Episodes");

            migrationBuilder.AlterColumn<Guid>(
                name: "DramaId",
                table: "Episodes",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_DramaId",
                table: "Episodes",
                column: "DramaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Episodes_Dramas_DramaId",
                table: "Episodes",
                column: "DramaId",
                principalTable: "Dramas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episodes_Dramas_DramaId",
                table: "Episodes");

            migrationBuilder.DropIndex(
                name: "IX_Episodes_DramaId",
                table: "Episodes");

            migrationBuilder.AlterColumn<int>(
                name: "DramaId",
                table: "Episodes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DramaId1",
                table: "Episodes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_DramaId1",
                table: "Episodes",
                column: "DramaId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Episodes_Dramas_DramaId1",
                table: "Episodes",
                column: "DramaId1",
                principalTable: "Dramas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
