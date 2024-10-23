using backend.Config;
using backend.DTO.UserDTO;
using backend.Utils;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.AudioServices
{
  public partial class AudioServices
  {
    private readonly ApplicationDbContext _context;

    public AudioServices(ApplicationDbContext context)
    {
      _context = context;
    }
    public async Task<LoginResult> Login(string username, string password)
    {
      try
      {
        // check if user exists
        var existingUser = await _context.Users.FirstOrDefaultAsync(user => user.Username == username);
        if (existingUser == null)
        {
          new PrintFailure($"User '{username}' not found.");
          return new LoginResult { StatusCode = 404, Message = "Invalid Credentials" };
        }

        // check password
        if (!BCrypt.Net.BCrypt.Verify(password, existingUser.Password))
        {
          new PrintFailure($"Wrong password entered for user '{username}'");
          return new LoginResult { StatusCode = 404, Message = "Invalid Credentials" };
        }

        // prepare user data to be sent
        var userData = new LoginResult.UserData { UserId = existingUser.Id, Username = existingUser.Username, Email = existingUser.Email, Image = existingUser.Image };
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