using backend.Services.PlaylistServices;
using backend.DTO;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
	[Route("api/playlist")]
	public class PlaylistController : ControllerBase
	{
		private readonly PlaylistServices _playlistServices;

		public PlaylistController(PlaylistServices playlistServices)
		{
			_playlistServices = playlistServices;
		}

		//////// CREATE PLAYLIST //////////
		[HttpPost("create")]
		public async Task<IActionResult> CreatePlaylist([FromBody] PlaylistRequest createRequest)
		{
			if (createRequest == null)
				return BadRequest(new { Message = "Playlist details not found" });

			// Create playlist
			var result = await _playlistServices.CreatePlaylist(createRequest.UserId, createRequest.Name);
			return StatusCode(result.StatusCode, new { result.Message, result.Playlist });
		}

		//////// GET PLAYLIST //////////
		[HttpGet("get")]
		public async Task<IActionResult> GetPlaylist([FromQuery] PlaylistRequest getRequest)
		{
			// Get playlist details by ID
			var result = await _playlistServices.GetPlaylist(getRequest.PlaylistId);
			if (result.Playlist == null)
				return NotFound(new { Message = "Playlist not found" });

			return Ok(new { result.Playlist, result.Message });
		}

		//////// EDIT PLAYLIST //////////
		[HttpPatch("edit")]
		public async Task<IActionResult> EditPlaylist([FromBody] PlaylistRequest editRequest)
		{
			if (editRequest == null)
				return BadRequest(new { Message = "Playlist details not found" });

			// Edit playlist
			var result = await _playlistServices.EditPlaylist(editRequest.PlaylistId, editRequest.Name);
			return StatusCode(result.StatusCode, new { result.Message, result.Playlist });
		}

		//////// DELETE PLAYLIST //////////
		[HttpDelete("delete")]
		public async Task<IActionResult> DeletePlaylist([FromBody] PlaylistRequest deleteRequest)
		{
			if (deleteRequest == null)
				return BadRequest(new { Message = "Playlist details not found" });

			// Delete playlist
			var result = await _playlistServices.DeletePlaylist(deleteRequest.PlaylistId);
			return StatusCode(result.StatusCode, new { result.Message });
		}

		//////// ADD AUDIO TO PLAYLIST //////////
		[HttpPost("add-audio")]
		public async Task<IActionResult> AddAudioToPlaylist([FromBody] PlaylistRequest addRequest)
		{
			if (addRequest == null)
				return BadRequest(new { Message = "Details not found" });

			// Add audio to playlist
			var result = await _playlistServices.AddAudio(addRequest.PlaylistId, addRequest.AudioId);
			return StatusCode(result.StatusCode, new { result.Message });
		}

		//////// REMOVE AUDIO FROM PLAYLIST //////////
		[HttpPost("remove-audio")]
		public async Task<IActionResult> RemoveAudioFromPlaylist([FromBody] PlaylistRequest removeRequest)
		{
			if (removeRequest == null)
				return BadRequest(new { Message = "Details not found" });

			// Remove audio from playlist
			var result = await _playlistServices.RemoveAudio(removeRequest.PlaylistId, removeRequest.AudioId);
			return StatusCode(result.StatusCode, new { result.Message });
		}
	}
}
