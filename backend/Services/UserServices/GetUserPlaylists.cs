using backend.DTO.UserDTO;
using backend.Utils;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.UserServices
{
  public partial class UserServices
  {
    public async Task<GetPlaylistResponse> GetUserPlaylists(int userId)
    {
      try
      {
        var existingUser = await _context.Users.Include(u => u.Playlists).FirstOrDefaultAsync(u => u.Id == userId);

        if (existingUser == null)
        {
          new PrintFailure($"User '{userId}' not found");
          return new GetPlaylistResponse { StatusCode = 404, Message = "User not found" };
        }
        
        // Prepare user data to be sent
        var playlistInfos = existingUser.Playlists.Select(playlist => new LoginResult.PlaylistInfo
        {
          PlaylistId = playlist.Id,
          Name = playlist.Name,
        }).ToList();

        new PrintSuccess($"Fetched playlists of user '{userId}'");
        return new GetPlaylistResponse { Playlists = playlistInfos, StatusCode = 200, Message = "User's playlists fetched successfully" };
      }
      catch (Exception exception)
      {
        new PrintError($"Error fetching playlists of user '{userId}'", exception);
        return new GetPlaylistResponse { StatusCode = 500, Message = "Error fetching user's playlists" };
      }
    }

  }
}