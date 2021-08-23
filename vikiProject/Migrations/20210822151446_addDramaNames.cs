using Microsoft.EntityFrameworkCore.Migrations;

namespace vikiProject.Migrations
{
    public partial class addDramaNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DramaNameWithCode",
                columns: table => new
                {
                    NameId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    DramaId = table.Column<int>(type: "INTEGER", nullable: true)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DramaNameWithCode");
        }
    }
}
