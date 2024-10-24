namespace backend.DTO
{
  public class AudioRequest
  {
    public int? UserId { get; set; }
    public int? AudioId { get; set; }
    public string? AudioBase64String { get; set; }
    public string? Title { get; set; }
    public string? Album { get; set; }
  }

  public class AudioResponse
  {
    public AudioData? Audio { get; set; }
    public int StatusCode { get; set; }
    public string? Message { get; set; }
  }

  public class AudioData
  {
    public int AudioId { get; set; }
    public string? AudioBase64String { get; set; }
    public string? Title { get; set; }
    public string? Album { get; set; }
  }
}