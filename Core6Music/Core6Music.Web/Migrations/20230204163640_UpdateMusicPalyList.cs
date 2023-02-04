using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core6Music.Web.Migrations
{
    public partial class UpdateMusicPalyList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AlbumId",
                schema: "dbo",
                table: "MusicManifestSong",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MusicManifestSong_AlbumId",
                schema: "dbo",
                table: "MusicManifestSong",
                column: "AlbumId");

            migrationBuilder.AddForeignKey(
                name: "FK_MusicManifestSong_Album_AlbumId",
                schema: "dbo",
                table: "MusicManifestSong",
                column: "AlbumId",
                principalSchema: "dbo",
                principalTable: "Album",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MusicManifestSong_Album_AlbumId",
                schema: "dbo",
                table: "MusicManifestSong");

            migrationBuilder.DropIndex(
                name: "IX_MusicManifestSong_AlbumId",
                schema: "dbo",
                table: "MusicManifestSong");

            migrationBuilder.DropColumn(
                name: "AlbumId",
                schema: "dbo",
                table: "MusicManifestSong");
        }
    }
}
