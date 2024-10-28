namespace backend.DTO
{

  public class PlaylistRequest
  {
    public int? UserId { get; set; }
    public int? PlaylistId { get; set; }
    public int? AudioId { get; set; }
    public string? ImageBase64String { get; set; }
    public string? Name { get; set; }

  }

  public class PlaylistResponse
  {
    public PlaylistData? Playlist { get; set; }
    public int StatusCode { get; set; }
    public string? Message { get; set; }
  }

  public class PlaylistData
  {
    public int PlaylistId { get; set; }
    public string? Name { get; set; }
    public string? ImageBase64String { get; set; }
    public List<AudioInfo> Audios { get; set; } = [];
  }

  public class AudioInfo
  {
    public int AudioId { get; set; }
    public string? Title { get; set; }
    public string? Album { get; set; }
  }
}

