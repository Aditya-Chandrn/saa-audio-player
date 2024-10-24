using backend.DTO;
using backend.Utils;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.AudioServices
{
	public partial class AudioServices
	{
		// Edit audio
		public async Task<AudioResponse> EditAudio(int? audioId, string? title, string? album)
		{
			try
			{
				var audio = await _context.Audios.FindAsync(audioId);
				if (audio == null)
				{
					new PrintFailure($"Audio {audioId} not found");
					return new AudioResponse { StatusCode = 404, Message = "Audio not found." };
				}

				// Update audio properties
				if(!string.IsNullOrEmpty(title)) audio.Title = title;
				if(!string.IsNullOrEmpty(album)) audio.Album = album;

				// Save changes to the database
				await _context.SaveChangesAsync();
				new PrintSuccess($"Edited audio {audioId}");
				return new AudioResponse { StatusCode = 200, Message = "Audio edited successfully." };
			}
			catch (Exception exception)
			{
				new PrintError($"Error editing audio {audioId}", exception);
				return new AudioResponse { StatusCode = 500, Message = "Error editing audio." };
			}
		}


	}
}
