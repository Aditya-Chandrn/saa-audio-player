using backend.Config;
using backend.DTO;
using backend.Models;
using backend.Utils;

namespace backend.Services.PlaylistServices
{
  public partial class PlaylistServices
  {
    public async Task<PlaylistResponse> AddAudio(int? playlistId, int? audioId)
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
        var playlistAudio = new PlaylistAudio
        {
          PlaylistId = existingPlaylist.Id,
          AudioId = existingAudio.Id
        };

        _context.PlaylistAudios.Add(playlistAudio);
        await _context.SaveChangesAsync();

        new PrintSuccess($"Audio '{audioId}' added to playlist '{playlistId}'");
        return new PlaylistResponse
        {
          StatusCode = 200,
          Message = "Audio added to playlist successfully"
        };
      }
      catch (Exception exception)
      {
        new PrintError($"Error adding audio '{audioId}' to playlist '{playlistId}'", exception);
        return new PlaylistResponse { StatusCode = 500, Message = "Error adding audio to playlist" };
      }
    }
  }
}
