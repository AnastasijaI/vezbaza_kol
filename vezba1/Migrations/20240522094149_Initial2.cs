using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vezba1.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AvtorKniga",
                columns: table => new
                {
                    AvtoriId = table.Column<int>(type: "int", nullable: false),
                    KnigiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvtorKniga", x => new { x.AvtoriId, x.KnigiId });
                    table.ForeignKey(
                        name: "FK_AvtorKniga_Avtori_AvtoriId",
                        column: x => x.AvtoriId,
                        principalTable: "Avtori",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AvtorKniga_Knigi_KnigiId",
                        column: x => x.KnigiId,
                        principalTable: "Knigi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AvtorKniga_KnigiId",
                table: "AvtorKniga",
                column: "KnigiId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AvtorKniga");
        }
    }
}
