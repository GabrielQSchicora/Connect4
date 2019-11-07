using Microsoft.EntityFrameworkCore.Migrations;

namespace Connect4.Data.Migrations
{
    public partial class Dono : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Dono",
                table: "Torneio",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dono",
                table: "Torneio");
        }
    }
}
