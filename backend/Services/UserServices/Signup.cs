using backend.DTO.UserDTO;
using backend.Models;
using backend.Utils;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.UserServices
{
  public partial class UserServices
  {
    public async Task<SignupResult> Signup(string username, string email, string password)
    {
      try
      {
        // check if username already exists
        var existingUser = await _context.Users.FirstOrDefaultAsync(user => user.Username == username);
        if (existingUser != null)
        {
          new PrintFailure($"Username '{username}' already exists");
          return new SignupResult { StatusCode = 400, Message = "Username already exists" };
        }

        existingUser = await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
        if (existingUser != null)
        {
          new PrintFailure($"Email '{email}' already registered");
          return new SignupResult { StatusCode = 400, Message = "Email already registered" };
        }

        // hash password
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

        // create new user
        var newUser = new User
        {
          Username = username,
          Email = email,
          Password = hashedPassword
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        new PrintSuccess($"Created user with username '{username}' and email '{email}'");
        return new SignupResult { StatusCode = 200, Message = "User created successfully" };
      }
      catch (Exception exception)
      {
        new PrintError("Error creating new user", exception);
        return new SignupResult { StatusCode = 500, Message = "Error creating new user" };
      }
    }
  }
}