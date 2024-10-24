using backend.Config;
using backend.DTO.UserDTO;
using backend.Utils;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.UserServices
{
  public partial class UserServices
  {
    private readonly ApplicationDbContext _context;

    public UserServices(ApplicationDbContext context)
    {
      _context = context;
    }
    public async Task<LoginResult> Login(string username, string password)
    {
      try
      {
        // Check if user exists
        var existingUser = await _context.Users
            .Include(user => user.Playlists) // Include playlists in the query
            .FirstOrDefaultAsync(user => user.Username == username);

        if (existingUser == null)
        {
          new PrintFailure($"User '{username}' not found.");
          return new LoginResult { StatusCode = 404, Message = "Invalid Credentials" };
        }

        // Check password
        if (!BCrypt.Net.BCrypt.Verify(password, existingUser.Password))
        {
          new PrintFailure($"Wrong password entered for user '{username}'");
          return new LoginResult { StatusCode = 404, Message = "Invalid Credentials" };
        }

        // Prepare user data to be sent
        var playlistInfos = existingUser.Playlists.Select(playlist => new LoginResult.PlaylistInfo
        {
          PlaylistId = playlist.Id,
          Name = playlist.Name,
        }).ToList();

        var userData = new LoginResult.UserData
        {
          UserId = existingUser.Id,
          Username = existingUser.Username,
          Image = existingUser.Image,
          Playlists = playlistInfos // Include the playlists in the response
        };

        new PrintSuccess($"User '{username}' logged in");
        return new LoginResult { StatusCode = 200, Message = "Login successful", User = userData };
      }
      catch (Exception exception)
      {
        new PrintError($"Error logging in username '{username}'", exception);
        return new LoginResult { StatusCode = 500, Message = "Error logging in" };
      }
    }
  }
}