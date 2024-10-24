namespace backend.DTO.AudioDTO
{
  public class GetAudioRequest
  {
    public int AudioId { get; set; }
  }

  public class GetAudioResult
  {

    public CreateAudioResult.AudioData? Audio { get; set; }
    public int StatusCode { get; set; }
    public string? Message { get; set; }
  }
}