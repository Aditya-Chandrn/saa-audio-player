using backend.Config;
using backend.DTO;
using backend.Models;
using backend.Utils;

namespace backend.Services.PlaylistServices
{

  public partial class PlaylistServices
  {
    private readonly ApplicationDbContext _context;

    public PlaylistServices(ApplicationDbContext context)
    {
      _context = context;
    }
    public async Task<PlaylistResponse> CreatePlaylist(int? userId, string? name)
    {
      try
      {
        // check if username already exists
        var existingUser = await _context.Users.FindAsync(userId);

        if (existingUser == null)
        {
          new PrintFailure($"User '{userId}' not found");
          return new PlaylistResponse { StatusCode = 404, Message = "User not found" };
        }

        var playlist = new Playlist
        {
          Name = name,
          CreatorId = existingUser.Id
        };
        
        _context.Playlists.Add(playlist);
        await _context.SaveChangesAsync();

        var playlistData = new PlaylistData { PlaylistId = playlist.Id, Name = playlist.Name };

        new PrintSuccess($"Playlist '{playlist.Id}' created");
        return new PlaylistResponse { Playlist = playlistData, StatusCode = 200, Message = "Playlist created successfully" };
      }
      catch (Exception exception)
      {
        new PrintError($"Error creating playlist by user '{userId}'", exception);
        return new PlaylistResponse { StatusCode = 500, Message = "Error creating playlist" };
      }
    }
  }
}
