using backend.Config;
using backend.DTO;
using backend.Models;
using backend.Utils;

namespace backend.Services.PlaylistServices
{
  public partial class PlaylistServices
  {
    public async Task<PlaylistResponse> DeletePlaylist(int? playlistId)
    {
      try
      {
        var existingPlaylist = await _context.Playlists.FindAsync(playlistId);

        if (existingPlaylist == null)
        {
          new PrintFailure($"Playlist '{playlistId}' not found");
          return new PlaylistResponse { StatusCode = 404, Message = "Playlist not found" };
        }

        _context.Playlists.Remove(existingPlaylist);
        await _context.SaveChangesAsync();

        new PrintSuccess($"Playlist '{playlistId}' deleted");
        return new PlaylistResponse { StatusCode = 200, Message = "Playlist deleted successfully" };
      }
      catch (Exception exception)
      {
        new PrintError($"Error deleting playlist '{playlistId}'", exception);
        return new PlaylistResponse { StatusCode = 500, Message = "Error deleting playlist" };
      }
    }
  }
}
