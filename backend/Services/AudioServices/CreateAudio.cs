using backend.Config;
using backend.DTO.AudioDTO;
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
    public async Task<CreateAudioResult> CreateAudio(int userId, string title, string audioBase64String, string? album = null)
    {
      if (string.IsNullOrEmpty(audioBase64String))
      {
        new PrintFailure($"Audio content not provided by user '{userId}'");
        return new CreateAudioResult { StatusCode = 400, Message = "Audio content required" };
      }
      if (string.IsNullOrEmpty(title))
      {
        new PrintFailure($"Audio title not provided by user '{userId}'");
        return new CreateAudioResult { StatusCode = 400, Message = "Audio title required" };
      }

      try
      {
        // Create a new audio entity
        var newAudio = new Audio
        {
          Title = title,
          AudioBase64String = audioBase64String,
          Album = album,
        };

        // Add the audio to the database
        _context.Audios.Add(newAudio);
        await _context.SaveChangesAsync();

        var defaultPlaylist = await _context.Playlists.FirstOrDefaultAsync(p => p.Name == "Default" && p.CreatorId == userId);
        if (defaultPlaylist != null)
        {
          newAudio.PlaylistsAudios.Add(new PlaylistAudio { PlaylistId = defaultPlaylist.Id, AudioId = newAudio.Id });
          await _context.SaveChangesAsync();
        }

        return new CreateAudioResult { AudioId = newAudio.Id, StatusCode = 200, Message = "Audio created successfully." };
      }
      catch (Exception exception)
      {
        new PrintError("Error creating new audio", exception);
        return new CreateAudioResult { StatusCode = 500, Message = "Error creating new audio." };
      }
    }
  }
}
