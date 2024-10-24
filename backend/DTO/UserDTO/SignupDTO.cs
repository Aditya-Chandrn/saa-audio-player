namespace backend.DTO.UserDTO
{
  public class SignupRequest
  {
    public string? Username { get; set; }
    public string? Password { get; set; }
  }

  public class SignupResult
  {
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public LoginResult.UserData? User { get; set; }
  }
}