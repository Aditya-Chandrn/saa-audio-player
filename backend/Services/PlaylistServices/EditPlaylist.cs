using backend.Config;
using backend.DTO;
using backend.Models;
using backend.Utils;

namespace backend.Services.PlaylistServices
{
  public partial class PlaylistServices
  {
    public async Task<PlaylistResponse> EditPlaylist(int? playlistId, string? name)
    {
      try
      {
        var existingPlaylist = await _context.Playlists.FindAsync(playlistId);

        if (existingPlaylist == null)
        {
          new PrintFailure($"Playlist '{playlistId}' not found");
          return new PlaylistResponse { StatusCode = 404, Message = "Playlist not found" };
        }

        if (!string.IsNullOrEmpty(name)) existingPlaylist.Name = name;

        await _context.SaveChangesAsync();

        var playlistData = new PlaylistData { PlaylistId = existingPlaylist.Id, Name = existingPlaylist.Name };
        new PrintSuccess($"Playlist '{playlistId}' edited");
        return new PlaylistResponse { Playlist = playlistData, StatusCode = 200, Message = "Playlist edited successfully" };
      }
      catch (Exception exception)
      {
        new PrintError($"Error editing playlist '{playlistId}'", exception);
        return new PlaylistResponse { StatusCode = 500, Message = "Error editing playlist" };
      }
    }
  }
}
