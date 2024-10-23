namespace backend.DTO.AudioDTO
{
  public class GetAudioRequest
  {
    public int AudioId { get; set; }
  }

  public class GetAudioResult
  {
    public string? AudioBase64String { get; set; }
    public string? Title { get; set; }
    public string? Album { get; set; }
    public string? Creator { get; set; }
    public int StatusCode { get; set; }
    public string? Message { get; set; }
  }
}