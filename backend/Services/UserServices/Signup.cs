using backend.DTO.UserDTO;
using backend.Models;
using backend.Utils;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.UserServices
{
  public partial class UserServices
  {
    public async Task<SignupResult> Signup(string username, string password)
    {
      try
      {
        // Check if username already exists
        var existingUser = await _context.Users.FirstOrDefaultAsync(user => user.Username == username);
        if (existingUser != null)
        {
          new PrintFailure($"Username '{username}' already exists");
          return new SignupResult { StatusCode = 400, Message = "Username already exists" };
        }

        // Hash password
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

        // Create new user
        var newUser = new User
        {
          Username = username,
          Password = hashedPassword,
          Playlists = new List<Playlist>() // Initialize the Playlists collection
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync(); // Save the new user to get the Id

        // Create default playlist for the user
        var defaultPlaylist = new Playlist
        {
          Name = $"{username}_Default",
          CreatorId = newUser.Id // Link the playlist to the new user
        };

        _context.Playlists.Add(defaultPlaylist);

        // Save changes to include the new playlist in the database
        await _context.SaveChangesAsync();

        // Map the newly created playlist to PlaylistInfo format
        var playlistInfo = new LoginResult.PlaylistInfo
        {
          PlaylistId = defaultPlaylist.Id,
          Name = defaultPlaylist.Name,
        };

        // Prepare the user data to send in the response
        var userData = new LoginResult.UserData
        {
          UserId = newUser.Id,
          Username = newUser.Username,
          Playlists = new List<LoginResult.PlaylistInfo> { playlistInfo } // Only include the default playlist
        };

        new PrintSuccess($"Created user with username '{username}'");
        return new SignupResult { User = userData, StatusCode = 200, Message = "User created successfully" };
      }
      catch (Exception exception)
      {
        new PrintError("Error creating new user", exception);
        return new SignupResult { StatusCode = 500, Message = "Error creating new user" };
      }
    }
  }
}