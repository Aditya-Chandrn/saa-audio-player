using backend.DTO.UserDTO;
using backend.Utils;

namespace backend.Services.UserServices
{
  public partial class UserServices
  {
    public async Task<DeleteUserResult> DeleteUser(int userId, string? username = null, string? email = null, string? password = null, string? image = null)
    {
      try
      {
        // check if username already exists
        var existingUser = await _context.Users.FindAsync(userId);

        if (existingUser == null)
        {
          new PrintFailure($"User '{userId}' not found");
          return new DeleteUserResult { StatusCode = 404, Message = "User not found" };
        }

        _context.Users.Remove(existingUser);
        await _context.SaveChangesAsync();

        new PrintSuccess($"Deleted user '{userId}' ");
        return new DeleteUserResult { StatusCode = 200, Message = "User deleted successfully" };
      }
      catch (Exception exception)
      {
        new PrintError($"Error deleting user '{userId}'", exception);
        return new DeleteUserResult { StatusCode = 500, Message = "Error deleting user" };
      }
    }
  }
}