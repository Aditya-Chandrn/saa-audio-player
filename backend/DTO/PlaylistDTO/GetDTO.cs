namespace backend.DTO.PlaylistDTO
{
  public class GetRequest
  {
    public int? PlaylistId { get; set; } // Optional: Fetch specific playlist by PlaylistId
    public int? UserId { get; set; } // Optional: Fetch all playlists for a user
  }

  public class GetResult
  {
    public class PlaylistData
    {
      public int PlaylistId { get; set; }
      public string? Name { get; set; }
      public List<int> Audios { get; set; } = [];
    }

    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public PlaylistData? Playlist { get; set; }
  }
}
