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
    public string? Password { get; set; }
    
    public string? Image { get; set; }
    public List<Playlist> Playlists { get; set; } = [];
  }
}