namespace backend.DTO.PlaylistDTO
{
  public class AddAudioToPlaylistRequest
  {
    public int PlaylistId { get; set; } // The ID of the playlist
    public int AudioId { get; set; } // The ID of the audio to add
  }

  public class AddAudioToPlaylistResult
  {
    public int StatusCode { get; set; }
    public string? Message { get; set; }
  }
}
