namespace backend.DTO.UserDTO
{
  public class LoginRequest
  {
    public string? Username { get; set; }
    public string? Password { get; set; }
  }

  public class LoginResult
  {
    public class UserData
    {
      public int UserId { get; set; }
      public string? Username { get; set; }
      public string? Email { get; set; }
      public string? Image { get; set; }
    }

    public int StatusCode { get; set; }
    public string? Message { get; set; }

    public UserData? User { get; set; }
  }
}