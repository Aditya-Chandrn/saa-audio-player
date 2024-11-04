using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class DeletePlaylistAudioOnDeletingAudio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistAudios_Audios_AudioId",
                table: "PlaylistAudios");

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistAudios_Audios_AudioId",
                table: "PlaylistAudios",
                column: "AudioId",
                principalTable: "Audios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistAudios_Audios_AudioId",
                table: "PlaylistAudios");

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistAudios_Audios_AudioId",
                table: "PlaylistAudios",
                column: "AudioId",
                principalTable: "Audios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
