using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core6Music.Web.Migrations
{
    public partial class UpdateSong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ArtistName",
                schema: "dbo",
                table: "Song",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArtistName",
                schema: "dbo",
                table: "Song");
        }
    }
}
