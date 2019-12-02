using Microsoft.EntityFrameworkCore.Migrations;

namespace Connect4.Migrations
{
    public partial class jogadorMaquina : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NomeMaquina",
                table: "Jogador",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "URLServico",
                table: "Jogador",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NomeMaquina",
                table: "Jogador");

            migrationBuilder.DropColumn(
                name: "URLServico",
                table: "Jogador");
        }
    }
}
