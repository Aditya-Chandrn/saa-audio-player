namespace backend.DTO.PlaylistDTO
{
  public class EditPlaylistRequest
  {
    public int PlaylistId { get; set; } // The ID of the playlist to edit
    public int UserId { get; set; } // The user editing the playlist
    public string? Title { get; set; } // Optional: Edit the playlist title
    public string? Description { get; set; } // Optional: Edit the playlist description
  }

  public class EditPlaylistResult
  {
    public int StatusCode { get; set; }
    public string? Message { get; set; }
  }
}
