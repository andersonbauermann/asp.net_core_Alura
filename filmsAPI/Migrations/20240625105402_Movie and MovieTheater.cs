using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace filmsAPI.Migrations
{
    public partial class MovieandMovieTheater : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Movies_MovieId",
                table: "Sections");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_MovieTheaters_MovieTheaterId",
                table: "Sections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sections",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_Sections_MovieId",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Sections");

            migrationBuilder.AlterColumn<int>(
                name: "MovieTheaterId",
                table: "Sections",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MovieId",
                table: "Sections",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sections",
                table: "Sections",
                columns: new[] { "MovieId", "MovieTheaterId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Movies_MovieId",
                table: "Sections",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_MovieTheaters_MovieTheaterId",
                table: "Sections",
                column: "MovieTheaterId",
                principalTable: "MovieTheaters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Movies_MovieId",
                table: "Sections");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_MovieTheaters_MovieTheaterId",
                table: "Sections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sections",
                table: "Sections");

            migrationBuilder.AlterColumn<int>(
                name: "MovieTheaterId",
                table: "Sections",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MovieId",
                table: "Sections",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Sections",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sections",
                table: "Sections",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_MovieId",
                table: "Sections",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Movies_MovieId",
                table: "Sections",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_MovieTheaters_MovieTheaterId",
                table: "Sections",
                column: "MovieTheaterId",
                principalTable: "MovieTheaters",
                principalColumn: "Id");
        }
    }
}
