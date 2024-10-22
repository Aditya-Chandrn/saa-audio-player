namespace backend.DTO
{
  public class DeleteUserRequest
  {
    public int UserId { get; set; }
  }

  public class DeleteUserResult
  {
    public int StatusCode { get; set; }
    public string? Message { get; set; }
  }
}