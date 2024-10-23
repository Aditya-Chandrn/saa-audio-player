// using backend.Config;
// using backend.DTO.AudioDTO;
// using backend.Models;
// using backend.Utils;
// using Microsoft.EntityFrameworkCore;

// namespace backend.Services.AudioServices
// {
//   public partial class AudioServices
//   {

//     // Get audio by ID
//     public async Task<Audio> GetAudio(int audioId)
//     {
//       return await _context.Audios
//           .Include(a => a.PlaylistsAudios)
//           .ThenInclude(pa => pa.Playlist) // Include related playlists if necessary
//           .FirstOrDefaultAsync(a => a.Id == audioId);
//     }


//   }
// }
