using Microsoft.EntityFrameworkCore.Migrations;

namespace Connect4.Data.Migrations
{
    public partial class Premiacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Premiacao",
                table: "Torneio",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Premiacao",
                table: "Torneio");
        }
    }
}
