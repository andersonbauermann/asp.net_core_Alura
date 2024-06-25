using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace filmsAPI.Migrations
{
    public partial class RelationshipSectionandMovieTheater : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MovieTheaterId",
                table: "Sections",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sections_MovieTheaterId",
                table: "Sections",
                column: "MovieTheaterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_MovieTheaters_MovieTheaterId",
                table: "Sections",
                column: "MovieTheaterId",
                principalTable: "MovieTheaters",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sections_MovieTheaters_MovieTheaterId",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_Sections_MovieTheaterId",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "MovieTheaterId",
                table: "Sections");
        }
    }
}
