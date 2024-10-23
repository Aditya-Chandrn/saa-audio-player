using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
  public class Playlist
  {
    public int Id { get; set; }
    [Required(ErrorMessage = "Playlist Name required")]
    public string? Name { get; set; }
    public string? ShareLink { get; set; }
    public List<PlaylistAudio> PlaylistAudios { get; set; } = [];


    [ForeignKey("CreatorId")]
    public int CreatorId { get; set; }
    public User? Creator { get; set; }

  }
}