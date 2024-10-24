using backend.DTO;
using backend.Utils;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.AudioServices
{
  public partial class AudioServices
  {

    // Get audio by ID
    public async Task<AudioResponse> GetAudio(int? audioId)
    {
      try
      {
        var audio = await _context.Audios.FirstOrDefaultAsync(a => a.Id == audioId);
        if (audio == null)
        {
          new PrintFailure($"Audio {audioId} not found");
          return new AudioResponse { StatusCode = 404, Message = "Audio not found" };
        }

        var audioData = new AudioData { AudioId = audio.Id, Title = audio.Title, Album = audio.Album, AudioBase64String = audio.AudioBase64String };
        return new AudioResponse { Audio = audioData, StatusCode = 200, Message = "Audio fetched successfully." };
      }
      catch (Exception exception)
      {
        new PrintError($"Error feching audio {audioId}", exception);
        return new AudioResponse { StatusCode = 500, Message = "Error fetching audio." };
      }
    }

  }
}
