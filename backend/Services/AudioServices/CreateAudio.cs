using backend.Config;
using backend.DTO;
using backend.Models;
using backend.Utils;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.AudioServices
{
  public partial class AudioServices
  {
    private readonly ApplicationDbContext _context;

    public AudioServices(ApplicationDbContext context)
    {
      _context = context;
    }

    // Create audio
    public async Task<AudioResponse> CreateAudio(int? userId, string? audioBase64String, string? title, string? album = null)
    {
      // check if username already exists
      var existingUser = await _context.Users.FindAsync(userId);

      if (existingUser == null)
      {
        new PrintFailure($"User '{userId}' not found");
        return new AudioResponse { StatusCode = 404, Message = "User not found" };
      }

      if (string.IsNullOrEmpty(audioBase64String))
      {
        new PrintFailure($"Audio content not provided by user '{userId}'");
        return new AudioResponse { StatusCode = 400, Message = "Audio content required" };
      }
      if (string.IsNullOrEmpty(title))
      {
        new PrintFailure($"Audio title not provided by user '{userId}'");
        return new AudioResponse { StatusCode = 400, Message = "Audio title required" };
      }

      try
      {
        Console.WriteLine("****************** Before creating audio ********************");
        // Create a new audio entity
        var newAudio = new Audio
        {
          Title = title,
          AudioBase64String = audioBase64String,
          Album = album
        };

        Console.WriteLine("****************** After creating audio ********************");

        // Add the audio to the database
        _context.Audios.Add(newAudio);
        await _context.SaveChangesAsync();

        Console.WriteLine("****************** After saving audio ********************");

        var defaultPlaylist = await _context.Playlists.FirstOrDefaultAsync(p => p.Name == $"{existingUser.Username}_Default" && p.CreatorId == userId);
        if (defaultPlaylist != null)
        {
          // Add the relationship to PlaylistAudio
          var playlistAudio = new PlaylistAudio
          {
            PlaylistId = defaultPlaylist.Id,
            AudioId = newAudio.Id
          };

          _context.PlaylistAudios.Add(playlistAudio); // Add the relationship
          await _context.SaveChangesAsync(); // Save the changes
        }

        var audio = new AudioData { AudioId = newAudio.Id };
        new PrintSuccess($"Created audio '{newAudio.Id}' by user '{userId}'");
        return new AudioResponse { Audio = audio, StatusCode = 200, Message = "Audio created successfully." };
      }
      catch (Exception exception)
      {
        new PrintError($"Error creating new audio by user '{userId}'", exception);
        return new AudioResponse { StatusCode = 500, Message = "Error creating new audio." };
      }
    }
  }
}
