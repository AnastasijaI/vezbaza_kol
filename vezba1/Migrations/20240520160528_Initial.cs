using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vezba1.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Avtori",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Pol = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Nacionalnost = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    DatumRagjanje = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avtori", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Knigi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naslov = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Godina = table.Column<int>(type: "int", nullable: true),
                    BrStrani = table.Column<int>(type: "int", nullable: true),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zanr = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Tirazh = table.Column<int>(type: "int", nullable: true),
                    Izdavac = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    SlikaUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Knigi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AvtorKnigi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AvtorId = table.Column<int>(type: "int", nullable: false),
                    KnigaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvtorKnigi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AvtorKnigi_Avtori_AvtorId",
                        column: x => x.AvtorId,
                        principalTable: "Avtori",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AvtorKnigi_Knigi_KnigaId",
                        column: x => x.KnigaId,
                        principalTable: "Knigi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AvtorKnigi_AvtorId",
                table: "AvtorKnigi",
                column: "AvtorId");

            migrationBuilder.CreateIndex(
                name: "IX_AvtorKnigi_KnigaId",
                table: "AvtorKnigi",
                column: "KnigaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AvtorKnigi");

            migrationBuilder.DropTable(
                name: "Avtori");

            migrationBuilder.DropTable(
                name: "Knigi");
        }
    }
}
