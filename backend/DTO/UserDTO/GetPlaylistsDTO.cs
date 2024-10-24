namespace backend.DTO.UserDTO
{
  public class GetPlaylistRequest
  {
    public int UserId { get; set; }
  }

  public class GetPlaylistResponse
  {
    public List<LoginResult.PlaylistInfo> Playlists { get; set; } = [];
    public int StatusCode { get; set; }
    public string? Message { get; set; }
  }
}