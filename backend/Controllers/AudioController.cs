using backend.Services.AudioServices;
using backend.DTO.AudioDTO;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
	[Route("/api/audio")]
	public class AudioController : ControllerBase
	{
		private readonly AudioServices _audioServices;

		public AudioController(AudioServices audioServices)
		{
			_audioServices = audioServices;
		}

		//////// CREATE AUDIO //////////
		[HttpPost("/create")]
		public async Task<IActionResult> CreateAudio([FromBody] CreateAudioRequest createAudioRequest)
		{
			if (createAudioRequest == null)
				return BadRequest(new { Message = "Audio details not found" });

			// Create audio
			var result = await _audioServices.CreateAudio(createAudioRequest.UserId, createAudioRequest.Title, createAudioRequest.Album, createAudioRequest.AudioBase64String);
			return StatusCode(result.StatusCode, new { result.Message, result.AudioId });
		}

		//////// GET AUDIO //////////
		[HttpGet("/get")]
		public async Task<IActionResult> GetAudio([FromQuery] int audioId)
		{
			// Get audio details by ID
			var result = await _audioServices.GetAudio(audioId);
			if (result.Audio == null)
				return NotFound(new { Message = "Audio not found" });

			return Ok(new { result.Audio, result.Message });
		}

		//////// EDIT AUDIO //////////
		[HttpPatch("/edit")]
		public async Task<IActionResult> EditAudio([FromBody] EditAudioRequest editAudioRequest)
		{
			if (editAudioRequest == null)
				return BadRequest(new { Message = "Audio details not found" });

			// Edit audio
			var result = await _audioServices.EditAudio(editAudioRequest.AudioId, editAudioRequest.Title, editAudioRequest.Album, editAudioRequest.AudioBase64String);
			return StatusCode(result.StatusCode, new { result.Message, result.Audio });
		}

		//////// DELETE AUDIO //////////
		[HttpDelete("/delete")]
		public async Task<IActionResult> DeleteAudio([FromBody] DeleteAudioRequest deleteAudioRequest)
		{
			if (deleteAudioRequest == null)
				return BadRequest(new { Message = "Audio details not found" });

			// Delete audio
			var result = await _audioServices.DeleteAudio(deleteAudioRequest.AudioId);
			return StatusCode(result.StatusCode, new { result.Message });
		}

	}
}
