namespace backend.DTO.UserDTO
{
  public class LoginRequest
  {
    public string? Username { get; set; }
    public string? Password { get; set; }
  }

  public class LoginResult
  {
    // user data
    public class UserData
    {
      public int UserId { get; set; }
      public string? Username { get; set; }
      public string? Image { get; set; }
      public List<PlaylistInfo> Playlists { get; set; } = [];
    }

    // playlist info
    public class PlaylistInfo
    {
      public int PlaylistId { get; set; }
      public string? Name { get; set; }
    }

    public int StatusCode { get; set; }
    public string? Message { get; set; }

    public UserData? User { get; set; }
  }
}