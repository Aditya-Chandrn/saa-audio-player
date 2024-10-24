namespace backend.DTO.AudioDTO
{

  public class EditAudioRequest
  {
    public int UserId { get; set; }
    public int AudioId { get; set; }
    public string? Title { get; set; }
    public string? Album { get; set; }
  }

  public class EditAudioResult
  {
    public CreateAudioResult.AudioData? Audio { get; set; }
    public int StatusCode { get; set; }
    public string? Message { get; set; }
  }
}

