using backend.DTO.UserDTO;
using backend.Models;
using backend.Utils;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.UserServices
{
  public partial class UserServices
  {
    public async Task<EditUserResult> EditUser(int userId, string? username = null, string? email = null, string? password = null, string? image = null)
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

        // update username
        if (username != null)
        {
          var duplicateUser = await _context.Users.FirstOrDefaultAsync(user => user.Username == username);
          if (duplicateUser != null)
          {
            new PrintFailure($"User with '{username}' already exists");
            return new EditUserResult { StatusCode = 400, Message = "Username already exists" };
          }

          existingUser.Username = username;
        }

        // update email
        if (email != null) existingUser.Email = email;

        // update image
        if (image != null)
        {
          // TODO: handle image update
        }

        Console.WriteLine(existingUser.Username);

        await _context.SaveChangesAsync();

        var userData = new EditUserResult.UserData { UserId = existingUser.Id, Username = existingUser.Username, Email = existingUser.Email, Image = existingUser.Image };
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