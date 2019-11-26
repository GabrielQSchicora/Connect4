using Microsoft.EntityFrameworkCore.Migrations;

namespace Connect4.Migrations
{
    public partial class foreignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_JogadorId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Jogador");

            migrationBuilder.AddColumn<int>(
                name: "Vencedor",
                table: "Tabuleiros",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_JogadorId",
                table: "AspNetUsers",
                column: "JogadorId",
                unique: true,
                filter: "[JogadorId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_JogadorId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Vencedor",
                table: "Tabuleiros");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Jogador",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_JogadorId",
                table: "AspNetUsers",
                column: "JogadorId");
        }
    }
}
