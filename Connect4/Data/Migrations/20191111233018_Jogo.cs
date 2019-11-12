using Microsoft.EntityFrameworkCore.Migrations;

namespace Connect4.Data.Migrations
{
    public partial class Jogo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jogador",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TorneioId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jogador", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jogador_Torneio_TorneioId",
                        column: x => x.TorneioId,
                        principalTable: "Torneio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tabuleiro",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JogadorAtual = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tabuleiro", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jogo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Jogador1Id = table.Column<int>(nullable: true),
                    Jogador2Id = table.Column<int>(nullable: true),
                    tabuleiroId = table.Column<int>(nullable: true),
                    TorneioId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jogo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jogo_Jogador_Jogador1Id",
                        column: x => x.Jogador1Id,
                        principalTable: "Jogador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Jogo_Jogador_Jogador2Id",
                        column: x => x.Jogador2Id,
                        principalTable: "Jogador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Jogo_Torneio_TorneioId",
                        column: x => x.TorneioId,
                        principalTable: "Torneio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Jogo_Tabuleiro_tabuleiroId",
                        column: x => x.tabuleiroId,
                        principalTable: "Tabuleiro",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jogador_TorneioId",
                table: "Jogador",
                column: "TorneioId");

            migrationBuilder.CreateIndex(
                name: "IX_Jogo_Jogador1Id",
                table: "Jogo",
                column: "Jogador1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Jogo_Jogador2Id",
                table: "Jogo",
                column: "Jogador2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Jogo_TorneioId",
                table: "Jogo",
                column: "TorneioId");

            migrationBuilder.CreateIndex(
                name: "IX_Jogo_tabuleiroId",
                table: "Jogo",
                column: "tabuleiroId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jogo");

            migrationBuilder.DropTable(
                name: "Jogador");

            migrationBuilder.DropTable(
                name: "Tabuleiro");
        }
    }
}
