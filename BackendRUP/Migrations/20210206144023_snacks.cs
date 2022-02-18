using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WatchTogetherAPI.Migrations
{
    public partial class snacks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SnackId",
                table: "Lobbies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Snacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Thumbnail = table.Column<string>(type: "longtext", nullable: true),
                    Title = table.Column<string>(type: "longtext", nullable: true),
                    Description = table.Column<string>(type: "longtext", nullable: true),
                    Link = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Snacks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lobbies_SnackId",
                table: "Lobbies",
                column: "SnackId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lobbies_Snacks_SnackId",
                table: "Lobbies",
                column: "SnackId",
                principalTable: "Snacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lobbies_Snacks_SnackId",
                table: "Lobbies");

            migrationBuilder.DropTable(
                name: "Snacks");

            migrationBuilder.DropIndex(
                name: "IX_Lobbies_SnackId",
                table: "Lobbies");

            migrationBuilder.DropColumn(
                name: "SnackId",
                table: "Lobbies");
        }
    }
}
