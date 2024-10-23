using backend.Config;
using backend.DTO.AudioDTO;
using backend.Models;
using backend.Utils;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.AudioServices
{
    public partial class AudioServices
    {
        

        // Edit audio
        public async Task<EditAudioResult> EditAudio(EditAudioRequest request)
        {
            try
            {
                var audioToUpdate = await _context.Audios.FindAsync(request.AudioId);
                if (audioToUpdate == null)
                {
                    return new EditAudioResult { StatusCode = 404, Message = "Audio not found." };
                }

                // Update audio properties
                audioToUpdate.Title = request.Title ?? audioToUpdate.Title; // Keep existing title if not provided
                audioToUpdate.AudioBase64String = request.AudioBase64String ?? audioToUpdate.AudioBase64String;
                audioToUpdate.Album = request.Album ?? audioToUpdate.Album;

                // Save changes to the database
                await _context.SaveChangesAsync();
                return new EditAudioResult { StatusCode = 200, Message = "Audio updated successfully." };
            }
            catch (Exception exception)
            {
                new PrintError("Error updating audio", exception);
                return new EditAudioResult { StatusCode = 500, Message = "Error updating audio." };
            }
        }

        
    }
}
