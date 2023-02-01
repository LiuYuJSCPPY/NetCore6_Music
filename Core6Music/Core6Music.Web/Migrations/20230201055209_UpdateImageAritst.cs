using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core6Music.Web.Migrations
{
    public partial class UpdateImageAritst : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ArtistHeadImage_ArtistId",
                schema: "dbo",
                table: "ArtistHeadImage");

            migrationBuilder.DropIndex(
                name: "IX_ArtistContextImage_ArtistId",
                schema: "dbo",
                table: "ArtistContextImage");

            migrationBuilder.DropIndex(
                name: "IX_ArtistBackImage_ArtistId",
                schema: "dbo",
                table: "ArtistBackImage");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistHeadImage_ArtistId",
                schema: "dbo",
                table: "ArtistHeadImage",
                column: "ArtistId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ArtistContextImage_ArtistId",
                schema: "dbo",
                table: "ArtistContextImage",
                column: "ArtistId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ArtistBackImage_ArtistId",
                schema: "dbo",
                table: "ArtistBackImage",
                column: "ArtistId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ArtistHeadImage_ArtistId",
                schema: "dbo",
                table: "ArtistHeadImage");

            migrationBuilder.DropIndex(
                name: "IX_ArtistContextImage_ArtistId",
                schema: "dbo",
                table: "ArtistContextImage");

            migrationBuilder.DropIndex(
                name: "IX_ArtistBackImage_ArtistId",
                schema: "dbo",
                table: "ArtistBackImage");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistHeadImage_ArtistId",
                schema: "dbo",
                table: "ArtistHeadImage",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistContextImage_ArtistId",
                schema: "dbo",
                table: "ArtistContextImage",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistBackImage_ArtistId",
                schema: "dbo",
                table: "ArtistBackImage",
                column: "ArtistId");
        }
    }
}
