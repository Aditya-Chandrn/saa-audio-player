namespace backend.DTO.AudioDTO
{
  public class CreateAudioRequest
  {
    public int UserId { get; set; }
    public string? AudioBase64String { get; set; }
    public string? Title { get; set; }
    public string? Album { get; set; }
  }

  public class CreateAudioResult
  {
    public int AudioId { get; set; }
    public int StatusCode { get; set; }
    public string? Message { get; set; }
  }
}