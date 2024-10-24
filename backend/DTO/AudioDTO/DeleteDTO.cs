namespace backend.DTO.AudioDTO
{
  public class DeleteAudioRequest
  {
    public int AudioId { get; set; }
    public int UserId { get; set; } 
  }

  public class DeleteAudioResult
  {
    public int StatusCode { get; set; }
    public string? Message { get; set; }
  }
}

