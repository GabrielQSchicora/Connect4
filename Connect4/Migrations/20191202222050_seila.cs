using Microsoft.EntityFrameworkCore.Migrations;

namespace Connect4.Migrations
{
    public partial class seila : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jogo_Tabuleiros_tabuleiroId",
                table: "Jogo");

            migrationBuilder.RenameColumn(
                name: "tabuleiroId",
                table: "Jogo",
                newName: "TabuleiroId");

            migrationBuilder.RenameIndex(
                name: "IX_Jogo_tabuleiroId",
                table: "Jogo",
                newName: "IX_Jogo_TabuleiroId");

            migrationBuilder.AlterColumn<int>(
                name: "Vencedor",
                table: "Tabuleiros",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Jogo_Tabuleiros_TabuleiroId",
                table: "Jogo",
                column: "TabuleiroId",
                principalTable: "Tabuleiros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jogo_Tabuleiros_TabuleiroId",
                table: "Jogo");

            migrationBuilder.RenameColumn(
                name: "TabuleiroId",
                table: "Jogo",
                newName: "tabuleiroId");

            migrationBuilder.RenameIndex(
                name: "IX_Jogo_TabuleiroId",
                table: "Jogo",
                newName: "IX_Jogo_tabuleiroId");

            migrationBuilder.AlterColumn<int>(
                name: "Vencedor",
                table: "Tabuleiros",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Jogo_Tabuleiros_tabuleiroId",
                table: "Jogo",
                column: "tabuleiroId",
                principalTable: "Tabuleiros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
