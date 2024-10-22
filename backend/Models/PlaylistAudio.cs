using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
  public class PlaylistAudio
  {
    public int PlaylistId { get; set; }
    public Playlist? Playlist { get; set; }

    public int AudioId { get; set; }
    public Audio? Audio { get; set; }
  }
}