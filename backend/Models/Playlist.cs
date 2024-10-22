using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
  public class Playlist
  {
    public int Id { get; set; }
    [Required(ErrorMessage = "Playlist Name required")]
    public string? Name { get; set; }
    public string? ShareLink { get; set; }
    public List<PlaylistAudio> PlaylistAudios { get; set; } = [];

    public int CreatorId { get; set; }
    public User? Creator { get; set; }

  }
}