using Microsoft.EntityFrameworkCore.Migrations;

namespace vikiProject.Migrations
{
    public partial class addOtherNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DramaNameWithCode");

            migrationBuilder.CreateTable(
                name: "OtherNames",
                columns: table => new
                {
                    NameId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DramaId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherNames", x => x.NameId);
                    table.ForeignKey(
                        name: "FK_OtherNames_Dramas_DramaId",
                        column: x => x.DramaId,
                        principalTable: "Dramas",
                        principalColumn: "DramaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OtherNames_DramaId",
                table: "OtherNames",
                column: "DramaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OtherNames");

            migrationBuilder.CreateTable(
                name: "DramaNameWithCode",
                columns: table => new
                {
                    NameId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<int>(type: "INTEGER", nullable: false),
                    DramaId = table.Column<int>(type: "INTEGER", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DramaNameWithCode", x => x.NameId);
                    table.ForeignKey(
                        name: "FK_DramaNameWithCode_Dramas_DramaId",
                        column: x => x.DramaId,
                        principalTable: "Dramas",
                        principalColumn: "DramaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DramaNameWithCode_DramaId",
                table: "DramaNameWithCode",
                column: "DramaId");
        }
    }
}
