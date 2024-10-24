using backend.Config;
using backend.DTO;
using backend.Models;
using backend.Utils;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.PlaylistServices
{
  public partial class PlaylistServices
  {
    public async Task<PlaylistResponse> RemoveAudio(int? playlistId, int? audioId)
    {
      try
      {
        // Check if the playlist exists
        var existingPlaylist = await _context.Playlists.FindAsync(playlistId);
        if (existingPlaylist == null)
        {
          new PrintFailure($"Playlist {playlistId} not found.");
          return new PlaylistResponse
          {
            StatusCode = 404,
            Message = "Playlist not found"
          };
        }

        // Check if the audio exists
        var existingAudio = await _context.Audios.FindAsync(audioId);
        if (existingAudio == null)
        {
          new PrintFailure($"Audio {audioId} not found.");
          return new PlaylistResponse
          {
            StatusCode = 404,
            Message = "Audio not found"
          };
        }

        // Create the association in PlaylistAudio
        var existingPlaylistAudio = await _context.PlaylistAudios
           .FirstOrDefaultAsync(pa => pa.PlaylistId == playlistId && pa.AudioId == audioId);

        if (existingPlaylistAudio == null)
        {
          new PrintFailure($"Audio '{audioId}' is not part of Playlist '{playlistId}'");
          return new PlaylistResponse
          {
            StatusCode = 404,
            Message = "Audio is not in the playlist"
          };
        }

        _context.PlaylistAudios.Remove(existingPlaylistAudio);
        await _context.SaveChangesAsync();

        new PrintSuccess($"Audio '{audioId}' removed from playlist '{playlistId}'");
        return new PlaylistResponse
        {
          StatusCode = 200,
          Message = "Audio removed from playlist successfully"
        };
      }
      catch (Exception exception)
      {
        new PrintError($"Error removing audio '{audioId}' from playlist '{playlistId}'", exception);
        return new PlaylistResponse { StatusCode = 500, Message = "Error removing audio from playlist" };
      }
    }
  }
}
