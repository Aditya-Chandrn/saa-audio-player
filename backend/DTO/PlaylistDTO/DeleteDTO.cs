namespace backend.DTO.PlaylistDTO
{
  public class DeletePlaylistRequest
  {
    public int PlaylistId { get; set; } // The ID of the playlist to delete
    public int UserId { get; set; } // The user deleting the playlist
  }

  public class DeletePlaylistResult
  {
    public int StatusCode { get; set; }
    public string? Message { get; set; }
  }
}
