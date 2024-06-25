using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace filmsAPI.Migrations
{
    public partial class NullableMovieId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Movies_MovieId",
                table: "Sections");

            migrationBuilder.AlterColumn<int>(
                name: "MovieId",
                table: "Sections",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Movies_MovieId",
                table: "Sections",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Movies_MovieId",
                table: "Sections");

            migrationBuilder.AlterColumn<int>(
                name: "MovieId",
                table: "Sections",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Movies_MovieId",
                table: "Sections",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
