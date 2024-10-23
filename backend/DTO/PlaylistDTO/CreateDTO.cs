namespace backend.DTO.PlaylistDTO
{
  public class CreatePlaylistRequest
  {
    public int UserId { get; set; } // The ID of the user creating the playlist
    public string? Title { get; set; } // Playlist title
    public string? Description { get; set; } // Optional: Playlist description
  }

  public class CreatePlaylistResult
  {
    public int PlaylistId { get; set; } // ID of the newly created playlist
    public int StatusCode { get; set; }
    public string? Message { get; set; }
  }
}
