using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core6Music.Web.Migrations
{
    public partial class UpdateFavorite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteAlbum_AspNetUsers_MusicUserId1",
                schema: "dbo",
                table: "FavoriteAlbum");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteArtist_AspNetUsers_MusicUserId1",
                schema: "dbo",
                table: "FavoriteArtist");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteSong_AspNetUsers_MusicUserId1",
                schema: "dbo",
                table: "FavoriteSong");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoriteSong",
                schema: "dbo",
                table: "FavoriteSong");

            migrationBuilder.DropIndex(
                name: "IX_FavoriteSong_MusicUserId1",
                schema: "dbo",
                table: "FavoriteSong");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoriteArtist",
                schema: "dbo",
                table: "FavoriteArtist");

            migrationBuilder.DropIndex(
                name: "IX_FavoriteArtist_MusicUserId1",
                schema: "dbo",
                table: "FavoriteArtist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoriteAlbum",
                schema: "dbo",
                table: "FavoriteAlbum");

            migrationBuilder.DropIndex(
                name: "IX_FavoriteAlbum_MusicUserId1",
                schema: "dbo",
                table: "FavoriteAlbum");

            migrationBuilder.DropColumn(
                name: "MusicUserId1",
                schema: "dbo",
                table: "FavoriteSong");

            migrationBuilder.DropColumn(
                name: "MusicUserId1",
                schema: "dbo",
                table: "FavoriteArtist");

            migrationBuilder.DropColumn(
                name: "MusicUserId1",
                schema: "dbo",
                table: "FavoriteAlbum");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                schema: "dbo",
                table: "FavoriteSong",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                schema: "dbo",
                table: "FavoriteArtist",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                schema: "dbo",
                table: "FavoriteAlbum",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoriteSong",
                schema: "dbo",
                table: "FavoriteSong",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoriteArtist",
                schema: "dbo",
                table: "FavoriteArtist",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoriteAlbum",
                schema: "dbo",
                table: "FavoriteAlbum",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteSong_MusicUserId",
                schema: "dbo",
                table: "FavoriteSong",
                column: "MusicUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteArtist_MusicUserId",
                schema: "dbo",
                table: "FavoriteArtist",
                column: "MusicUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteAlbum_MusicUserId",
                schema: "dbo",
                table: "FavoriteAlbum",
                column: "MusicUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteAlbum_AspNetUsers_MusicUserId",
                schema: "dbo",
                table: "FavoriteAlbum",
                column: "MusicUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteArtist_AspNetUsers_MusicUserId",
                schema: "dbo",
                table: "FavoriteArtist",
                column: "MusicUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteSong_AspNetUsers_MusicUserId",
                schema: "dbo",
                table: "FavoriteSong",
                column: "MusicUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteAlbum_AspNetUsers_MusicUserId",
                schema: "dbo",
                table: "FavoriteAlbum");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteArtist_AspNetUsers_MusicUserId",
                schema: "dbo",
                table: "FavoriteArtist");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteSong_AspNetUsers_MusicUserId",
                schema: "dbo",
                table: "FavoriteSong");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoriteSong",
                schema: "dbo",
                table: "FavoriteSong");

            migrationBuilder.DropIndex(
                name: "IX_FavoriteSong_MusicUserId",
                schema: "dbo",
                table: "FavoriteSong");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoriteArtist",
                schema: "dbo",
                table: "FavoriteArtist");

            migrationBuilder.DropIndex(
                name: "IX_FavoriteArtist_MusicUserId",
                schema: "dbo",
                table: "FavoriteArtist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoriteAlbum",
                schema: "dbo",
                table: "FavoriteAlbum");

            migrationBuilder.DropIndex(
                name: "IX_FavoriteAlbum_MusicUserId",
                schema: "dbo",
                table: "FavoriteAlbum");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "dbo",
                table: "FavoriteSong");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "dbo",
                table: "FavoriteArtist");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "dbo",
                table: "FavoriteAlbum");

            migrationBuilder.AddColumn<string>(
                name: "MusicUserId1",
                schema: "dbo",
                table: "FavoriteSong",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MusicUserId1",
                schema: "dbo",
                table: "FavoriteArtist",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MusicUserId1",
                schema: "dbo",
                table: "FavoriteAlbum",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoriteSong",
                schema: "dbo",
                table: "FavoriteSong",
                column: "MusicUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoriteArtist",
                schema: "dbo",
                table: "FavoriteArtist",
                column: "MusicUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoriteAlbum",
                schema: "dbo",
                table: "FavoriteAlbum",
                column: "MusicUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteSong_MusicUserId1",
                schema: "dbo",
                table: "FavoriteSong",
                column: "MusicUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteArtist_MusicUserId1",
                schema: "dbo",
                table: "FavoriteArtist",
                column: "MusicUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteAlbum_MusicUserId1",
                schema: "dbo",
                table: "FavoriteAlbum",
                column: "MusicUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteAlbum_AspNetUsers_MusicUserId1",
                schema: "dbo",
                table: "FavoriteAlbum",
                column: "MusicUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteArtist_AspNetUsers_MusicUserId1",
                schema: "dbo",
                table: "FavoriteArtist",
                column: "MusicUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteSong_AspNetUsers_MusicUserId1",
                schema: "dbo",
                table: "FavoriteSong",
                column: "MusicUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
