using backend.Config;
using backend.DTO.AudioDTO;
using backend.Models;
using backend.Utils;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.AudioServices
{
    public partial class AudioServices
    {
        

        // Delete audio
        public async Task<DeleteAudioResult> DeleteAudio(int audioId)
        {
            try
            {
                var audioToDelete = await _context.Audios.FindAsync(audioId);
                if (audioToDelete == null)
                {
                    return new DeleteAudioResult { StatusCode = 404, Message = "Audio not found." };
                }

                // Remove audio from the database
                _context.Audios.Remove(audioToDelete);
                await _context.SaveChangesAsync();
                return new DeleteAudioResult { StatusCode = 200, Message = "Audio deleted successfully." };
            }
            catch (Exception exception)
            {
                new PrintError("Error deleting audio", exception);
                return new DeleteAudioResult { StatusCode = 500, Message = "Error deleting audio." };
            }
        }
    }
}
