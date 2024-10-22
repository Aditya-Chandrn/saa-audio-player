using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
  public class User
  {
    public int Id { get; set; }

    [Required(ErrorMessage = "Username required")]
    public string? Username { get; set; }

    [Required(ErrorMessage = "Email required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password required")]
    public string? Password { get; set; }
    
    public string? Image { get; set; }
    public List<Audio> Audios { get; set; } = [];
    public List<Playlist> Playlists { get; set; } = [];
  }
}