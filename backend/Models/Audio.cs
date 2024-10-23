using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
  public class Audio
  {
    public int Id { get; set; }

    [Required(ErrorMessage = "Audio Title required")]
    public string? Title { get; set; }
    
    [Url]
    public string? AudioBase64String { get; set; }
    public string? Time { get; set; }
    public string? Album { get; set; }

    public List<PlaylistAudio> PlaylistsAudios { get; set; } = [];
  }
}