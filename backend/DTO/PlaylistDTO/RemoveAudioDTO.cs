namespace backend.DTO.PlaylistDTO
{
  public class RemoveAudioRequest
  {
    public int PlaylistId { get; set; } // The ID of the playlist
    public int AudioId { get; set; } // The ID of the audio to remove
  }

  public class RemoveAudioResult
  {
    public int StatusCode { get; set; }
    public string? Message { get; set; }
  }
}
