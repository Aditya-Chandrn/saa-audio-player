using backend.DTO.UserDTO;
using backend.Models;
using backend.Utils;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.UserServices
{
  public partial class UserServices
  {
    public async Task<EditUserResult> EditUser(int userId, string? username, string? password = null, string? image = null)
    {
      try
      {
        // check if username already exists
        var existingUser = await _context.Users.FindAsync(userId);

        if (existingUser == null)
        {
          new PrintFailure($"User '{userId}' not found");
          return new EditUserResult { StatusCode = 404, Message = "User not found" };
        }

        // update password
        if (password != null)
        {
          string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
          existingUser.Password = hashedPassword;
        }

        // update image
        if (image != null) existingUser.Image = image;

        Console.WriteLine(existingUser.Username);

        await _context.SaveChangesAsync();

        var userData = new EditUserResult.UserData { UserId = existingUser.Id, Username = existingUser.Username, Image = existingUser.Image };
        new PrintSuccess($"Edited user '{userId}'");
        return new EditUserResult { StatusCode = 200, Message = "User details edited successfully", User = userData };
      }
      catch (Exception exception)
      {
        new PrintError($"Error editing user '{userId}'", exception);
        return new EditUserResult { StatusCode = 500, Message = "Error editing user details" };
      }
    }
  }
}