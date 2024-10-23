// using backend.Services;
// using backend.DTO;
// using Microsoft.AspNetCore.Mvc;

// namespace backend.Controllers
// {
//     [Route("/api/audio")]
//     public class AudioController : ControllerBase
//     {
//         private readonly AudioServices _audioServices;

//         public AudioController(AudioServices audioServices)
//         {
//             _audioServices = audioServices;
//         }

//         //////// CREATE AUDIO //////////
//         [HttpPost("/create")]
//         public async Task<IActionResult> CreateAudio([FromBody] CreateAudioRequest createAudioRequest)
//         {
//             if (createAudioRequest == null)
//                 return BadRequest(new { Message = "Audio details not found" });

//             // Create audio
//             var result = await _audioServices.CreateAudio(createAudioRequest.UserId, createAudioRequest.Title, createAudioRequest.Album, createAudioRequest.AudioBase64String);
//             return StatusCode(result.StatusCode, new { result.Message, result.AudioId });
//         }

//         //////// GET AUDIO //////////
//         [HttpGet("/get")]
//         public async Task<IActionResult> GetAudio([FromQuery] int audioId)
//         {
//             // Get audio details by ID
//             var result = await _audioServices.GetAudio(audioId);
//             if (result.Audio == null)
//                 return NotFound(new { Message = "Audio not found" });

//             return Ok(new { result.Audio });
//         }

//         //////// EDIT AUDIO //////////
//         [HttpPatch("/edit")]
//         public async Task<IActionResult> EditAudio([FromBody] EditAudioRequest editAudioRequest)
//         {
//             if (editAudioRequest == null)
//                 return BadRequest(new { Message = "Audio details not found" });

//             // Edit audio
//             var result = await _audioServices.EditAudio(editAudioRequest.AudioId, editAudioRequest.Title, editAudioRequest.Album, editAudioRequest.AudioBase64String);
//             return StatusCode(result.StatusCode, new { result.Message, result.Audio });
//         }

//         //////// DELETE AUDIO //////////
//         [HttpDelete("/delete")]
//         public async Task<IActionResult> DeleteAudio([FromBody] DeleteAudioRequest deleteAudioRequest)
//         {
//             if (deleteAudioRequest == null)
//                 return BadRequest(new { Message = "Audio details not found" });

//             // Delete audio
//             var result = await _audioServices.DeleteAudio(deleteAudioRequest.AudioId);
//             return StatusCode(result.StatusCode, new { result.Message });
//         }

//         //////// ADD AUDIO TO PLAYLIST //////////
//         [HttpPost("/add-to-playlist")]
//         public async Task<IActionResult> AddAudioToPlaylist([FromBody] AddAudioToPlaylistRequest addAudioToPlaylistRequest)
//         {
//             if (addAudioToPlaylistRequest == null)
//                 return BadRequest(new { Message = "Playlist or audio details not found" });

//             // Add audio to playlist
//             var result = await _audioServices.AddAudioToPlaylist(addAudioToPlaylistRequest.PlaylistId, addAudioToPlaylistRequest.AudioId);
//             return StatusCode(result.StatusCode, new { result.Message });
//         }

//         //////// REMOVE AUDIO FROM PLAYLIST //////////
//         [HttpDelete("/remove-from-playlist")]
//         public async Task<IActionResult> RemoveAudioFromPlaylist([FromBody] RemoveAudioFromPlaylistRequest removeAudioFromPlaylistRequest)
//         {
//             if (removeAudioFromPlaylistRequest == null)
//                 return BadRequest(new { Message = "Playlist or audio details not found" });

//             // Remove audio from playlist
//             var result = await _audioServices.RemoveAudioFromPlaylist(removeAudioFromPlaylistRequest.PlaylistId, removeAudioFromPlaylistRequest.AudioId);
//             return StatusCode(result.StatusCode, new { result.Message });
//         }
//     }
// }